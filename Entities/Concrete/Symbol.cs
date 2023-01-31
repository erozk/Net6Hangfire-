using System.ComponentModel.DataAnnotations;
using HangfireExchangeRates.Abstract;

namespace HangfireExchangeRates.Entities
{
    public class Symbol : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string SymbolName { get; set; }
        public string LongName { get; set; }

    }
}