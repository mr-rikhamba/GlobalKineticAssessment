using System;
using Microsoft.EntityFrameworkCore;

namespace CoinJar.Core
{
    public class CoinJarDBContext : DbContext
    {
        public CoinJarDBContext(DbContextOptions<CoinJarDBContext> options)
            : base(options) { }

        public DbSet<Coin> Coin { get; set; }
    }
}
