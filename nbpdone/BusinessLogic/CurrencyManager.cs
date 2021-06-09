using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nbpdone.Models;
using System.Net.Http;
using Newtonsoft.Json;
using ExchangeRate.Models;

namespace ExchangeRate.BusinessLogic
{
    public static class CurrencyManager
    {
        /// <summary>
        /// Simple basic check for supported currencies.
        /// </summary>
        /// <param name="currency">Input currency abreviation (e.g. USD).</param>
        /// <returns>If currency is supported or not.</returns>
        public static bool CheckCurrencySupport(string currency)
        {
            var isSupportedCurrency = currency.ToUpper() switch
            {
                "EUR" or "USD" or "CHF" or "GBP" => true,
                _ => false
            };

            return isSupportedCurrency;
        }

        /// <summary>
        /// Get historical exchange rates from Narodowy Bank Polski (NBP) Api.
        /// </summary>
        /// <param name="currency">Input currency abreviation (e.g. USD).</param>
        /// <param name="startDate">Exchange rate historical data start date.</param>
        /// <param name="endDate">Exchange rate historical data end date.</param>
        /// <returns>List of exchange rates from the start to the end date.</returns>
        public async static Task<List<CurrencyExchangeRates>> GetHistoricalExchangeRates(string currency, DateTime startDate, DateTime endDate)
        {
            var client = new HttpClient();
            string response = await client.GetStringAsync($"http://api.nbp.pl/api/exchangerates/rates/c/{currency}/{startDate.ToString("yyyy-MM-dd")}/{endDate.ToString("yyyy-MM-dd")}/?format=json");

            var nbpApiResponse = JsonConvert.DeserializeObject<NBPApiResponse>(response);
            return nbpApiResponse.rates.Select(x => new CurrencyExchangeRates() { buying = x.ask, selling = x.bid }).ToList();
        }
    }
}
