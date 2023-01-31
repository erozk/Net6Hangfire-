using Hangfire;
using HangfireExchangeRates.Business;
using HangfireExchangeRates.Tasks;

namespace HangfireExchangeRates
{
    public static class RecurringJobs
    {
        public static void ConsoleTestTask()
        {
            ConsoleTask task = new();

            RecurringJob.AddOrUpdate(() => task.ConsoleWriteTest(), Cron.Minutely);
        }

        public static void GetRates(IRateServiceProvider service)
        {
            RateProviderTask task = new(service);

            RecurringJob.AddOrUpdate(() => task.GetRates(), Cron.MinuteInterval(20));
        }

        public static void GetSymbols(IRateServiceProvider service)
        {
            RateProviderTask task = new(service);

            RecurringJob.AddOrUpdate(() => task.GetSymbols(), Cron.Monthly); 
        }
    }
}
