using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TradingBot.Modules;
using TradingBot.Threads;

namespace TradingBot
{
    class Program
    {
        public static volatile bool ShouldRun = true;
        public static string SQLConString = "";

        private static DateTime runningSince;

        private static Thread marketCrawlerThread;


        private static MarketCrawler marketCrawler;

        static void Main(string[] args)
        {
            Console.WriteLine(" ----------- Trade Bot -----------");
            Console.WriteLine();
            Console.WriteLine("Started at " + DateTime.Now.ToString());
            Console.WriteLine();

            Console.WriteLine("Please wait - initializing threads and setting params...");

            runningSince = DateTime.Now;

            Initialize();

            Console.WriteLine("Threads initialized. Application is ready to make you a millionaire!");


            while (ShouldRun)
            {
                HandleCommands();
            }
        }

        private static void Initialize()
        {
            SQL.InitializeSQLConString();

            SQL sql = new SQL();


            marketCrawler = new MarketCrawler();
            marketCrawlerThread = new Thread(new ThreadStart(marketCrawler.Run));


            marketCrawlerThread.Start();
        }

        private static void HandleCommands()
        {
            Console.WriteLine();
            Console.Write("Waiting for command: ");
            string input = Console.ReadLine();

            if (HasCommand(input, "exit", true))
            {
                CloseApplication();
            }
            else if(HasCommand(input, "stat", true))
            {
                Console.WriteLine("Application running since: " + runningSince.ToString());
                Console.WriteLine("Crawler ran: " + marketCrawler.CrawlerRan);
                Console.WriteLine("Daily collector ran: " + marketCrawler.DailyCollectionRan);
                Console.WriteLine("Crawler ran: " + marketCrawler.LastRun);
            }
        }

        private static bool HasCommand(string input, string command, bool equal)
        {
            string f_command = command.Trim().ToLower();
            string f_input = input.Trim().ToLower();

            if (equal)
                return f_command == f_input;
            else
                return f_command.Length <= f_input.Length && f_input.Contains(f_command);
        }



        private static void CloseApplication()
        {
            Console.WriteLine("Closing application - waiting for threads to close, please wait!");

            ShouldRun = false;

            marketCrawlerThread.Join();



            Console.WriteLine("Threads closed. Good bye!");
        }
    }
}
