using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Models.Opts
{
    public class ApiOptions
    {
        public string Url { get; set; }

        // résilience manuelle
        //public int Delay { get; set; }
        //public int RetryMax { get; set; }
    }
}
