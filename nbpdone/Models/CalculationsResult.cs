using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nbpdone.Models
{
    /// <summary>
    /// StanardDeviationResult:
    /// * It contains standard deviation value as well everage price (a mean)
    /// * which was used to calculate standard deviation.
    /// </summary>
    [Serializable]
    public class CalculationsResult
    {
        public double standard_deviation { get; set; }
        public double average_price { get; set; }
    }
}
