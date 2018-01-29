using System;
using System.Collections.Generic;
using System.IO;
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

        public static void WriteLog(string message)
        {
            string LogPath = "mLog.txt";

            using (StreamWriter sw = new StreamWriter(LogPath, true))
            {
                sw.Write(DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToLongTimeString() + ": ");
                sw.WriteLine(message);
            }
        }

    }
}
