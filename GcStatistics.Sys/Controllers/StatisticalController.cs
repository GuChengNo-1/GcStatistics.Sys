using GcStatistics.Sys.Dal;
using GcStatistics.Sys.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public void Get(string key)
        {
            //搜索引擎来源
            var x = System.Web.HttpContext.Current.Request.UserAgent;
            ////获取浏览器信息
            //string aaa = Browser;
            //火狐浏览器
            if (x.Contains("Firefox")) { x = "火狐浏览器"; }
            //谷歌浏览器
            if (x.Contains("Chrome")) { x = "谷歌浏览器"; }
            //Safari浏览器（苹果浏览器）
            if (x.Contains("Version")) { x = "苹果浏览器"; }
            //Opera浏览器
            if (x.Contains("Opera")) { x = "Opera浏览器"; }
            //LBBROWSER浏览器（猎豹）
            if (x.Contains("LBBROWSER")) { x = "猎豹浏览器"; }
            //sougou浏览器（sougou）
            if (x.Contains("MetaSr")) { x = "搜狗浏览器"; }
            //Maxthon浏览器（傲游）
            if (x.Contains("Maxthon")) { x = "傲游浏览器"; }
            ////获取用户使用设备
            //string Client = "";
            //string u = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"];
            //if (u.Contains("ipad"))
            //{
            //    Client = "IPAD端访问量";
            //}
            //else if (u.Contains("iphone os") || u.Contains("midp") || u.Contains("rv:1.2.3.4") || u.Contains("ucweb") || u.Contains("android") || u.Contains("windows ce") || u.Contains("windows mobile"))
            //{
            //    Client = "IPHONE端访问量";
            //}
            //else
            //{
            //    Client = "PC端访问量";
            //}
            WebInfo web = new WebInfo();
            if (work.CreateRepository<WebInfo>().GetList(m => m.WebKey == key).Count() > 0)
            {
                web = work.CreateRepository<WebInfo>().GetFirst(m => m.WebKey == key);
                web.WebPv = web.WebPv + 1;
            }
            VisitorInfo vist = new VisitorInfo();
            vist.AccessTime = DateTime.Now;
            //vist.VisitPage = System.Web.HttpContext.Current.Request.Url.AbsoluteUri.ToString();
            vist.IpAddress = "";
            vist.VisitSE = "";
            vist.WebInfo = web;
            HttpContext.Current.Session["PageNumber"] = work.CreateRepository<WebInfo>().GetEntityById(vist.Id);
        }

        //[HttpPost]
        //#region
        //public IEnumerable<string> Get(string key, string VisitPage, string IpAddress, string Address)
        //{
        //    //搜索引擎来源
        //    var x = System.Web.HttpContext.Current.Request.UserAgent;
        //    //火狐浏览器
        //    if (x.Contains("Firefox")) { x = "火狐浏览器"; }
        //    //谷歌浏览器
        //    if (x.Contains("Chrome")) { x = "谷歌浏览器"; }
        //    //Safari浏览器（苹果浏览器）
        //    if (x.Contains("Version")) { x = "苹果浏览器"; }
        //    //Opera浏览器
        //    if (x.Contains("Opera")) { x = "Opera浏览器"; }
        //    //LBBROWSER浏览器（猎豹）
        //    if (x.Contains("LBBROWSER")) { x = "猎豹浏览器"; }
        //    //sougou浏览器（sougou）
        //    if (x.Contains("MetaSr")) { x = "搜狗浏览器"; }
        //    //Maxthon浏览器（傲游）
        //    if (x.Contains("Maxthon")) { x = "傲游浏览器"; }
        //    //获取用户使用设备
        //    string Client = "";
        //    string u = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"];
        //    if (u.Contains("ipad"))
        //    {
        //        Client = "IPAD端访问量";
        //    }
        //    else if (u.Contains("iphone os") || u.Contains("midp") || u.Contains("rv:1.2.3.4") || u.Contains("ucweb") || u.Contains("android") || u.Contains("windows ce") || u.Contains("windows mobile"))
        //    {
        //        Client = "IPHONE端访问量";
        //    }
        //    else
        //    {
        //        Client = "PC端访问量";
        //    }
        //    WebInfo web = new WebInfo();
        //    if (work.CreateRepository<WebInfo>().GetList(m => m.WebKey == key).Count() > 0)
        //    {
        //        web = work.CreateRepository<WebInfo>().GetFirst(m => m.WebKey == key);
        //        web.WebPv = web.WebPv + 1;
        //        web.WebUv = web.WebUv + 1;
        //    }
        //    VisitorInfo vist = new VisitorInfo();
        //    vist.AccessTime = DateTime.Now;
        //    vist.VisitPage = VisitPage;
        //    vist.IpAddress = IpAddress;
        //    vist.VisitSE = x;
        //    vist.WebInfo = web;

        //    return new string[] { "pv:" + web.WebPv + "-" + "uv:" + web.WebUv };
        //}
        //#endregion

        public void Put(string key)
        {
            string url = HttpContext.Current.Request.Url.PathAndQuery;
        }
        public void Delete(string key)
        {
        }
        public void Update(string key)
        {

        }
    }
}
