using HtmlAgilityPack;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IntelliStock_WebService
{
    public class Web_Scraper : IJob
    {

        public string test { get; set; }
        string category { get; set; }



        public List<Company> companies = new List<Company>();
        public Market_Summary Market_Summary = new Market_Summary();

      
        
        
        
        public DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable actual_summary = new DataTable();
        DataTable summary = new DataTable();
  
    //    KSE_Database_In_Memory_Dataset k = new KSE_Database_In_Memory_Dataset();
        

        /// <summary>
        /// Variables for Market Summary.
        /// </summary>

        public Web_Scraper()
        {
            // creating in-memory DBs , initializing...

            DataTable system_prediction = new DataTable();
            DataTable user_prediction = new DataTable();
            DataTable user_status = new DataTable();
            DataTable users = new DataTable();          


           

            //ds.Tables.Add(system_prediction);
            //ds.Tables.Add(user_prediction);
            //ds.Tables.Add(user_status);
            //ds.Tables.Add(users);

            // creating in-memory DB.

            DataColumn ms = new DataColumn("MARKET_STATUS", typeof(string));
            DataColumn mv = new DataColumn("MARKET_VOLUME", typeof(double));
            DataColumn mvalue = new DataColumn("MARKET_VALUE", typeof(double));
            DataColumn mt = new DataColumn("MARKET_TRADES", typeof(double));
            DataColumn sa = new DataColumn("SYMBOLS_ADVANCED", typeof(double));
            DataColumn sd = new DataColumn("SYMBOLS_DECLINED", typeof(double));
            DataColumn su = new DataColumn("SYMBOLS_UNCHANGED", typeof(double));
            DataColumn st = new DataColumn("SYMBOLS_TOTAL", typeof(double));
            DataColumn kse100current = new DataColumn("KSE100_CURRENT", typeof(double));
            DataColumn kse100high = new DataColumn("KSE100_HIGH", typeof(double));
            DataColumn kse100low = new DataColumn("KSE100_LOW", typeof(double));
            DataColumn kse100change = new DataColumn("KSE100_CHANGE", typeof(double));
            DataColumn allsharecurrent = new DataColumn("ALLSHARE_CURRENT", typeof(double));
            DataColumn allsharehigh = new DataColumn("ALLSHARE_HIGH", typeof(double));
            DataColumn allsharelow = new DataColumn("ALLSHARE_LOW", typeof(double));
            DataColumn allsharechange = new DataColumn("ALLSHARE_CHANGE", typeof(double));
            DataColumn kse30current = new DataColumn("KSE30_CURRENT", typeof(double));
            DataColumn kse30high = new DataColumn("KSE30_HIGH", typeof(double));
            DataColumn kse30low = new DataColumn("KSE30_LOW", typeof(double));
            DataColumn kse30change = new DataColumn("KSE30_CHANGE", typeof(double));
            DataColumn kmi30current = new DataColumn("KMI30_CURRENT", typeof(double));
            DataColumn kmi30high = new DataColumn("KMI30_HIGH", typeof(double));
            DataColumn kmi30low = new DataColumn("KMI30_LOW", typeof(double));
            DataColumn kmi30change = new DataColumn("KMI30_CHANGE", typeof(double));
            DataColumn date = new DataColumn("DATE", typeof(DateTime));

            actual_summary.Columns.Add(ms);
            actual_summary.Columns.Add(mv);
            actual_summary.Columns.Add(mt);
            actual_summary.Columns.Add(mvalue);
            actual_summary.Columns.Add(sa);
            actual_summary.Columns.Add(sd);
            actual_summary.Columns.Add(su);
            actual_summary.Columns.Add(st);
            actual_summary.Columns.Add(kse100current);
            actual_summary.Columns.Add(kse100high);
            actual_summary.Columns.Add(kse100low);
            actual_summary.Columns.Add(kse100change);
            actual_summary.Columns.Add(allsharecurrent);
            actual_summary.Columns.Add(allsharehigh);
            actual_summary.Columns.Add(allsharelow);
            actual_summary.Columns.Add(allsharechange);
            actual_summary.Columns.Add(kse30current);
            actual_summary.Columns.Add(kse30high);
            actual_summary.Columns.Add(kse30low);
            actual_summary.Columns.Add(kse30change);
            actual_summary.Columns.Add(kmi30current);
            actual_summary.Columns.Add(kmi30high);
            actual_summary.Columns.Add(kmi30low);
            actual_summary.Columns.Add(kmi30change);
            actual_summary.Columns.Add(date);


            ds.Tables.Add(actual_summary);

            DataColumn si = new DataColumn("SYMBOL_ID", typeof(int));
            DataColumn date1 = new DataColumn("DATE", typeof(string));
            DataColumn sname = new DataColumn("SYMBOL_NAME", typeof(string));
            DataColumn sc = new DataColumn("SYMBOL_CATEGORY", typeof(string));
            DataColumn sldcp = new DataColumn("SYMBOL_LDCP", typeof(string));
            DataColumn sopen = new DataColumn("SYMBOL_OPEN", typeof(string));
            DataColumn sh = new DataColumn("SYMBOL_HIGH", typeof(string));
            DataColumn sl = new DataColumn("SYMBOL_LOW", typeof(string));
            DataColumn scurrent = new DataColumn("SYMBOL_CURRENT", typeof(string));
            DataColumn schange = new DataColumn("SYMBOL_CHANGE", typeof(string));
            DataColumn svolume = new DataColumn("SYMBOL_VOLUME", typeof(string));

            summary.Columns.Add(si);
            summary.Columns.Add(date1);
            summary.Columns.Add(sname);
            summary.Columns.Add(sc);
            summary.Columns.Add(sldcp);
            summary.Columns.Add(sopen);
            summary.Columns.Add(sh);
            summary.Columns.Add(sl);
            summary.Columns.Add(scurrent);
            summary.Columns.Add(schange);
            summary.Columns.Add(svolume);

            ds.Tables.Add(summary);


        }
        public DateTime currentDateTime { get; set; }

        DBC db = new DBC();

        // For scheduling...
        public void Execute(IJobExecutionContext context)
        {

            //do your action here.

            startScrapping();
        }

        public void startScrapping() 
        {            
           
            currentDateTime = DateTime.Now;
            HtmlWeb webGet;
            HtmlDocument document = new HtmlDocument();
            //     string linkToLSE = "http://www.lse.com.pk/Markets/MarketSummary.aspx" ;
            string linkToKSE = "http://www.kse.com.pk/phps/mktSummary.php";
            try
            {

                webGet = new HtmlWeb();
                document = webGet.Load(linkToKSE);

                ///////////// KSE data.. //////                            
                //int total_tables = 32;
                //int total_companies = 350;

                int total_tables = 5;
                int total_companies = 54;
               
                try
                {
                    for (int TABLE_NUM = 1; TABLE_NUM <= total_tables; TABLE_NUM++)
                    {
                        for (int ROW_NUM = 1; ROW_NUM <= total_companies; ROW_NUM++)
                        {
                            HtmlNode table = document.DocumentNode.SelectSingleNode("//table[" + TABLE_NUM + "]//tr[" + ROW_NUM + "]");
                           
                            if (TABLE_NUM == 2)     //Market summary..
                            {
                                if (ROW_NUM == 2)
                                {
                                    
                                    string[] arr = table.InnerText.Split('\n');
                                    Market_Summary.marketStatus = arr[3].Trim();
                                    Market_Summary.total_Advanced = double.Parse(arr[9].Trim());
                                    Market_Summary.current_KSE100 = double.Parse(arr[15].Trim());
                                    Market_Summary.current_AllShare = double.Parse(arr[21].Trim());
                                    Market_Summary.current_KSE30 = double.Parse(arr[27].Trim());
                                    Market_Summary.current_KMI30 = double.Parse(arr[33].Trim());
                                          
                                    // Uncomment it when you want to add summary into original DB.
                                   
                                    //      insertForRow1(Market_Summary.marketStatus, Market_Summary.total_Advanced, Market_Summary.current_KSE100, Market_Summary.current_AllShare, Market_Summary.current_KSE30, Market_Summary.current_KMI30);

                                }
                                else if (ROW_NUM == 3)
                                {
                                    string[] arr = table.InnerText.Split('\n');

                                    Market_Summary.market_Volume = double.Parse(arr[3].Trim());
                                    Market_Summary.total_Declined = double.Parse(arr[9].Trim());
                                    Market_Summary.high_KSE100 = double.Parse(arr[15].Trim());
                                    Market_Summary.high_AllShare = double.Parse(arr[21].Trim());
                                    Market_Summary.high_KSE30 = double.Parse(arr[27].Trim());
                                    Market_Summary.high_KMI30 = double.Parse(arr[33].Trim());

                                    // Uncomment it when you want to add summary into original DB.
                                    //    insertForRow2(Market_Summary.market_Volume , Market_Summary.total_Declined , Market_Summary.high_KSE100 , Market_Summary.high_AllShare , Market_Summary.high_KSE30 , Market_Summary.high_KMI30);

                                }
                                else if (ROW_NUM == 4)
                                {
                                    string[] arr = table.InnerText.Split('\n');

                                    Market_Summary.market_Value = double.Parse(arr[3].Trim());
                                    Market_Summary.total_Unchanged = double.Parse(arr[9].Trim());
                                    Market_Summary.low_KSE100 = double.Parse(arr[15].Trim());
                                    Market_Summary.low_AllShare = double.Parse(arr[21].Trim());
                                    Market_Summary.low_KSE30 = double.Parse(arr[27].Trim());
                                    Market_Summary.low_KMI30 = double.Parse(arr[33].Trim());

                                    // Uncomment it when you want to add summary into original DB.

                                    //    insertForRow3(Market_Summary.market_Value, Market_Summary.total_Unchanged, Market_Summary.low_KSE100, Market_Summary.low_AllShare, Market_Summary.low_KSE30, Market_Summary.low_KMI30);

                                }
                                else if (ROW_NUM == 5)
                                {
                                    string[] arr = table.InnerText.Split('\n');

                                    Market_Summary.market_Trades = double.Parse(arr[3].Trim());
                                    Market_Summary.total = double.Parse(arr[9].Trim());
                                    Market_Summary.change_KSE100 = double.Parse(arr[16].Trim());
                                    Market_Summary.change_AllShare = double.Parse(arr[23].Trim());
                                    Market_Summary.change_KSE30 = double.Parse(arr[30].Trim());
                                    Market_Summary.change_KMI30 = double.Parse(arr[37].Trim());

                                    // Uncomment it when you want to add summary into original DB.

                                    //     insertForRow4(Market_Summary.market_Trades, Market_Summary.total, Market_Summary.change_KSE100, Market_Summary.change_AllShare, Market_Summary.change_KSE30, Market_Summary.change_KMI30);
                                   
                                }



                                //}
                                //else if (TABLE_NUM == 2)        //Chemicals.
                                //{
                                //    if (ROW_NUM == 1)
                                //    {
                                //        category = table.InnerText.Trim();
                                //    }
                                //    else if (ROW_NUM == 3)
                                //    {
                                //        InsertInDB(table, 14);
                                //    }
                                //    else if (ROW_NUM == 4)
                                //    {
                                //        InsertInDB(table, 15);
                                //    }
                                //    else if (ROW_NUM == 5)
                                //    {
                                //        InsertInDB(table, 16);
                                //    }
                                //    else if (ROW_NUM == 6)
                                //    {
                                //        InsertInDB(table, 17);
                                //    }
                                //    else if (ROW_NUM == 7)
                                //    {
                                //        InsertInDB(table, 18);
                                //    }
                                //    else if (ROW_NUM == 8)
                                //    {
                                //        InsertInDB(table, 19);
                                //    }
                                //    else if (ROW_NUM == 9)
                                //    {
                                //        InsertInDB(table, 20);
                                //    }
                                //    else if (ROW_NUM == 10)
                                //    {
                                //        InsertInDB(table, 21);
                                //    }
                                //    else if (ROW_NUM == 11)
                                //    {
                                //        InsertInDB(table, 22);
                                //    }
                                //    else if (ROW_NUM == 12)
                                //    {
                                //        InsertInDB(table, 23);
                                //    }
                                //    else if (ROW_NUM == 13)
                                //    {
                                //        InsertInDB(table, 24);
                                //    }
                                //    else if (ROW_NUM == 14)
                                //    {
                                //        InsertInDB(table, 25);
                                //    }
                                //    else if (ROW_NUM == 15)
                                //    {
                                //        InsertInDB(table, 26);
                                //    }
                                //    else if (ROW_NUM == 16)
                                //    {
                                //        InsertInDB(table, 27);
                                //    }
                                //    else if (ROW_NUM == 17)
                                //    {
                                //        InsertInDB(table, 28);
                                //    }
                                //    else if (ROW_NUM == 18)
                                //    {
                                //        InsertInDB(table, 29);
                                //    }
                                //    else if (ROW_NUM == 19)
                                //    {
                                //        InsertInDB(table, 30);
                                //    }
                                //    else if (ROW_NUM == 20)
                                //    {
                                //        InsertInDB(table, 31);
                                //    }

                                //    else if (ROW_NUM == 21)
                                //    {
                                //        InsertInDB(table, 32);
                                //    }
                                //    else if (ROW_NUM == 22)
                                //    {
                                //        InsertInDB(table, 33);
                                //    }
                                //    else if (ROW_NUM == 23)
                                //    {
                                //        InsertInDB(table, 34);
                                //    }
                                //    else if (ROW_NUM == 24)
                                //    {
                                //        InsertInDB(table, 35);
                                //    }
                                //    else if (ROW_NUM == 25)
                                //    {
                                //        InsertInDB(table, 36);
                                //    }
                                //    else if (ROW_NUM == 26)
                                //    {
                                //        InsertInDB(table, 37);
                                //    }
                                //    else if (ROW_NUM == 27)
                                //    {
                                //        InsertInDB(table, 38);
                                //    }
                                //    else if (ROW_NUM == 28)
                                //    {
                                //        InsertInDB(table, 39);
                                //    }
                                //    else if (ROW_NUM == 29)
                                //    {
                                //        InsertInDB(table, 40);
                                //    }
                                //    else if (ROW_NUM == 30)
                                //    {
                                //        InsertInDB(table, 41);
                                //    }
                                //    else if (ROW_NUM == 31)
                                //    {
                                //        InsertInDB(table, 42);
                                //    }
                                //    else if (ROW_NUM == 32)
                                //    {
                                //        InsertInDB(table, 43);
                                //    }
                                //    else if (ROW_NUM == 33)
                                //    {
                                //        InsertInDB(table, 44);
                                //    }



                            }

                            else if (TABLE_NUM == 3)            // Forestry (Paper and Board)
                            {


                                if (ROW_NUM == 1)
                                {
                                    category = table.InnerText.Trim();
                                }
                                else if (ROW_NUM == 3)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 45);
                                }
                                else if (ROW_NUM == 4)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 46);
                                }
                                else if (ROW_NUM == 5)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 47);



                                }

                            }
                            else if (TABLE_NUM == 4)           // Industrial metals and Mining
                            {
                                if (ROW_NUM == 1)
                                {
                                    category = table.InnerText.Trim();
                                }
                                else if (ROW_NUM == 3)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 48);
                                }
                                else if (ROW_NUM == 4)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 49);
                                }
                                else if (ROW_NUM == 5)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 50);
                                }
                                else if (ROW_NUM == 6)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 51);
                                }
                                else if (ROW_NUM == 7)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 52);
                                }
                                else if (ROW_NUM == 8)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 53);
                                }
                                else if (ROW_NUM == 9)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 54);
                                }
                                else if (ROW_NUM == 10)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 55);
                                }



                            }
                            else if (TABLE_NUM == 5)              //  Construction and Materials (Cement)
                            {
                                if (ROW_NUM == 1)
                                {
                                    category = table.InnerText.Trim();
                                }
                                else if (ROW_NUM == 3)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 56);
                                }
                                else if (ROW_NUM == 4)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 57);
                                }
                                else if (ROW_NUM == 5)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 58);
                                }
                                else if (ROW_NUM == 6)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 59);
                                }
                                else if (ROW_NUM == 7)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 60);
                                }
                                else if (ROW_NUM == 8)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 61);
                                }
                                else if (ROW_NUM == 9)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 62);
                                }
                                else if (ROW_NUM == 10)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 63);
                                }
                                else if (ROW_NUM == 11)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 64);
                                }
                                else if (ROW_NUM == 12)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 65);
                                }
                                else if (ROW_NUM == 13)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 66);
                                }
                                else if (ROW_NUM == 14)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 67);
                                }
                                else if (ROW_NUM == 15)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 68);
                                }
                                else if (ROW_NUM == 16)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 69);
                                }
                                else if (ROW_NUM == 17)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 70);
                                }
                                else if (ROW_NUM == 18)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 71);
                                }
                                else if (ROW_NUM == 19)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 72);
                                }
                                else if (ROW_NUM == 20)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 73);
                                }

                                else if (ROW_NUM == 21)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 74);
                                }
                                else if (ROW_NUM == 22)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 75);
                                }
                                else if (ROW_NUM == 23)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 76);
                                }
                                else if (ROW_NUM == 24)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 77);
                                }
                                else if (ROW_NUM == 25)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 78);
                                }
                                else if (ROW_NUM == 26)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 79);
                                }
                                else if (ROW_NUM == 27)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 80);
                                }
                                else if (ROW_NUM == 28)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 81);
                                }
                                else if (ROW_NUM == 29)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 82);
                                }
                                else if (ROW_NUM == 30)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 83);
                                }
                                else if (ROW_NUM == 31)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 84);
                                }

                            }
                            else if (TABLE_NUM == 6)                // General Industrials
                            {
                                if (ROW_NUM == 1)
                                {
                                    category = table.InnerText.Trim();
                                }
                                else if (ROW_NUM == 3)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 85);
                                }
                                else if (ROW_NUM == 4)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 86);
                                }
                                else if (ROW_NUM == 5)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 87);
                                }
                                else if (ROW_NUM == 6)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 88);
                                }
                                else if (ROW_NUM == 7)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 89);
                                }
                                else if (ROW_NUM == 8)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 90);
                                }
                                else if (ROW_NUM == 9)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 91);
                                }
                                else if (ROW_NUM == 10)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 92);
                                }
                                else if (ROW_NUM == 11)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 93);
                                }
                                else if (ROW_NUM == 12)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 94);
                                }
                                else if (ROW_NUM == 13)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 95);
                                }



                            }
                            else if (TABLE_NUM == 7)            //Engineering
                            {
                                if (ROW_NUM == 1)
                                {
                                    category = table.InnerText.Trim();
                                }
                                else if (ROW_NUM == 3)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 96);
                                }
                                else if (ROW_NUM == 4)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 97);
                                }
                                else if (ROW_NUM == 5)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 98);
                                }
                                else if (ROW_NUM == 6)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 99);
                                }
                                else if (ROW_NUM == 7)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 100);
                                }
                                else if (ROW_NUM == 8)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 101);
                                }
                                else if (ROW_NUM == 9)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 102);
                                }
                                else if (ROW_NUM == 10)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 103);
                                }
                            }
                            else if (TABLE_NUM == 8)        //Industrial Transportation
                            {
                                if (ROW_NUM == 1)
                                {
                                    category = table.InnerText.Trim();
                                }
                                else if (ROW_NUM == 3)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 104);
                                }
                                else if (ROW_NUM == 4)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 105);
                                }
                                else if (ROW_NUM == 5)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 106);
                                }
                            }
                            else if (TABLE_NUM == 9)            // Support Services
                            {
                                if (ROW_NUM == 1)
                                {
                                    category = table.InnerText.Trim();
                                }
                                else if (ROW_NUM == 3)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 107);
                                }
                            }
                            else if (TABLE_NUM == 10)              //Automobile and Parts           
                            {
                                if (ROW_NUM == 1)
                                {
                                    category = table.InnerText.Trim();
                                }
                                else if (ROW_NUM == 3)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 108);
                                }
                                else if (ROW_NUM == 4)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 109);
                                }
                                else if (ROW_NUM == 5)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 110);
                                }
                                else if (ROW_NUM == 6)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 111);
                                }
                                else if (ROW_NUM == 7)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 112);
                                }
                                else if (ROW_NUM == 8)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 113);
                                }
                                else if (ROW_NUM == 9)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 114);
                                }
                                else if (ROW_NUM == 10)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 115);
                                }
                                else if (ROW_NUM == 11)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 116);
                                }
                                else if (ROW_NUM == 12)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 117);
                                }
                                else if (ROW_NUM == 13)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 118);
                                }
                                else if (ROW_NUM == 14)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 119);
                                }

                            }
                            else if (TABLE_NUM == 11)           //Beverages
                            {
                                if (ROW_NUM == 1)
                                {
                                    category = table.InnerText.Trim();
                                }
                                else if (ROW_NUM == 3)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 120);
                                }
                                else if (ROW_NUM == 4)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 121);
                                }
                                else if (ROW_NUM == 5)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 122);
                                }
                            }
                            else if (TABLE_NUM == 12)               //Food Producers
                            {
                                if (ROW_NUM == 1)
                                {
                                    category = table.InnerText.Trim();
                                }
                                else if (ROW_NUM == 3)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 123);
                                }
                                else if (ROW_NUM == 4)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 124);
                                }
                                else if (ROW_NUM == 5)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 125);
                                }
                                else if (ROW_NUM == 6)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 126);
                                }
                                else if (ROW_NUM == 7)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 127);
                                }
                                else if (ROW_NUM == 8)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 128);
                                }
                                else if (ROW_NUM == 9)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 129);
                                }
                                else if (ROW_NUM == 10)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 130);
                                }
                                else if (ROW_NUM == 11)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 131);
                                }
                                else if (ROW_NUM == 12)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 132);
                                }
                                else if (ROW_NUM == 13)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 133);
                                }
                                else if (ROW_NUM == 14)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 134);
                                }
                                else if (ROW_NUM == 15)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 135);
                                }
                                else if (ROW_NUM == 16)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 136);
                                }
                                else if (ROW_NUM == 17)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 137);
                                }
                                else if (ROW_NUM == 18)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 138);
                                }
                                else if (ROW_NUM == 19)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 139);
                                }
                                else if (ROW_NUM == 20)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 140);
                                }

                                else if (ROW_NUM == 21)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 141);
                                }
                                else if (ROW_NUM == 22)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 142);
                                }
                                else if (ROW_NUM == 23)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 143);
                                }
                                else if (ROW_NUM == 24)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 144);
                                }
                                else if (ROW_NUM == 25)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 145);
                                }
                                else if (ROW_NUM == 26)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 146);
                                }


                            }
                            else if (TABLE_NUM == 13)           //Household Goods
                            {
                                if (ROW_NUM == 1)
                                {
                                    category = table.InnerText.Trim();
                                }
                                else if (ROW_NUM == 3)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 147);
                                }
                                else if (ROW_NUM == 4)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 148);
                                }
                                else if (ROW_NUM == 5)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 149);
                                }
                                else if (ROW_NUM == 6)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 150);
                                }
                                else if (ROW_NUM == 7)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 151);
                                }

                            }
                            else if (TABLE_NUM == 14)       //Lesiure Goods (Miscellaneous)
                            {
                                if (ROW_NUM == 1)
                                {
                                    category = table.InnerText.Trim();
                                }
                                else if (ROW_NUM == 3)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 152);
                                }
                            }
                            else if (TABLE_NUM == 15)       //Personal Goods (Textile)
                            {
                                if (ROW_NUM == 1)
                                {
                                    category = table.InnerText.Trim();
                                }
                                else if (ROW_NUM == 3)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 153);
                                }
                                else if (ROW_NUM == 4)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 154);
                                }
                                else if (ROW_NUM == 5)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 155);
                                }
                                else if (ROW_NUM == 6)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 156);
                                }
                                else if (ROW_NUM == 7)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 157);
                                }
                                else if (ROW_NUM == 8)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 158);
                                }
                                else if (ROW_NUM == 9)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 159);
                                }
                                else if (ROW_NUM == 10)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 160);
                                }
                                else if (ROW_NUM == 11)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 161);
                                }
                                else if (ROW_NUM == 12)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 162);
                                }
                                else if (ROW_NUM == 13)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 163);
                                }
                                else if (ROW_NUM == 14)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 164);
                                }
                                else if (ROW_NUM == 15)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 165);
                                }
                                else if (ROW_NUM == 16)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 166);
                                }
                                else if (ROW_NUM == 17)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 167);
                                }
                                else if (ROW_NUM == 18)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 168);
                                }
                                else if (ROW_NUM == 19)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 169);
                                }
                                else if (ROW_NUM == 20)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 170);
                                }

                                else if (ROW_NUM == 21)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 171);
                                }
                                else if (ROW_NUM == 22)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 172);
                                }
                                else if (ROW_NUM == 23)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 173);
                                }
                                else if (ROW_NUM == 24)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 174);
                                }
                                else if (ROW_NUM == 25)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 175);
                                }
                                else if (ROW_NUM == 26)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 176);
                                }
                                else if (ROW_NUM == 27)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 177);
                                }
                                else if (ROW_NUM == 28)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 178);
                                }
                                else if (ROW_NUM == 29)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 179);
                                }
                                else if (ROW_NUM == 30)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 180);
                                }
                                else if (ROW_NUM == 31)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 181);
                                }
                                else if (ROW_NUM == 32)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 182);
                                }
                                else if (ROW_NUM == 33)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 183);
                                }
                                else if (ROW_NUM == 34)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 184);
                                }
                                else if (ROW_NUM == 35)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 185);
                                }
                                else if (ROW_NUM == 36)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 186);
                                }

                                else if (ROW_NUM == 37)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 187);
                                }
                                else if (ROW_NUM == 38)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 188);
                                }
                                else if (ROW_NUM == 39)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 189);
                                }
                                else if (ROW_NUM == 40)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 190);
                                }

                                else if (ROW_NUM == 41)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 191);
                                }
                                else if (ROW_NUM == 42)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 192);
                                }
                                else if (ROW_NUM == 43)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 193);
                                }
                                else if (ROW_NUM == 44)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 194);
                                }
                                else if (ROW_NUM == 45)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 195);
                                }

                                else if (ROW_NUM == 46)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 196);
                                }
                                else if (ROW_NUM == 47)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 197);
                                }
                                else if (ROW_NUM == 48)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 198);
                                }
                                else if (ROW_NUM == 49)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 199);
                                }

                                else if (ROW_NUM == 50)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 200);
                                }
                                else if (ROW_NUM == 51)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 201);
                                }
                                else if (ROW_NUM == 52)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 202);
                                }
                                else if (ROW_NUM == 53)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 203);
                                }
                                else if (ROW_NUM == 54)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 204);
                                }
                                else if (ROW_NUM == 55)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 205);
                                }
                                else if (ROW_NUM == 56)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 206);
                                }
                                else if (ROW_NUM == 57)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 207);
                                }
                                else if (ROW_NUM == 58)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 208);
                                }
                                else if (ROW_NUM == 59)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 209);
                                }
                                else if (ROW_NUM == 60)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 210);
                                }
                                else if (ROW_NUM == 61)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 211);
                                }

                                else if (ROW_NUM == 62)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 212);
                                }
                                else if (ROW_NUM == 63)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 213);
                                }
                                else if (ROW_NUM == 64)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 214);
                                }
                                else if (ROW_NUM == 65)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 215);
                                }

                                else if (ROW_NUM == 66)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 216);
                                }
                                else if (ROW_NUM == 67)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 217);
                                }
                                else if (ROW_NUM == 68)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 218);
                                }
                                else if (ROW_NUM == 69)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 219);
                                }
                                else if (ROW_NUM == 70)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 220);
                                }

                                else if (ROW_NUM == 71)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 221);
                                }
                                else if (ROW_NUM == 72)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 222);
                                }
                                else if (ROW_NUM == 73)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 223);
                                }
                                else if (ROW_NUM == 74)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 224);
                                }

                                else if (ROW_NUM == 75)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 225);
                                }
                                else if (ROW_NUM == 76)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 226);
                                }
                                else if (ROW_NUM == 77)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 227);
                                }
                                else if (ROW_NUM == 78)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 228);
                                }
                                else if (ROW_NUM == 79)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 229);
                                }
                                else if (ROW_NUM == 80)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 230);
                                }
                                else if (ROW_NUM == 81)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 231);
                                }
                                else if (ROW_NUM == 82)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 232);
                                }

                            }
                            else if (TABLE_NUM == 16)           // tobacco
                            {
                                if (ROW_NUM == 1)
                                {
                                    category = table.InnerText.Trim();
                                }
                                else if (ROW_NUM == 3)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 233);
                                }
                                else if (ROW_NUM == 4)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 234);
                                }
                                else if (ROW_NUM == 5)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 235);
                                }
                            }
                            else if (TABLE_NUM == 17)           // future contracts , not to be included.
                            {

                            }
                            else if (TABLE_NUM == 18)           // Health Care Equipment and Services
                            {
                                if (ROW_NUM == 1)
                                {
                                    category = table.InnerText.Trim();
                                }
                                else if (ROW_NUM == 3)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 236);
                                }

                            }
                            else if (TABLE_NUM == 19)           //Pharma and Bio Tech
                            {
                                if (ROW_NUM == 1)
                                {
                                    category = table.InnerText.Trim();
                                }
                                else if (ROW_NUM == 3)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 237);
                                }
                                else if (ROW_NUM == 4)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 238);
                                }
                                else if (ROW_NUM == 5)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 239);
                                }
                                else if (ROW_NUM == 6)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 240);
                                }
                                else if (ROW_NUM == 7)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 241);
                                }
                                else if (ROW_NUM == 8)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 242);
                                }
                                else if (ROW_NUM == 9)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 243);
                                }
                                else if (ROW_NUM == 10)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 244);
                                }
                                else if (ROW_NUM == 11)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 245);
                                }

                            }
                            else if (TABLE_NUM == 20)       // Media
                            {
                                if (ROW_NUM == 1)
                                {
                                    category = table.InnerText.Trim();
                                }
                                else if (ROW_NUM == 3)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 246);
                                }
                                else if (ROW_NUM == 4)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 247);
                                }

                            }
                            else if (TABLE_NUM == 21)       //Travel and Leisure
                            {
                                if (ROW_NUM == 1)
                                {
                                    category = table.InnerText.Trim();
                                }
                                else if (ROW_NUM == 3)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 248);
                                }
                                else if (ROW_NUM == 4)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 249);
                                }



                            }
                            else if (TABLE_NUM == 22)       //Fixed Line Telecommunication
                            {
                                if (ROW_NUM == 1)
                                {
                                    category = table.InnerText.Trim();
                                }
                                else if (ROW_NUM == 3)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 250);
                                }
                                else if (ROW_NUM == 4)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 251);
                                }
                                else if (ROW_NUM == 5)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 252);
                                }
                                else if (ROW_NUM == 6)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 253);
                                }

                            }
                            else if (TABLE_NUM == 23)       //Electricity
                            {
                                if (ROW_NUM == 1)
                                {
                                    category = table.InnerText.Trim();
                                }
                                else if (ROW_NUM == 3)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 254);
                                }
                                else if (ROW_NUM == 4)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 255);
                                }
                                else if (ROW_NUM == 5)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 256);
                                }
                                else if (ROW_NUM == 6)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 257);
                                }
                                else if (ROW_NUM == 7)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 258);
                                }
                                else if (ROW_NUM == 8)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 259);
                                }
                                else if (ROW_NUM == 9)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 260);
                                }
                                else if (ROW_NUM == 10)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 261);
                                }
                                else if (ROW_NUM == 11)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 262);
                                }
                                else if (ROW_NUM == 12)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 263);
                                }
                                else if (ROW_NUM == 13)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 264);
                                }
                                else if (ROW_NUM == 14)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 265);
                                }
                                else if (ROW_NUM == 15)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 266);
                                }
                                else if (ROW_NUM == 16)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 267);
                                }
                                else if (ROW_NUM == 17)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 268);
                                }
                                else if (ROW_NUM == 18)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 269);
                                }
                            }
                            else if (TABLE_NUM == 24)   // Multiutilities (Gas and water)
                            {
                                if (ROW_NUM == 1)
                                {
                                    category = table.InnerText.Trim();
                                }
                                else if (ROW_NUM == 3)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 270);
                                }
                                else if (ROW_NUM == 4)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 280);
                                }

                            }
                            else if (TABLE_NUM == 25)   //Commercial Banks
                            {
                                if (ROW_NUM == 1)
                                {
                                    category = table.InnerText.Trim();
                                }
                                else if (ROW_NUM == 3)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 281);
                                }
                                else if (ROW_NUM == 4)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 282);
                                }
                                else if (ROW_NUM == 5)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 283);
                                }
                                else if (ROW_NUM == 6)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 284);
                                }
                                else if (ROW_NUM == 7)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 285);
                                }
                                else if (ROW_NUM == 8)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 286);
                                }
                                else if (ROW_NUM == 9)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 287);
                                }
                                else if (ROW_NUM == 10)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 288);
                                }
                                else if (ROW_NUM == 11)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 289);
                                }
                                else if (ROW_NUM == 12)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 290);
                                }
                                else if (ROW_NUM == 13)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 291);
                                }
                                else if (ROW_NUM == 14)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 292);
                                }
                                else if (ROW_NUM == 15)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 293);
                                }
                                else if (ROW_NUM == 16)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 294);
                                }
                                else if (ROW_NUM == 17)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 295);
                                }
                                else if (ROW_NUM == 18)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 296);
                                }
                                else if (ROW_NUM == 19)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 297);
                                }
                                else if (ROW_NUM == 20)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 298);
                                }

                                else if (ROW_NUM == 21)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 299);
                                }
                                else if (ROW_NUM == 22)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 300);
                                }
                                else if (ROW_NUM == 23)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 301);
                                }
                                else if (ROW_NUM == 24)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 302);
                                }
                            }
                            else if (TABLE_NUM == 26)   //Non Life Insurance
                            {
                                if (ROW_NUM == 1)
                                {
                                    category = table.InnerText.Trim();
                                }
                                else if (ROW_NUM == 3)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 303);
                                }
                                else if (ROW_NUM == 4)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 304);
                                }
                                else if (ROW_NUM == 5)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 305);
                                }
                                else if (ROW_NUM == 6)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 306);
                                }
                                else if (ROW_NUM == 7)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 307);
                                }
                                else if (ROW_NUM == 8)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 308);
                                }
                                else if (ROW_NUM == 9)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 309);
                                }
                                else if (ROW_NUM == 10)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 310);
                                }
                                else if (ROW_NUM == 11)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 311);
                                }
                                else if (ROW_NUM == 12)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 312);
                                }
                                else if (ROW_NUM == 13)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 313);
                                }
                                else if (ROW_NUM == 14)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 314);
                                }
                                else if (ROW_NUM == 15)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 315);
                                }
                                else if (ROW_NUM == 16)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 316);
                                }
                                else if (ROW_NUM == 17)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 317);
                                }

                            }
                            else if (TABLE_NUM == 27)   //Life Insurance
                            {
                                if (ROW_NUM == 1)
                                {
                                    category = table.InnerText.Trim();
                                }
                                else if (ROW_NUM == 3)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 318);
                                }
                                else if (ROW_NUM == 4)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 319);
                                }
                                else if (ROW_NUM == 5)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 320);
                                }

                            }
                            else if (TABLE_NUM == 28)       //Real Estate Investment and Services
                            {
                                if (ROW_NUM == 1)
                                {
                                    category = table.InnerText.Trim();
                                }
                                else if (ROW_NUM == 3)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 321);
                                }

                            }
                            else if (TABLE_NUM == 29)       //Financial Services
                            {
                                if (ROW_NUM == 1)
                                {
                                    category = table.InnerText.Trim();
                                }
                                else if (ROW_NUM == 3)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 322);
                                }
                                else if (ROW_NUM == 4)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 323);
                                }
                                else if (ROW_NUM == 5)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 324);
                                }
                                else if (ROW_NUM == 6)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 325);
                                }
                                else if (ROW_NUM == 7)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 326);
                                }
                                else if (ROW_NUM == 8)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 327);
                                }
                                else if (ROW_NUM == 9)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 328);
                                }
                                else if (ROW_NUM == 10)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 329);
                                }
                                else if (ROW_NUM == 11)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 330);
                                }
                                else if (ROW_NUM == 12)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 331);
                                }
                                else if (ROW_NUM == 13)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 332);
                                }
                                else if (ROW_NUM == 14)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 333);
                                }
                                else if (ROW_NUM == 15)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 334);
                                }
                                else if (ROW_NUM == 16)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 335);
                                }
                                else if (ROW_NUM == 17)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 336);
                                }
                                else if (ROW_NUM == 18)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 337);
                                }
                                else if (ROW_NUM == 19)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 338);
                                }
                                else if (ROW_NUM == 20)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 339);
                                }

                                else if (ROW_NUM == 21)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 340);
                                }
                                else if (ROW_NUM == 22)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 341);
                                }
                                else if (ROW_NUM == 23)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 342);
                                }
                                else if (ROW_NUM == 24)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 343);
                                }
                                else if (ROW_NUM == 25)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 344);
                                }
                                else if (ROW_NUM == 26)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 345);
                                }
                                else if (ROW_NUM == 27)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 346);
                                }
                                else if (ROW_NUM == 28)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 347);
                                }

                            }
                            else if (TABLE_NUM == 30)       //Equity Investment Instruments. not be included.
                            {


                            }
                            else if (TABLE_NUM == 31)       //Software and Computer Services
                            {
                                if (ROW_NUM == 1)
                                {
                                    category = table.InnerText.Trim();
                                }
                                else if (ROW_NUM == 3)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 348);
                                }


                            }
                            else if (TABLE_NUM == 32)           //Technology Hardware and Equipment
                            {
                                if (ROW_NUM == 1)
                                {
                                    category = table.InnerText.Trim();
                                }
                                else if (ROW_NUM == 3)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 349);
                                }
                                else if (ROW_NUM == 4)
                                {
                                    if (table == null)
                                    {
                                        continue;
                                    }
                                    else
                                        InsertInDB(table, 350);
                                }
                            }
                        }
                    }
                    
                // filiing the dataTable with actual market summary.                    
                    insertActualSummaryIntoInMemoryDB(Market_Summary.marketStatus, Market_Summary.total_Advanced, Market_Summary.current_KSE100, Market_Summary.current_AllShare, Market_Summary.current_KSE30, Market_Summary.current_KMI30, Market_Summary.market_Volume, Market_Summary.total_Declined, Market_Summary.high_KSE100, Market_Summary.high_AllShare, Market_Summary.high_KSE30, Market_Summary.high_KMI30, Market_Summary.market_Value, Market_Summary.total_Unchanged, Market_Summary.low_KSE100, Market_Summary.low_AllShare, Market_Summary.low_KSE30, Market_Summary.low_KMI30, Market_Summary.market_Trades, Market_Summary.total, Market_Summary.change_KSE100, Market_Summary.change_AllShare, Market_Summary.change_KSE30, Market_Summary.change_KMI30); 
                       
                }
                catch (Exception e)
                {
                    test = e.Message;
                    System.Console.WriteLine("An Exception was caught in reading data table- KSE  ");
                    System.Console.WriteLine(e.Message);
                }
            }
            catch (Exception e)
            {
                
                test = "error inside big catch";
                System.Console.WriteLine("Unable to fetch data from: " + linkToKSE);
                System.Console.WriteLine(e.Message);
            }

        }
        public bool InsertInDB(HtmlNode table, int id)
        {
            
           
            if (table == null)
            {

            }
            string[] arr = table.InnerText.Split('\n');

            DataRow dr = summary.NewRow();

            dr[0] = id;
            dr[1] = DateTime.Now;
            dr[2] = arr[1].Trim();
            dr[3] = category;
            dr[4] = double.Parse(arr[2]);
            dr[5] = double.Parse(arr[3]);
            dr[6] = double.Parse(arr[4]);
            dr[7] = double.Parse(arr[5]);
            dr[8] = double.Parse(arr[6]);
            dr[9] = double.Parse(arr[7]);
            dr[10] = double.Parse(arr[8]);
            
            Company oneEntryForOneSymbol = new Company();

            oneEntryForOneSymbol.symbolID = id;
            oneEntryForOneSymbol.symbol = arr[1].Trim();
            oneEntryForOneSymbol.category = category;
            oneEntryForOneSymbol.ldcp = double.Parse(arr[2]);
            oneEntryForOneSymbol.open = double.Parse(arr[3]);
            oneEntryForOneSymbol.high = double.Parse(arr[4]);
            oneEntryForOneSymbol.low = double.Parse(arr[5]);
            oneEntryForOneSymbol.current = double.Parse(arr[6]);
            oneEntryForOneSymbol.change = double.Parse(arr[7]);
            oneEntryForOneSymbol.volume = double.Parse(arr[8]);
            oneEntryForOneSymbol.date = DateTime.Now;


            companies.Add(oneEntryForOneSymbol);

            ////   Insert this data in  original database when needed.

            ////bool check = db.populate_KSE_DATABASE_SUMMARY_Table(id, currentDateTime, symbol, category, ldcp, open, high, low, current, change, volume);
            ////  if (check == true)
            ////  {
            ////      Console.WriteLine("inserted!");
            ////      return true;
            ////  }           
            ////  return false; 

            return true;

        }       
        public void insertActualSummaryIntoInMemoryDB(string marketStatus, double total_Advanced, double current_KSE100, double current_AllShare, double current_KSE30, double current_KMI30, double market_Volume, double total_Declined, double high_KSE100, double high_AllShare, double high_KSE30, double high_KMI30, double market_Value, double total_Unchanged, double low_KSE100, double low_AllShare, double low_KSE30, double low_KMI30, double market_Trades, double total, double change_KSE100, double change_AllShare, double change_KSE30, double change_KMI30)
        {           

            DataRow dr = actual_summary.NewRow();

            dr[0] = marketStatus;
            dr[1] = market_Volume;
            dr[2] = market_Trades;
            dr[3] = market_Value ;

            dr[4] = total_Advanced;
            dr[5] = total_Declined;
            dr[6] = total_Unchanged;
            dr[7] = total;

            dr[8] = current_KSE100;
            dr[9] = high_KSE100;
            dr[10] = low_KSE100;
            dr[11] = change_KSE100;

            dr[12] = current_AllShare;
            dr[13] = high_AllShare;
            dr[14] = low_AllShare;
            dr[15] = change_AllShare;

            dr[16] = current_KSE30;
            dr[17] = high_KSE30;
            dr[18] = low_KSE30;
            dr[19] = change_KSE30;

            dr[20] = current_KMI30;
            dr[21] = high_KMI30;
            dr[22] = low_KMI30;
            dr[23] = change_KMI30;

            dr[24] = DateTime.Now;
           

       //     test = "current_AllShare: " + dr[12].ToString() ;            
            //string conString = @"C:\USERS\ABDUL_000\DOCUMENTS\VISUAL STUDIO 2013\PROJECTS\INTELLISTOCK_WEBSERVICE\INTELLISTOCK_WEBSERVICE\KSE_DATABASE.MDF";
            //SqlConnection con = new SqlConnection(conString);
            //con.Open();
            //string queryForActualSummaryTable = "Select * from ACTUAL_SUMMARY";
            //SqlCommand cmd = new SqlCommand(queryForActualSummaryTable, con);
            //da.SelectCommand = cmd;

            //// filing the actual summary table.

            //da.Fill(actual_summary);

            //DataRow[] dr = ds.Tables["ACTUAL_SUMMARY"].Select();
            //List<Market_Summary> listOfMarketSummaries = new List<Market_Summary>();


            //for (int i = 0; i < dr.Length; i++)
            //{
            //    Market_Summary mktsum = new Market_Summary();
            //    mktsum.marketStatus = (string)dr[i][0];
            //    mktsum.market_Volume = (double)dr[i][1];
            //    mktsum.market_Value = (double)dr[i][2];
            //    mktsum.market_Trades = (double)dr[i][3];

            //    mktsum.total_Advanced = (double)dr[i][4];
            //    mktsum.total_Declined = (double)dr[i][5];
            //    mktsum.total_Unchanged = (double)dr[i][6];
            //    mktsum.total = (double)dr[i][7];

            //    mktsum.current_KSE100 = (double)dr[i][8];
            //    mktsum.high_KSE100 = (double)dr[i][9];
            //    mktsum.low_KSE100 = (double)dr[i][10];
            //    mktsum.change_KSE100 = (double)dr[i][11];


            //    mktsum.current_AllShare = (double)dr[i][12];
            //    mktsum.high_AllShare = (double)dr[i][13];
            //    mktsum.low_AllShare = (double)dr[i][14];
            //    mktsum.change_AllShare = (double)dr[i][15];


            //    mktsum.current_KSE30 = (double)dr[i][16];
            //    mktsum.high_KSE30 = (double)dr[i][17];
            //    mktsum.low_KSE30 = (double)dr[i][18];
            //    mktsum.change_KSE30 = (double)dr[i][19];

            //    mktsum.current_KMI30 = (double)dr[i][20];
            //    mktsum.high_KMI30 = (double)dr[i][21];
            //    mktsum.low_KMI30 = (double)dr[i][22];
            //    mktsum.change_KMI30 = (double)dr[i][23];

            //    mktsum.currentDateTime = (DateTime)dr[i][24];
            //    listOfMarketSummaries.Add(mktsum);
            //}

            //string queryForSummaryTable = "Select * from SUMMARY";
            //SqlCommand cmd1 = new SqlCommand(queryForSummaryTable, con);
            //da.SelectCommand = cmd1;
            //da.Fill(summary);

            //test = status;
            //DataTable t1 = new DataTable();
            //DataColumn c1 = new DataColumn("Status", typeof(double));

            //ds.Tables.Add(t1);
          
            //  DataRow r = ds.Tables[1].Rows[2];
         
        }
        bool insertForRow1(string marketStatus, double totalAdvanced, double KSE100_Current, double AllShare_Current, double KSE30_Current, double KMI30_Current)
        {
            return db.insertForRow1IntoDBC(marketStatus, totalAdvanced, KSE100_Current, AllShare_Current, KSE30_Current, KMI30_Current, currentDateTime);

        }
        bool insertForRow2(double marketVolume, double totalDeclined, double KSE100_High, double AllShare_High, double KSE30_High, double KMI30_High)
        {

            return db.insertForRow2IntoDBC(marketVolume, totalDeclined, KSE100_High, AllShare_High, KSE30_High, KMI30_High, currentDateTime);
        }
        bool insertForRow3(double marketValue, double totalUnchanged, double KSE100_Low, double AllShare_Low, double KSE30_Low, double KMI30_Low)
        {

            return db.insertForRow3IntoDBC(marketValue, totalUnchanged, KSE100_Low, AllShare_Low, KSE30_Low, KMI30_Low, currentDateTime);
        }
        bool insertForRow4(double marketTrades, double total, double KSE100_Change, double AllShare_Change, double KSE30_Change, double KMI30_Change)
        {

            return db.insertForRow4IntoDBC(marketTrades, total, KSE100_Change, AllShare_Change, KSE30_Change, KMI30_Change, currentDateTime);
        }            
       
    }
}