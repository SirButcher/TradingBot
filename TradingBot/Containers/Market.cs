using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBot.Containers
{
    public class Market
    {
        public int ID { get; set; }

        public string MarketCurrency { get; set; }
        public string BaseCurrency { get; set; }
        public string MarketCurrencyLong { get; set; }
        public string BaseCurrencyLong { get; set; }
        public double MinTradeSize { get; set; }
        public string MarketName { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public string Notice { get; set; }
        public bool? IsSponsored { get; set; }
        public string LogoUrl { get; set; }


        public override int GetHashCode()
        {
            return (MarketCurrency + MarketCurrencyLong + Created.GetHashCode()).GetHashCode();
        }
    }

    public class MarketPackage
    {
        public bool success { get; set; }
        public string message { get; set; }
        public List<Market> result { get; set; }
    }
}
