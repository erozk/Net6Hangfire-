# Hangfire DI .Net 6 Application 


* ORMs, Rate Apis and Databases can be immplemented optionally 

    - In the project > Entity Framework Core, FixerIO Api and Sqlite were used


* Important Notes : 

    In Program.cs > UseSQLiteStorage "../Net6RedisSql/SqLiteDatabase.db"

    In DataAccess > ApiDbContext > UseSQLite as "../Net6RedisSql/SqLiteDatabase.db"

    were set due to match the same instance on local development with Net6RedisSql project together

    -------------------------------------------

    Migration can be used from Net6RedisSql project. So this project will inherit tables directly

    -------------------------------------------

    * FixerIO were used but ExchangeRate can be also implemented via dependency injection.

    * Rate Service were set for each 20 min. 
    
    * GetSymbols should run as monthly.

    public static void GetRates(IRateServiceProvider service)
        {
            RateProviderTask task = new(service);

            RecurringJob.AddOrUpdate(() => task.GetRates(), Cron.MinuteInterval(30));
        }

    public static void GetSymbols(IRateServiceProvider service)
        {
            RateProviderTask task = new(service);

            RecurringJob.AddOrUpdate(() => task.GetSymbols(), Cron.Monthly);
        }
    