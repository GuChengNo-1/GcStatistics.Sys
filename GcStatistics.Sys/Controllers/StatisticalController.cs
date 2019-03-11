using GcStatistics.Sys.Dal;
using GcStatistics.Sys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        GcSiteDb db = new GcSiteDb();
        public void Put(string key)
        {
            //string strPath = "http://" + Request.ServerVariables["HTTP_HOST"] + Request.ServerVariables["PATH_INFO"] + "?" + Request.ServerVariables["QUERY_STRING"];
            //if (strPath.EndsWith("?"))
            //{
            //    strPath = strPath.Substring(0, strPath.Length - 1);
            //}
           string url=  HttpContext.Current.Request.Url.PathAndQuery;
        }
        public void Get(string key)
        {

        }
        public void Delete(string key)
        {
        }
        public void Update(string key)
        {
            
        }
    }
}
