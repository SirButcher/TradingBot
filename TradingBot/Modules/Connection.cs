using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TradingBot.Modules
{
    public static class Connection
    {
        public static string GetServerResponse(string URL, string apiSign)
        {
            HttpWebRequest http = (HttpWebRequest)WebRequest.Create(URL);

            if (apiSign.Length > 0)
                http.Headers.Add("apisign:" + apiSign);

            WebResponse response = http.GetResponse();

            Stream stream = response.GetResponseStream();

            StreamReader sr = new StreamReader(stream);

            string content = sr.ReadToEnd();

            sr.Close();
            stream.Close();

            response.Close();

            return content;
        }
    }
}
