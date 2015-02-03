using Newtonsoft.Json;
using Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IntelliStock_WebService.Controllers
{
    public class ValuesController : ApiController
    {       
        // GET api/values             
        public Object Get()
        {
            SchedulerClass schedule = new SchedulerClass();
            Web_Scraper obj = new Web_Scraper();
            obj.startScrapping();

            return obj.symbol_list;  
        }
      
    }
}