using System;
namespace CoinJar.Logic.IServices
{
    public interface ICoinJarService
    {
        void AddCoin(ICoin coin);
        decimal GetTotalAmount();
        void Reset();
    }
}
