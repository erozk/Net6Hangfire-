using HangfireExchangeRates.DataAccess.Abstract;
using HangfireExchangeRates.Entities;

namespace HangfireExchangeRates.Business
{
    public class ExchangeRatesApiProvider : IRateServiceProvider
    {
        private readonly IRateDal _ratedal;

        public ExchangeRatesApiProvider(IRateDal ratedal)
        {
            _ratedal = ratedal;
        }

        public List<Rate> GetRates()
        {
            throw new NotImplementedException();
        }

        public List<Symbol> GetSymbols()
        {
            throw new NotImplementedException();
        }

    }
}