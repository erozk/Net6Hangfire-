using HangfireExchangeRates.Core;
using HangfireExchangeRates.Entities;

namespace HangfireExchangeRates.DataAccess.Abstract
{
    public interface IRateDal : IEntityRepository<Rate>
    {
            // sp or view operations additional
    }
}