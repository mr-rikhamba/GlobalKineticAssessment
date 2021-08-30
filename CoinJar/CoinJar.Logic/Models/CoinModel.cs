using System;
using System.ComponentModel.DataAnnotations;
using CoinJar.Logic.IServices;

namespace CoinJar.Logic.Models
{
    public class CoinModel : ICoin
    {
        public Guid Id { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public decimal Volume { get; set; }
    }
}
