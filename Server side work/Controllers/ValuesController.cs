//using Newtonsoft.Json;
using Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using System.Web.Mvc;
using System.Web;
using System.Data.SqlClient;
using IntelliStock_WebService.Models;
namespace IntelliStock_WebService.Controllers
{
    public class ValuesController : ApiController
    {
        KSE_DATABASEEntities db = new KSE_DATABASEEntities();
       
        [HttpGet]
        public ACTUAL_SUMMARY[] ReturnActualSummary()
        {
            //int count = db.ACTUAL_SUMMARY.Count();
            //ACTUAL_SUMMARY[] summary = new ACTUAL_SUMMARY[count];
            //for (int i = 1; i <= count;i++)
            //{
            //    summary[i] = db.ACTUAL_SUMMARY.Find(i);
            //}
            //return summary;
            return db.ACTUAL_SUMMARY.ToArray();
        }

        [HttpGet]
        public SUMMARY[] ReturnSummary()
        {
            return db.SUMMARies.ToArray();
        }

        [HttpPost]
        public bool AddStatus(int id, string status)
        {
            try
            {
                USER_STATUS us = (USER_STATUS)db.USER_STATUS.Where(x => x.ID == id);
                us.STATUS = status;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpGet]
        public string ViewStatus(int id)
        {
            try
            {
                USER_STATUS us = (USER_STATUS)db.USER_STATUS.Where(x => x.ID == id);
                return us.STATUS;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet]
        public SYSTEM_PREDICTION[] ViewSystemPrediction()
        {
            try
            {
                return db.SYSTEM_PREDICTION.ToArray();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet]
        public USER VerifyUser(string email, string password)
        {
            try
            {
                USER obj = db.USERS.First(x => x.EMAIL.Equals(email) && x.PASSWORD.Equals(password));
                return obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public bool AddUser(USER obj)
        {
            try
            {
                db.USERS.Add(obj);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        public bool UpdateUser(USER obj, string email)
        {
            try
            {
                if (email.Equals(null))
                {
                    return false;
                }
                else
                {
                    USER temp = db.USERS.First(x => x.EMAIL.Equals(email));
                    temp.LAST_NAME = obj.LAST_NAME;
                    temp.MOBILE = obj.MOBILE;
                    temp.FIRST_NAME = obj.FIRST_NAME;
                    temp.DOB = obj.DOB;
                    temp.CITY = obj.CITY;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        public bool RemoveUser(string email)
        {
            try
            {
                USER u = db.USERS.First(x => x.EMAIL.Equals(email));
                db.USERS.Remove(u);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        public bool AddPrediction(USER_PREDICTION obj)
        {
            try
            {
                db.USER_PREDICTION.Add(obj);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public  class contact
        {
            public string name { get; set; }
            public string email { get; set; }

        }
        public class User
        {
            public contact c = new contact();
            public string u { get; set; }
            public string p { get; set; }
        }
             



//----------------------------------------------------------------------------------------------------------------------------------------------
       

        //// GET api/values             
        //public IEnumerable<Company> GetAllCompanies()
        //{
        //    //SchedulerClass schedule = new SchedulerClass();

        //    Web_Scraper obj = new Web_Scraper();
        //    obj.startScrapping();

        //    if (obj.companies.Equals(null))
        //    {
        //        // no internet.
        //    }
        //    return obj.companies;
         

      
           
        //public Object GetMarketSummary(string get)
        //{
        //    Web_Scraper obj = new Web_Scraper();
        //    obj.startScrapping();

        //    if (obj.Market_Summary.Equals(null))
        //    {
        //        // no internet.
        //    }
        //    return obj.Market_Summary;
         

        //}      
        //public void Post([FromBody]string value)
        //{

        //}

        //// PUT api/values/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}


        
      
    }
}