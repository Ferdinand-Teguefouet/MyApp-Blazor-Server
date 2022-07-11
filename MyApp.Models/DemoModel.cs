using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Models
{
    public enum TrancheAge
    {
        Bébé,
        Enfant,
        Adolescent,
        Adulte,
        Senior
    }
    public class DemoModel
    {
        public TrancheAge Tranche { get; set; }
    }
}
