using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nbpdone.Models
{
    /// <summary>
    /// NBPApiResult is a model (a type) of Narodowy Bank Polski API return value.
    /// For more info go to: http://api.nbp.pl/
    /// </summary>
    public class NBPApiResponse
    {
        public string table { get; set; }
        public string currency { get; set; }
        public string code { get; set; }
        public List<Rate> rates { get; set; }
    }

    public class Rate
    {
        public string no { get; set; }
        public string effectiveDate { get; set; }
        public double bid { get; set; }
        public double ask { get; set; }
    }
}
