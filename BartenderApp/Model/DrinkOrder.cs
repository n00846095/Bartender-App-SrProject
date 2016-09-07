using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BartenderApp.Model
{
    public class DrinkOrder
    {
        public int DrinkOrderID { get; set; }
        public int DrinkID { get; set; }
        public DateTime? OrderDateTime { get; set; }
        public int TableNumber { get; set; }
        public string DrinkName { get; set; }
        public string Ingredients { get; set; }
    }
}
