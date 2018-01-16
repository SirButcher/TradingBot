using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBot
{
    public static class Helpers
    {
        public static string GetUnixTimestamp()
        {
            return (DateTime.UtcNow.Subtract(new DateTime(1970, 01, 01))).TotalSeconds.ToString();
        }
    }
}
