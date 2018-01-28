using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBot.Containers
{
    public class MarketPrice
    {
        public double Bid { get; set; }
        public double Ask { get; set; }
        public double Last { get; set; }
    }

    public class MarketPricePackage
    {
        public bool success { get; set; }
        public string message { get; set; }
        public MarketPrice result { get; set; }
    }
}
