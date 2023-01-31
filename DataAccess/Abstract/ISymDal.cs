using HangfireExchangeRates.Core;
using HangfireExchangeRates.Entities;

namespace HangfireExchangeRates.DataAccess.Abstract
{
    public interface ISymDal : IEntityRepository<Symbol>
    {
            // sp or view operations additional
    }
}