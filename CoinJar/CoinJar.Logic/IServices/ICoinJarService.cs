using System;
using System.Threading.Tasks;

namespace CoinJar.Logic.IServices
{
    public interface ICoinJarService
    {
        Task AddCoin(ICoin coin);
        Task<decimal> GetTotalAmount();
        Task Reset();
    }
}
