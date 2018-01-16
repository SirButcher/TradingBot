using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TradingBot
{
    public class GetPrices
    {
        public void GetAvailablePrices()
        {
            Crypto crypto = new Crypto();

            string apiKey = crypto.GetAPIKey();
            string apisecret = crypto.GetSecretKey();
            string nonce = Helpers.GetUnixTimestamp();
            string uri = "https://bittrex.com/api/v1.1/public/getmarkets";


            string response = GetServerResponse(uri, "");

            return;
        }

        private string GetServerResponse(string URL, string apiSign)
        {
            HttpWebRequest http = (HttpWebRequest)WebRequest.Create(URL);

            if(apiSign.Length > 0)
                http.Headers.Add("apisign:" + apiSign);

            WebResponse response = http.GetResponse();

            Stream stream = response.GetResponseStream();

            StreamReader sr = new StreamReader(stream);

            string content = sr.ReadToEnd();


            return content;
        }
    }
}
