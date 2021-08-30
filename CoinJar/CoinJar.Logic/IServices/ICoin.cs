using System;
namespace CoinJar.Logic.IServices
{
    public interface ICoin
    {
        decimal Amount { get; set; }
        decimal Volume { get; set; }
    }
}
