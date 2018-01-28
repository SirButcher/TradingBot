using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.Containers;

namespace TradingBot.Modules
{
    public class SQL
    {
        public OleDbConnection traveldbCon;
        public OleDbCommand cmd;
        public OleDbDataReader reader;

        public static void InitializeSQLConString()
        {
            using (StreamReader sr = new StreamReader("Files/SQL.dat"))
            {
                Program.SQLConString = sr.ReadToEnd();
            }

            if (Program.SQLConString.Length == 0)
                throw new FileNotFoundException("Warning! The SQL.dat is missing - without it the SQL connection couldn't be established!");
        }

        public SQL()
        {
            traveldbCon = new OleDbConnection(Program.SQLConString);

            OpenDataBase();
            cmd = traveldbCon.CreateCommand();
            CloseDataBase();
        }

        #region Utility methods

        public void OpenDataBase()
        {
            if (cmd != null)
                cmd.Parameters.Clear();

            if (reader != null && !reader.IsClosed)
                reader.Close();

            if (traveldbCon.State != System.Data.ConnectionState.Open)
                traveldbCon.Open();
        }
        public void CloseDataBase()
        {
            if (cmd != null)
                cmd.Parameters.Clear();

            if (reader != null && !reader.IsClosed)
                reader.Close();

            if (traveldbCon.State != System.Data.ConnectionState.Closed)
                traveldbCon.Close();
        }

        #endregion


        public void AddDailyMarket(List<Market> marketList)
        {
            foreach(Market data in marketList)
            {
                int hashCode = data.GetHashCode();

                cmd.Parameters.Clear();

                cmd.CommandText = "IF EXISTS (SELECT TOP 1 [ID] FROM [GilGames].[dbo].[Trade_Markets] WHERE UniqueHash = ?) " +
                                    "UPDATE [GilGames].[dbo].[Trade_Markets] SET [MinTradeSize] = ?, [IsActive] =? WHERE UniqueHash = ?; " +
                                  "ELSE " +
                                    "INSERT INTO [GilGames].[dbo].[Trade_Markets] VALUES ( ?, ?, ?, ?, ?, ?, ?, ? );";

                cmd.Parameters.AddWithValue("UniqueHash", hashCode);

                cmd.Parameters.AddWithValue("MinTradeSize", data.MinTradeSize);
                cmd.Parameters.AddWithValue("IsActive", data.IsActive);
                cmd.Parameters.AddWithValue("UniqueHash", hashCode);

                cmd.Parameters.AddWithValue("[UniqueHash]", hashCode);
                cmd.Parameters.AddWithValue("[MarketCurrency]", data.MarketCurrency);
                cmd.Parameters.AddWithValue("[MarketCurrencyLong]", data.MarketCurrencyLong);
                cmd.Parameters.AddWithValue("[BaseCurrency]", data.BaseCurrency);
                cmd.Parameters.AddWithValue("[BaseCurrencyLong]", data.BaseCurrencyLong);
                cmd.Parameters.AddWithValue("[MinTradeSize]", data.MinTradeSize);
                cmd.Parameters.AddWithValue("[MarketName]", data.MarketName);
                cmd.Parameters.AddWithValue("[IsActive]", data.IsActive);

                cmd.ExecuteNonQuery();
            }
        }

        public void AddDailyCurrencyInfo(List<CurrencyInfo> currencyList)
        {
            foreach (CurrencyInfo data in currencyList)
            {
                int hashCode = data.GetHashCode();

                cmd.Parameters.Clear();

                cmd.CommandText = "IF EXISTS (SELECT TOP 1 [ID] FROM [GilGames].[dbo].[Trade_Currencies] WHERE [UniqueHash] = ?) " +
                                    "UPDATE [GilGames].[dbo].[Trade_Currencies] SET [TxFee] = ?, [IsActive] = ? WHERE [UniqueHash] = ?; " +
                                  "ELSE " +
                                    "INSERT INTO [GilGames].[dbo].[Trade_Currencies] VALUES ( ?, ?, ?, ?, ?, ? );";

                cmd.Parameters.AddWithValue("UniqueHash", hashCode);

                cmd.Parameters.AddWithValue("TxFee", data.TxFee);
                cmd.Parameters.AddWithValue("IsActive", data.IsActive);
                cmd.Parameters.AddWithValue("UniqueHash", hashCode);

                cmd.Parameters.AddWithValue("[UniqueHash]", hashCode);
                cmd.Parameters.AddWithValue("[Currency]", data.Currency);
                cmd.Parameters.AddWithValue("[CurrencyLong]", data.CurrencyLong);
                cmd.Parameters.AddWithValue("[Cointype]", data.CoinType);
                cmd.Parameters.AddWithValue("[TxFee]", data.TxFee);
                cmd.Parameters.AddWithValue("[IsActive]", data.IsActive);

                cmd.ExecuteNonQuery();
            }
        }

        public Market[] GetActiveMarkets()
        {
            List<Market> marketList = new List<Market>();

            cmd.Parameters.Clear();

            cmd.CommandText = "SELECT [ID], [MarketCurrency], [MarketCurrencyLong], [MinTradeSize], [MarketName], [BaseCurrency] FROM [GilGames].[dbo].[Trade_Markets] WHERE [IsActive] = 1";
            reader = cmd.ExecuteReader();

            while(reader.Read())
            {
                Market market = new Market();
                market.ID = reader.GetInt32(0);
                market.MarketCurrency = reader.GetString(1);
                market.MarketCurrencyLong = reader.GetString(2);
                market.MinTradeSize = (double)reader.GetDecimal(3);
                market.MarketName = reader.GetString(4);
                market.BaseCurrency = reader.GetString(5);

                marketList.Add(market);
            }

            reader.Close();

            return marketList.ToArray();
        }

        public void AddNewCurrencyPrice(MarketPrice info, Market market)
        {
            cmd.Parameters.Clear();

            cmd.CommandText = "INSERT INTO [GilGames].[dbo].[Trade_Currencies_Prices] VALUES ( ?, ?, ?, ?, ?, ?, GETDATE() )";
            cmd.Parameters.AddWithValue("[MarketID]", market.ID);
            cmd.Parameters.AddWithValue("[BaseCurrency]", market.BaseCurrency);
            cmd.Parameters.AddWithValue("[Currency]", market.MarketCurrency);
            cmd.Parameters.AddWithValue("[Bid]", info.Bid);
            cmd.Parameters.AddWithValue("[Ask]", info.Ask);
            cmd.Parameters.AddWithValue("[Last]", info.Last);

            cmd.ExecuteNonQuery();
        }
    }
}
