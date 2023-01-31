using System.ComponentModel.DataAnnotations;
using HangfireExchangeRates.Abstract;

namespace HangfireExchangeRates.Entities
{
    public class Rate : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string BaseCurrency { get; set; }
        public DateTime Date { get; set; }
        public string Currency { get; set; }
        public float CurrencyRate { get; set; }
        public Int64 TimeStamp { get; set; }

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
}