using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRate.Models
{
    public class CurrencyExchangeRates
    {
        // NBP equivalent json field name: "bid"
        public double buying { get; set; }

        // NBP equivalent json field name: "ask"
        public double selling { get; set; }
    }
}
