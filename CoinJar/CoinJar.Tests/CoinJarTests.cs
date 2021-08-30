using System;
using CoinJar.Logic.IServices;
using CoinJar.Logic.Models;
using Xunit;

namespace CoinJar.Tests
{
    public class CoinJarTests
    {
        private readonly ICoinJarService _coinjarService;

        public CoinJarTests(ICoinJarService coinjarService)
        {
            _coinjarService = coinjarService;
        }

        [Theory]
        [InlineData(15, 15)]
        public async void ShouldAddCoin(decimal amount, decimal volume)
        {
            await _coinjarService.Reset();
            await _coinjarService.AddCoin(new CoinModel
            {
                Amount = amount,
                Volume = volume
            });
            await _coinjarService.AddCoin(new CoinModel
            {
                Amount = 20,
                Volume = 13
            });
            var result = await _coinjarService.GetTotalAmount();
            Assert.True(15 < result);
        }

        [Fact]
        public async void ShouldThrowArgumentOutOfRangeException()
        {
            var model = new CoinModel
            {
                Amount = 20,
                Volume = 100
            };
           await Assert.ThrowsAsync<ArgumentOutOfRangeException>( () =>  _coinjarService.AddCoin(model));

        }
        [Theory]
        [InlineData(15, 30)]
        [InlineData(50, 100)]
        public async void ShouldGetAmount(decimal amount, decimal expected)
        {
            await _coinjarService.Reset();
            await _coinjarService.AddCoin(new CoinModel
            {
                Amount = amount * 2,
                Volume = 10
            });
            Assert.Equal(expected, await _coinjarService.GetTotalAmount());
        }

        [Theory]
        [InlineData(10, 10)]
        [InlineData(32, 16)]
        public async void ShouldResetAndEqual0(decimal amount, decimal volume)
        {
            await _coinjarService.Reset();
            await _coinjarService.AddCoin(new CoinModel
            {
                Amount = amount,
                Volume = volume
            });
            Assert.Equal(amount, await _coinjarService.GetTotalAmount());

            await _coinjarService.Reset();
            Assert.Equal(0, await _coinjarService.GetTotalAmount());
        }
    }
}
