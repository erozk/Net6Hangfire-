using System.Linq.Expressions;
using HangfireExchangeRates.DataAccess.Abstract;
using HangfireExchangeRates.Entities;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace HangfireExchangeRates.Business
{
    public class FixerIoApiProvider : IRateServiceProvider
    {
        private readonly IRateDal _ratedal;
        private readonly ISymDal _symboldal;

        public FixerIoApiProvider(IRateDal ratedal, ISymDal symboldal)
        {
            _ratedal = ratedal;
            _symboldal = symboldal;
        }

        public List<Rate> GetRates()
        {
            IList<Symbol> allSymbols = _symboldal.GetList();

            foreach (Symbol item in allSymbols)
            {
                IList<Symbol> CheckSymbol = allSymbols.Where(t => t.SymbolName != item.SymbolName).ToList();
                for (int i = 0; i < CheckSymbol.Count; i++)
                {
                    var client = new RestClient("https://api.apilayer.com/fixer/latest?symbols=" + CheckSymbol[i].SymbolName + "&base=" + item.SymbolName);
                    client.Options.Timeout = -1;

                    var request = new RestRequest("https://api.apilayer.com/fixer/latest?symbols=" + CheckSymbol[i].SymbolName + "&base=" + item.SymbolName, Method.Get);
                    request.AddHeader("apikey", "zfZk4FcF5e7eIaakCPOdSyVdvoHuVD1B");

                    RestResponse response = client.Execute(request);

                    JObject o = JObject.Parse(response.Content);
                    JObject r = (JObject)o["rates"];
                    Rate rate = new Rate
                    {
                        BaseCurrency = (string)o["base"],
                        Date = (DateTime)o["date"],
                        Currency = CheckSymbol[i].SymbolName,
                        CurrencyRate = float.Parse((string)(r[CheckSymbol[i].SymbolName])),
                        TimeStamp = Int64.Parse((string)o["timestamp"])
                    };
                    Expression<Func<Rate, bool>> filter = m => m.BaseCurrency == rate.BaseCurrency && m.Currency == rate.Currency;
                    Rate deleted = _ratedal.Get(filter);
                    _ratedal.Delete(deleted);
                    _ratedal.Add(rate);
                }


            }

            return _ratedal.GetList();

            // {
            //   "base": "EUR",
            //   "date": "2023-01-30",
            //   "rates": {
            //     "GBP": 0.878464
            //   },
            //   "success": true,
            //   "timestamp": 1675119543
            // }

        }

        public List<Symbol> GetSymbols()
        {
            var client = new RestClient("https://api.apilayer.com/fixer/symbols");
            client.Options.Timeout = -1;

            var request = new RestRequest("https://api.apilayer.com/fixer/symbols", Method.Get);
            request.AddHeader("apikey", "zfZk4FcF5e7eIaakCPOdSyVdvoHuVD1B");

            RestResponse response = client.Execute(request);

            JObject o = JObject.Parse(response.Content);

            JObject a = (JObject)o["symbols"];
            List<Symbol> currencyList = new List<Symbol>();
            foreach (JProperty property in a.Properties())
            {
                Symbol s = new Symbol { SymbolName = property.Name, LongName = (string)property.Value };
                currencyList.Add(s);
                Expression<Func<Symbol, bool>> filter = m => m.SymbolName == s.SymbolName;
                Symbol existingSymbol = _symboldal.Get(filter);
                if (string.IsNullOrEmpty(existingSymbol.SymbolName))
                    _symboldal.Add(s);
            }

            return currencyList;


            // {
            //     "success": true,
            //     "symbols": {
            //         "AED": "United Arab Emirates Dirham",
            //         "AFN": "Afghan Afghani",
            //         "ALL": "Albanian Lek",
            //         "AMD": "Armenian Dram",
            //         "ANG": "Netherlands Antillean Guilder",
            //         "AOA": "Angolan Kwanza",
            //         "ARS": "Argentine Peso",
            //         "AUD": "Australian Dollar",
            //         "AWG": "Aruban Florin"
            //      }
            // }
        }

        public class TempSymbol
        {
            public string ShortName { get; set; }
            public string LongName { get; set; }
        }
    }
}