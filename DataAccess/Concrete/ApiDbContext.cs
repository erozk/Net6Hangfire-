using Microsoft.EntityFrameworkCore;
using HangfireExchangeRates.DataAccess.Abstract;
using HangfireExchangeRates.Entities;

namespace HangfireExchangeRates.DataAccess
{
    public class ApiDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Data Source=../Net6RedisSql/SqLiteDatabase.db");
        }

        public DbSet<Rate> Rates { get; set; }
        public DbSet<Symbol> Symbols { get; set; }
        
    }
    
}