using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using nbpdone.Models;
using Microsoft.AspNetCore.Http;
using ExchangeRate.BusinessLogic;

namespace nbpdone.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ILogger<CurrencyController> _logger;

        public CurrencyController(ILogger<CurrencyController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// HTTP GET method of:
        /// * "Serwis udostępniający obliczony średni kurs kupna oraz odchylenie standardowe kursów
        /// *  sprzedaży dla podanych danych na podstawie danych z NBP."
        /// </summary>
        /// <param name="currency">Input currency abreviation (e.g. USD).</param>
        /// <param name="startDate">Exchange rate historical data start date.</param>
        /// <param name="endDate">Exchange rate historical data end date.</param>
        /// <returns>Return calculations results including average price of buying rate and standard deviation of selling exchange rate.</returns>
        [HttpGet("/{currency}/{startDate}/{endDate}/")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CalculationsResult>> Get([FromRoute] string currency, [FromRoute] DateTime startDate, [FromRoute] DateTime endDate)
        {
            try
            {
                if (!CurrencyManager.CheckCurrencySupport(currency))
                {
                    var error = new Dictionary<string, string[]>() { {"currency", new string[] { $"The value {currency} is not valid." } }, };
                    return ValidationProblem(new ValidationProblemDetails(error));
                }

                return await Calculations.CurrencyIndex(currency, startDate, endDate);
            } 
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
