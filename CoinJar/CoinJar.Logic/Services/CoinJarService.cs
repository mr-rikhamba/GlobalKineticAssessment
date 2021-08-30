using System;
using System.Linq;
using System.Threading.Tasks;
using CoinJar.Core;
using CoinJar.Logic.IServices;
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

        public async void AddCoin(ICoin coin)
        {
            if (IsBelowMaxVolum(coin))
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
                throw new Exception("Maximum coin join volume reached. Please reset jar.");
            }
        }

        public decimal GetTotalAmount()
        {
            return _ctx.Coin.Sum(w => w.Amount);
        }

        public async void Reset()
        {
            _ctx.Coin.RemoveRange(_ctx.Coin.ToList());
            await _ctx.SaveChangesAsync();
        }

        private bool IsBelowMaxVolum(ICoin coin)
        {
            var currentVolume = _ctx.Coin.Sum(w => w.Volume);
            if ((coin.Volume + currentVolume) <= 42)
            {
                return true;
            }
            return false;
        }
    }
}
