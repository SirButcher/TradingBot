using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBot.Containers
{
    public class CurrencyInfo
    {
        public string Currency { get; set; }
        public string CurrencyLong { get; set; }
        public int MinConfirmation { get; set; }
        public double TxFee { get; set; }
        public bool IsActive { get; set; }
        public string CoinType { get; set; }
        public string BaseAddress { get; set; }
        public string Notice { get; set; }

        public override int GetHashCode()
        {
            return (Currency + CurrencyLong + CoinType).GetHashCode();
        }
    }

    public class CurrenciesPackage
    {
        public bool success { get; set; }
        public string message { get; set; }
        public List<CurrencyInfo> result { get; set; }
    }
}
