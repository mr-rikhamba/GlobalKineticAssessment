using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoinJar.Logic.IServices;
using CoinJar.Logic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoinJar.Api.Controllers
{
    [Route("api/[controller]")]
    public class CoinJarController : Controller
    {
        private readonly ICoinJarService _coinJarService;
        private readonly ILogger<CoinJarController> _logger;

        public CoinJarController(ICoinJarService coinJarService, ILogger<CoinJarController> logger)
        {
            _coinJarService = coinJarService;
            _logger = logger;
        }

        [HttpGet("GetTotalAmount")]
        public IActionResult GetTotalAmount()
        {
            try
            {
                _logger.LogInformation("Begin fetching amount.");
               var totalAmount = _coinJarService.GetTotalAmount();
                _logger.LogInformation("Completed fetching amount.");
                return Ok(totalAmount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get total amount.");
                return BadRequest("Failed to get total amount.");
            }
        }

        // POST api/values
        [HttpPost("AddCoin")]
        public IActionResult Post([FromBody] CoinModel coinModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Begin addding coin.");
                    _coinJarService.AddCoin(coinModel);
                    _logger.LogInformation("Completed adding coin.");
                    return Ok();
                }
                else
                {
                    _logger.LogWarning("Invalid coin jar model.");
                    return BadRequest("Please enter a valid amount and volume.");
                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Failed to add coin to jar. Please try again later.");
                return BadRequest("Failed to add coin to jar. Please try again later.");
            }
        }

        // PUT api/values/5
        [HttpPut("Reset")]
        public IActionResult Put()
        {
            try
            {
                _logger.LogInformation("Begin Coin Jar Reset.");
                _coinJarService.Reset();
                _logger.LogInformation("Completed Coin Jar Reset.");
                return Ok("Coin jar reset successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to reset coin jar. Please try again later.");
                return BadRequest("Failed to reset coin jar. Please try again later.");
            }
 
        }
    }
}
