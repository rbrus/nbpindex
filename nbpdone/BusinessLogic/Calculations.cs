using nbpdone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRate.BusinessLogic
{
    /// <summary>
    /// Calculations extensions.

    /// </summary>
    public static class Calculations
    {
        /// <summary>
        /// StandardDeviation calculations.
        /// * To know more about calculating StandardDeviation check:
        /// * https://www.matemaks.pl/odchylenie-standardowe.html
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static double StandardDeviation(List<double> values)
        {
            // Get the mean.
            double mean = values.Sum() / values.Count();

            // Get the sum of the squares of the differences between the values and the mean.
            var sum_of_squares = values.Sum(x => ((x - mean) * (x - mean)));

            // Standard Deviation
            return Math.Sqrt(sum_of_squares / values.Count());
        }

        /// <summary>
        /// CurrencyIndex: the core business logic of the service.
        /// * "Serwis udostępniający obliczony średni kurs kupna oraz odchylenie standardowe kursów
        /// *  sprzedaży dla podanych danych na podstawie danych z NBP."
        /// </summary>
        /// <param name="currency">Input currency abreviation (e.g. USD).</param>
        /// <param name="startDate">Exchange rate historical data start date.</param>
        /// <param name="endDate">Exchange rate historical data end date.</param>
        /// <returns>Return calculations results including average price of buying rate and standard deviation of selling exchange rate.</returns>
        public async static Task<CalculationsResult> CurrencyIndex(string currency, DateTime startDate, DateTime endDate)
        {
            var historicalExchangeRates = await CurrencyManager.GetHistoricalExchangeRates(currency, startDate, endDate);

            // Buying Average of Exchange Rates
            var avgBuyRate = (historicalExchangeRates.Select(x => x.buying).ToList()).Average();

            // Selling StandardDeviation of Exchange Rates
            var stdDevSellRate = Calculations.StandardDeviation(historicalExchangeRates.Select(x => x.selling).ToList());
            
            return new CalculationsResult() { average_price = Math.Round(avgBuyRate, 4), standard_deviation = Math.Round(stdDevSellRate, 4) };
        }
    }
}
