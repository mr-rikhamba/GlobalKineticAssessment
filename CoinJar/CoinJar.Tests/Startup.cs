using System;
using CoinJar.Core;
using CoinJar.Logic.IServices;
using CoinJar.Logic.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoinJar.Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddDbContext<CoinJarDBContext>(options => options.UseInMemoryDatabase(databaseName: "CoinJarDB"));
            services.AddScoped<ICoinJarService, CoinJarService>();
        }
    }
}
