using Hangfire;
using Hangfire.Heartbeat;
using Hangfire.JobsLogger;
using Hangfire.Storage.SQLite;
using HangfireExchangeRates;
using HangfireExchangeRates.Business;
using HangfireExchangeRates.DataAccess.Abstract;
using HangfireExchangeRates.DataAccess.Concrete.EntityFramework;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddScoped<IRateServiceProvider,FixerIoApiProvider>();

        // ExchangeRatesApiProvider can be implemented 
        //builder.Services.AddScoped<IRateServiceProvider,ExchangeRatesApiProvider>();


        builder.Services.AddScoped<IRateDal, RateDal>();
        builder.Services.AddScoped<ISymDal, SymbolDal>();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();



        // builder.Configuration["Hangfire:Database"]  to be updated below for the same remote db instance 

        builder.Services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSQLiteStorage("../Net6RedisSql/SqLiteDatabase.db", new SQLiteStorageOptions() { AutoVacuumSelected = SQLiteStorageOptions.AutoVacuum.FULL, JobExpirationCheckInterval = TimeSpan.FromSeconds(30) })
                .UseHeartbeatPage(checkInterval: TimeSpan.FromSeconds(30))
                .UseJobsLogger());

        builder.Services.AddHangfireServer();

        var app = builder.Build();

        app.UseHangfireDashboard(string.Empty);

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }


        RecurringJobs.ConsoleTestTask();

        var serviceProvider = builder.Services.BuildServiceProvider();
        var _rateService = serviceProvider.GetService<IRateServiceProvider>();

        RecurringJobs.GetSymbols(_rateService);

        RecurringJobs.GetRates(_rateService);

        //app.UseHttpsRedirection();

        // app.UseAuthorization();

        // app.MapControllers();

        app.Run();
    }
}