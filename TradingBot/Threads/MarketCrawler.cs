using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TradingBot.Containers;
using TradingBot.Modules;

namespace TradingBot.Threads
{
    public class MarketCrawler
    {
        public DateTime LastRun
        {
            get; private set;
        }

        public int CrawlerRan
        {
            get; private set;
        }

        public int DailyCollectionRan
        {
            get; private set;
        }

        public MarketCrawler()
        {
            CrawlerRan = 0;
            DailyCollectionRan = 0;
        }


        public void Run()
        {
            GetPrices priceDownloader = new GetPrices();

            SQL sql = new SQL();

            while (Program.ShouldRun)
            {
                sql.OpenDataBase();

                if ( (DateTime.Now - LastRun).TotalHours > 24)
                {
                    // Gather the available currencies and available markets one per day
                    // as it is very unlikely that they change too often

                    MarketPackage availableMarkets = priceDownloader.GetAvailableMarkets();
                    CurrenciesPackage availableCurrencies = priceDownloader.GetCurrencies();

                    if(availableMarkets.success)
                        sql.AddDailyMarket(availableMarkets.result);

                    if (availableCurrencies.success)
                        sql.AddDailyCurrencyInfo(availableCurrencies.result);


                    LastRun = DateTime.Now;

                    DailyCollectionRan++;
                }

                Market[] markets = sql.GetActiveMarkets();

                foreach(Market market in markets)
                {
                    MarketPricePackage marketPrice = priceDownloader.GetMarketPrice(market.MarketName);

                    if (marketPrice.success)
                        sql.AddNewCurrencyPrice(marketPrice.result, market);
                }

                sql.CloseDataBase();

                CrawlerRan++;

                Thread.Sleep(30 * 1000);
            }
        }
    }
}
