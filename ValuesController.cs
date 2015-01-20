using Newtonsoft.Json;
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
        public class User
        {
            public string name { get; set; }
            public int id { get; set; }

        }
        // GET api/values             
        public Object Get()
        {

       //     User obj = new User { name = "Moeed", id = 1 };
    
            Web_Scraper obj = new Web_Scraper();
            obj.startScrapping();

            return obj.symbol_list;  
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }


        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}