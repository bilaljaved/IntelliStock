using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IntelliStock_WebService
{
    public class DBC
    {
        string conString = null;
        SqlConnection s = null;

        public void getConnection()
        {
            conString = @"C:\USERS\ABDUL_000\DOCUMENTS\VISUAL STUDIO 2013\PROJECTS\INTELLISTOCK_WEBSERVICE\INTELLISTOCK_WEBSERVICE\KSE_DATABASE.MDF";
            s = new SqlConnection(conString);
            s.Open();

        }


        //public bool populate_LSE25_Index_Table(double lse25, double change, double volume, double value , DateTime dt)
        //{
        //    SqlParameter p1 = new SqlParameter("lse25", lse25 );
        //    SqlParameter p2 = new SqlParameter("change", change);
        //    SqlParameter p3 = new SqlParameter("volume", volume);
        //    SqlParameter p4 = new SqlParameter("value", value);
        //    SqlParameter p5 = new SqlParameter("dateTime", dt.Date.ToString("yyyy-MM-dd HH:mm:ss"));
        //    queryForInsert = "Insert into LSE25_Index(DATE , LSE25 , CHANGE , VOLUME , VALUE ) values ( @dateTime , @lse25 ,@change , @volume , @value  ) ";

        //    SqlCommand cmd = new SqlCommand(queryForInsert, s);
        //    cmd.Parameters.Add(p1);
        //    cmd.Parameters.Add(p2);
        //    cmd.Parameters.Add(p3);
        //    cmd.Parameters.Add(p4);
        //    cmd.Parameters.Add(p5);

        //    int i = cmd.ExecuteNonQuery();
        //    if(i == 0)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }

        //}

        //public bool populate_LSE25_Stats_Table(double scrips, double up , double equal, double down, DateTime dt)
        //{
        //    SqlParameter p1 = new SqlParameter("scrips", scrips);
        //    SqlParameter p2 = new SqlParameter("up", up);
        //    SqlParameter p3 = new SqlParameter("equal", equal);
        //    SqlParameter p4 = new SqlParameter("down", down);
        //    SqlParameter p5 = new SqlParameter("dateTime", dt.Date.ToString("yyyy-MM-dd HH:mm:ss"));
        //    queryForInsert = "Insert into LSE25_Stats(DATE ,SCRIPS , UP , EQUAL , DOWN ) values ( @dateTime , @scrips ,@up , @equal , @down  ) ";

        //    SqlCommand cmd = new SqlCommand(queryForInsert, s);
        //    cmd.Parameters.Add(p1);
        //    cmd.Parameters.Add(p2);
        //    cmd.Parameters.Add(p3);
        //    cmd.Parameters.Add(p4);
        //    cmd.Parameters.Add(p5);

        //    int i = cmd.ExecuteNonQuery();
        //    if (i == 0)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}            
        public bool populate_KSE_DATABASE_SUMMARY_Table(int SYMBOL_ID, DateTime DATE, string SYMBOL_NAME, string CATEGORY, double LDCP, double OPEN, double HIGH, double LOW, double CURRENT, double CHANGE, double VOLUME)
        {
            getConnection();
            SqlParameter p1 = new SqlParameter("symbol", SYMBOL_ID);
            SqlParameter p2 = new SqlParameter("dateTime", DATE.ToString("yyyy-MM-dd HH:mm:ss"));
            SqlParameter p3 = new SqlParameter("symbolName", SYMBOL_NAME);
            SqlParameter p4 = new SqlParameter("category", CATEGORY);
            SqlParameter p5 = new SqlParameter("ldcp", LDCP);
            SqlParameter p6 = new SqlParameter("open", OPEN);
            SqlParameter p7 = new SqlParameter("high", HIGH);
            SqlParameter p8 = new SqlParameter("low", LOW);
            SqlParameter p9 = new SqlParameter("current", CURRENT);
            SqlParameter p10 = new SqlParameter("change", CHANGE);
            SqlParameter p11 = new SqlParameter("volume", VOLUME);

            string queryForInsert = "Insert into SUMMARY(SYMBOL_ID , DATE , SYMBOL_NAME , SYMBOL_CATEGORY , SYMBOL_LDCP , SYMBOL_OPEN , SYMBOL_HIGH , SYMBOL_LOW , SYMBOL_CURRENT , SYMBOL_CHANGE, SYMBOL_VOLUME ) values (@symbol , @dateTime , @symbolName , @category , @ldcp , @open , @high , @low , @current , @change ,@volume ) ";

            SqlCommand cmd = new SqlCommand(queryForInsert, s);

            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
            cmd.Parameters.Add(p5);
            cmd.Parameters.Add(p6);
            cmd.Parameters.Add(p7);
            cmd.Parameters.Add(p8);
            cmd.Parameters.Add(p9);
            cmd.Parameters.Add(p10);
            cmd.Parameters.Add(p11);
            int i = cmd.ExecuteNonQuery();
            if (i == 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        public bool insertForRow1IntoDBC(string marketStatus, double totalAdvanced, double KSE100_Current, double AllShare_Current, double KSE30_Current, double KMI30_Current, DateTime dateTime)
        {
            getConnection();
            SqlParameter p1 = new SqlParameter("marketStatus", marketStatus);
            SqlParameter p2 = new SqlParameter("totalAdvanced", totalAdvanced);
            SqlParameter p3 = new SqlParameter("kse100current", AllShare_Current);
            SqlParameter p4 = new SqlParameter("allsharecurrent", AllShare_Current);
            SqlParameter p5 = new SqlParameter("kse30current", KSE30_Current);
            SqlParameter p6 = new SqlParameter("kmi30current", KMI30_Current);

            SqlParameter p7 = new SqlParameter("dateTime", dateTime.ToString("yyyy-MM-dd HH:mm:ss"));

            string queryForInsert = "Insert into ACTUAL_SUMMARY(MARKET_STATUS , SYMBOLS_ADVANCED , KSE100_CURRENT , ALLSHARE_CURRENT , KSE30_CURRENT , KMI30_CURRENT , DATE ) values (@marketStatus , @totalAdvanced , @kse100current ,@allsharecurrent , @kse30current, @kmi30current , @dateTime ) ";

            SqlCommand cmd = new SqlCommand(queryForInsert, s);

            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
            cmd.Parameters.Add(p5);
            cmd.Parameters.Add(p6);
            cmd.Parameters.Add(p7);

            int i = cmd.ExecuteNonQuery();
            if (i == 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        public bool insertForRow2IntoDBC(double marketVolume, double totalDeclined, double KSE100_High, double AllShare_High, double KSE30_High, double KMI30_High, DateTime dateTime)
        {
            getConnection();
            SqlParameter p1 = new SqlParameter("marketVolume", marketVolume);
            SqlParameter p2 = new SqlParameter("totalDeclined", totalDeclined);
            SqlParameter p3 = new SqlParameter("KSE100_High", KSE100_High);
            SqlParameter p4 = new SqlParameter("AllShare_High", AllShare_High);
            SqlParameter p5 = new SqlParameter("KSE30_High", KSE30_High);
            SqlParameter p6 = new SqlParameter("KMI30_High", KMI30_High);

            SqlParameter p7 = new SqlParameter("dateTime", dateTime.ToString("yyyy-MM-dd HH:mm:ss"));

            string queryForUpdate = "UPDATE ACTUAL_SUMMARY SET MARKET_VOLUME=@marketVolume , SYMBOLS_DECLINED=@totalDeclined , KSE100_HIGH=@KSE100_HIGH , ALLSHARE_HIGH=@AllShare_High , KSE30_HIGH= @KSE30_High , KMI30_HIGH=@KMI30_High , DATE= @dateTime WHERE MARKET_STATUS IS NOT NULL";
            SqlCommand cmd = new SqlCommand(queryForUpdate, s);

            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
            cmd.Parameters.Add(p5);
            cmd.Parameters.Add(p6);
            cmd.Parameters.Add(p7);

            int i = cmd.ExecuteNonQuery();
            if (i == 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        public bool insertForRow3IntoDBC(double marketValue, double totalUnchanged, double KSE100_Low, double AllShare_Low, double KSE30_Low, double KMI30_Low, DateTime dateTime)
        {
            getConnection();
            SqlParameter p1 = new SqlParameter("marketValue", marketValue);
            SqlParameter p2 = new SqlParameter("totalUnchanged", totalUnchanged);
            SqlParameter p3 = new SqlParameter("KSE100_Low", KSE100_Low);
            SqlParameter p4 = new SqlParameter("AllShare_Low", AllShare_Low);
            SqlParameter p5 = new SqlParameter("KSE30_Low", KSE30_Low);
            SqlParameter p6 = new SqlParameter("KMI30_Low", KMI30_Low);

            SqlParameter p7 = new SqlParameter("dateTime", dateTime.ToString("yyyy-MM-dd HH:mm:ss"));

            string queryForUpdate = "UPDATE ACTUAL_SUMMARY SET MARKET_VALUE=@marketValue , SYMBOLS_UNCHANGED=@totalUnchanged , KSE100_LOW=@KSE100_Low , ALLSHARE_LOW=@AllShare_Low  , KSE30_LOW=@KSE30_Low , KMI30_LOW=@KMI30_Low , DATE=@dateTime WHERE MARKET_STATUS IS NOT NULL ";

            SqlCommand cmd = new SqlCommand(queryForUpdate, s);

            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
            cmd.Parameters.Add(p5);
            cmd.Parameters.Add(p6);
            cmd.Parameters.Add(p7);

            int i = cmd.ExecuteNonQuery();
            if (i == 0)
            {
                return false;
            }
            else
            {
                return true;
            }



        }
        public bool insertForRow4IntoDBC(double marketTrades, double total, double KSE100_Change, double AllShare_Change, double KSE30_Change, double KMI30_Change, DateTime dateTime)
        {
            getConnection();
            SqlParameter p1 = new SqlParameter("marketTrades", marketTrades);
            SqlParameter p2 = new SqlParameter("total", total);
            SqlParameter p3 = new SqlParameter("KSE100_Change", KSE100_Change);
            SqlParameter p4 = new SqlParameter("AllShare_Change", AllShare_Change);
            SqlParameter p5 = new SqlParameter("KSE30_Change", KSE30_Change);
            SqlParameter p6 = new SqlParameter("KMI30_Change", KMI30_Change);

            SqlParameter p7 = new SqlParameter("dateTime", dateTime.ToString("yyyy-MM-dd HH:mm:ss"));

            string queryForUpdate = "UPDATE ACTUAL_SUMMARY SET MARKET_TRADES=@marketTrades , SYMBOLS_TOTAL=@total , KSE100_CHANGE=@KSE100_Change, ALLSHARE_CHANGE=@AllShare_Change , KSE30_CHANGE=@KSE30_Change , KMI30_CHANGE=@KMI30_Change , DATE=@dateTime where MARKET_STATUS IS NOT NULL ";

            SqlCommand cmd = new SqlCommand(queryForUpdate, s);

            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
            cmd.Parameters.Add(p5);
            cmd.Parameters.Add(p6);
            cmd.Parameters.Add(p7);

            int i = cmd.ExecuteNonQuery();
            if (i == 0)
            {
                return false;
            }
            else
            {
                return true;
            }


        }
        //public bool populate_LSE25_Stats_Table(double scrips, double up, double equal, double down, DateTime dt)
        //{
        //    SqlParameter p1 = new SqlParameter("scrips", scrips);
        //    SqlParameter p2 = new SqlParameter("up", up);
        //    SqlParameter p3 = new SqlParameter("equal", equal);
        //    SqlParameter p4 = new SqlParameter("down", down);
        //    SqlParameter p5 = new SqlParameter("dateTime", dt.Date.ToString("yyyy-MM-dd HH:mm:ss"));
        //    queryForInsert = "Insert into LSE25_Stats(DATE ,SCRIPS , UP , EQUAL , DOWN ) values ( @dateTime , @scrips ,@up , @equal , @down  ) ";

        //    SqlCommand cmd = new SqlCommand(queryForInsert, s);
        //    cmd.Parameters.Add(p1);
        //    cmd.Parameters.Add(p2);
        //    cmd.Parameters.Add(p3);
        //    cmd.Parameters.Add(p4);
        //    cmd.Parameters.Add(p5);

        //    int i = cmd.ExecuteNonQuery();
        //    if (i == 0)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}

    }
}