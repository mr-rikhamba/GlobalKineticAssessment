using System;
using System.Linq;
using System.Threading.Tasks;
using CoinJar.Core;
using CoinJar.Logic.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoinJar.Logic.Services
{
    public class CoinJarService : ICoinJarService
    {
        private CoinJarDBContext _ctx;
        private ILogger<CoinJarService> _logger;
        private const decimal MaxVolume = 42;

        public CoinJarService(CoinJarDBContext ctx)
        {
            _ctx = ctx;
        }

        public async Task AddCoin(ICoin coin)
        {
            if (await IsBelowMaxVolume(coin))
            {
                await _ctx.Coin.AddAsync(new Coin
                {
                    Id = Guid.NewGuid(),
                    Amount = coin.Amount,
                    Volume = coin.Volume
                });
                await _ctx.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentOutOfRangeException("Volume", "Maximum coin join volume reached. Please reset jar.");
            }
        }

        public async Task<Decimal> GetTotalAmount()
        {
            return await _ctx.Coin.SumAsync(w => w.Amount);
        }

        public async Task Reset()
        {
            _ctx.Coin.RemoveRange(_ctx.Coin.ToList());
            await _ctx.SaveChangesAsync();
        }

        private async Task<bool> IsBelowMaxVolume(ICoin coin)
        {
            var currentVolume = await _ctx.Coin.SumAsync(w => w.Volume);
            if ((coin.Volume + currentVolume) <= 42)
            {
                return true;
            }
            return false;
        }
    }
}
