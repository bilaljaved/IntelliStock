using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntelliStock_WebService
{
    public class Symbols_Table
    {
        public DateTime date { get; set; }
        public int symbolID { get; set; }
        public string symbol { get; set; }
        public string category { get; set; }
        public double ldcp { get; set; }
        public double open { get; set; }
        public double high { get; set; }
        public double low { get; set; }
        public double current { get; set; }
        public double change { get; set; }
        public double volume { get; set; }
    }
}