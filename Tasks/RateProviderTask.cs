using HangfireExchangeRates.Business;

namespace HangfireExchangeRates.Tasks
{
    public class RateProviderTask
    {
         private readonly IRateServiceProvider _serviceProvider;

        public RateProviderTask(IRateServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void GetRates()
        {
             _serviceProvider.GetRates();
        }

        public void GetSymbols()
        {
             _serviceProvider.GetSymbols();
        }
    }
}