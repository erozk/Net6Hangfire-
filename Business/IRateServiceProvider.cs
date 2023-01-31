using HangfireExchangeRates.Entities;

namespace HangfireExchangeRates.Business
{
    public interface IRateServiceProvider
    {
        List<Symbol> GetSymbols();
        List<Rate> GetRates();
    }
}