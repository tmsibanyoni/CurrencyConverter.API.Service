using CurrencyConverter.Repository.Interface;
using CurrencyConverter.Repository.Model;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace CurrencyConverter.API.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController(IDomainManager domainManager) : ControllerBase
    {
        private readonly IDomainManager _domainManager = domainManager;

        /// <summary>
        /// Returns the mount from a base currency to a target currency
        /// </summary>
        /// <returns></returns>
        [HttpGet("convert", Name = "Convert")]
        [ProducesResponseType(200, Type = typeof(CurrencyModel))]
        public async Task<ActionResult> Convert(string @base, string target, double amount)
        {
            var result = await _domainManager.Convert(@base, target,amount);
            return Ok(result);
        }

        /// <summary>
        /// Returns all the Historic Target Convertions
        /// </summary>
        /// <returns></returns>

        [HttpGet("history", Name = "History")]
        [ProducesResponseType(200, Type = typeof(ExchangeRatesResponseModel))]
        public async Task<ActionResult> History()
        {
            var result = await _domainManager.GetHistoryRates();
            return Ok(result);
        }
    }
}
