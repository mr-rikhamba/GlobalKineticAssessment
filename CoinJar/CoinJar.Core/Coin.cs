using System;
namespace CoinJar.Core
{
    public class Coin
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public decimal Volume { get; set; }
    }
}
