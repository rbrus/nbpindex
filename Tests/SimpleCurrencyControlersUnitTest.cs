using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using nbpdone.Controllers;
using nbpdone.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class SimpleCurrencyControlersUnitTest
    {
        [Fact]
        public async Task GetCurrencyIndexAsOfJanuary2013()
        {
            // Act
            ILogger<CurrencyController> logger = null; // ToDo:
            var orderController = new CurrencyController(logger);

            // http://api.nbp.pl/api/exchangerates/rates/c/EUR/2013-01-28/2013-01-31/?format=json
            var actionResult = await orderController.Get("EUR", DateTime.Parse("2013-01-28"), DateTime.Parse("2013-01-31"));

            // Assert
            var viewResult = Assert.IsType<ActionResult<CalculationsResult>>(actionResult);
            Assert.Equal(4.2344, viewResult.Value.average_price);
            Assert.Equal(0.0122, viewResult.Value.standard_deviation);
        }
    }
}
