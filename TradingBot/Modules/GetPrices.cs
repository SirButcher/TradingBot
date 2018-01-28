using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TradingBot.Containers;

namespace TradingBot.Modules
{
    public class GetPrices
    {
        Crypto crypto;

        public GetPrices()
        {
            crypto = new Crypto();
        }

        public MarketPackage GetAvailableMarkets()
        {
            string uri = "https://bittrex.com/api/v1.1/public/getmarkets";


            string response = Connection.GetServerResponse(uri, "");

            return JsonConvert.DeserializeObject<MarketPackage>(response);
        }

        public CurrenciesPackage GetCurrencies()
        {
            string uri = "https://bittrex.com/api/v1.1/public/getcurrencies";


            string response = Connection.GetServerResponse(uri, "");

            return JsonConvert.DeserializeObject<CurrenciesPackage>(response);
        }

        public MarketPricePackage GetMarketPrice(string marketName)
        {
            string uri = "https://bittrex.com/api/v1.1/public/getticker?market=" + marketName;


            string response = Connection.GetServerResponse(uri, "");

            return JsonConvert.DeserializeObject<MarketPricePackage>(response);
        }

    }
}
