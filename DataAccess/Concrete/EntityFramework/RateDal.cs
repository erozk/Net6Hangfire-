using HangfireExchangeRates.Abstract.Orm;
using HangfireExchangeRates.DataAccess;
using HangfireExchangeRates.DataAccess.Abstract;
using HangfireExchangeRates.Entities;

namespace HangfireExchangeRates.DataAccess.Concrete.EntityFramework
{
    public class RateDal : EntityFrameworkEntityRepositoryBase<Rate, ApiDbContext>, IRateDal
    {
        
    }
}