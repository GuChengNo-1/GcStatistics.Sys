using GcStatistics.Sys.Dal;
using GcStatistics.Sys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GcStatistics.Sys.Controllers
{
    //只规定哪些域名访问该api
    //[EnableCors(origins: "http://localhost:58392/", headers: "*", methods: "GET,POST,PUT,DELETE")]
    public class ValuesController : ApiController
    {
        public static int count = 0;
        WorkOfUnit work = new WorkOfUnit();
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(string key)
        {
            IPAddress[] ip = null;
            string url = System.Web.HttpContext.Current.Request.Url.Host.ToString();
            if (key == "E10ADC3949BA59ABBE56E057F20F883E")
            {
                //获取访问总量
                work.Save();
                //获取本机ip地址
                string pcName = System.Net.Dns.GetHostName();
                ip = Dns.Resolve(pcName).AddressList;
            }

            return ip[0].ToString();
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        //PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
