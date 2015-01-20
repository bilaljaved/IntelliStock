using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntelliStock_WebService
{
    public class Market_Summary
    {
        public String marketStatus { get; set; }
        public double total_Advanced { get; set; }
        public double current_KSE100 { get; set; }
        public double current_AllShare { get; set; }
        public double current_KSE30 { get; set; }
        public double current_KMI30 { get; set; }


        public double market_Volume { get; set; }
        public double total_Declined { get; set; }
        public double high_KSE100 { get; set; }
        public double high_AllShare { get; set; }
        public double high_KSE30 { get; set; }
        public double high_KMI30 { get; set; }

        public double market_Value { get; set; }
        public double total_Unchanged { get; set; }
        public double low_KSE100 { get; set; }
        public double low_AllShare { get; set; }
        public double low_KSE30 { get; set; }
        public double low_KMI30 { get; set; }

        public double market_Trades { get; set; }
        public double total { get; set; }
        public double change_KSE100 { get; set; }
        public double change_AllShare { get; set; }
        public double change_KSE30 { get; set; }
        public double change_KMI30 { get; set; }

        public DateTime currentDateTime { get; set; }
    }
}