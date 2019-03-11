using GcStatistics.Sys.Dal;
using GcStatistics.Sys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;

namespace GcStatistics.Sys.Controllers
{
    /// <summary>
    /// 站长统计
    /// </summary>
    public class StatisticalController : ApiController
    {
        WorkOfUnit work = new WorkOfUnit();
        public void Get(string key)
        {
            WebInfo web = new WebInfo();
            if (work.CreateRepository<WebInfo>().GetList(m => m.WebKey == key).Count() > 0)
            {
                web = work.CreateRepository<WebInfo>().GetFirst(m => m.WebKey == key);
                web.WebPv = web.WebPv + 1;
                
            }
            VisitorInfo vist = new VisitorInfo();
            vist.AccessTime = DateTime.Now.Date;
            vist.VisitPage = System.Web.HttpContext.Current.Request.Url.AbsoluteUri.ToString();
            vist.IpAddress = "";
            vist.VisitSE = "";
            vist.WebInfo = web;
        }
        public void Put(string key)
        {

        }
    }
}
