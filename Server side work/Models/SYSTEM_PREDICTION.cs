//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IntelliStock_WebService.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SYSTEM_PREDICTION
    {
        public int SPID { get; set; }
        public Nullable<System.DateTime> DATE { get; set; }
        public string SYMBOL { get; set; }
        public string PREDICTION { get; set; }
        public string STATUS { get; set; }
    }
}