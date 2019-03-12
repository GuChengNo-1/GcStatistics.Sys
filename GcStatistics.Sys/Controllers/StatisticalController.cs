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
        private static string IsPostUrl = string.Empty;
        public IEnumerable<object> Get(string key,string VisitPage, string IpAddress, string Address)
         {
            WebInfo web = work.CreateRepository<WebInfo>().GetFirst(m => m.WebKey == key);
            if (web != null)
            {
                #region 获取访客信息&添加访客信息
                //搜索引擎来源  
                var se = System.Web.HttpContext.Current.Request.UserAgent;
                //获取浏览器信息
                //火狐浏览器
                if (se.Contains("Firefox")) { se = "火狐"; }
                //谷歌浏览器
                if (se.Contains("Chrome")) { se = "谷歌"; }
                //Safari浏览器（苹果浏览器）
                if (se.Contains("Version")) { se = "苹果"; }
                //Opera浏览器
                if (se.Contains("Opera")) { se = "Opera"; }
                //LBBROWSER浏览器（猎豹）
                if (se.Contains("LBBROWSER")) { se = "猎豹"; }
                //sougou浏览器（sougou）
                if (se.Contains("MetaSr")) { se = "搜狗"; }
                //Maxthon浏览器（傲游）
                if (se.Contains("Maxthon")) { se = "傲游"; }
                VisitorInfo vist = new VisitorInfo();
                vist.AccessTime = DateTime.Now;
                vist.VisitPage = VisitPage; //System.Web.HttpContext.Current.Request.UrlReferrer.ToString()
                vist.IpAddress = IpAddress;
                vist.VisitSE = se;
                vist.WebInfo = web;
                vist.Address = Address;
                vist.Age = 0;
                vist.AccessEndTime = DateTime.Now;
                //判断用户是否访问一个就退出
                if (IsPostUrl == string.Empty)
                {
                    IsPostUrl = VisitPage;
                }
                if (IsPostUrl != VisitPage)
                {
                    vist.PageNumber = 1;
                }
                var alikeCount = work.CreateRepository<VisitorInfo>().GetList(
                    m => m.IpAddress == vist.IpAddress && m.VisitPage == vist.VisitPage
                    );
                //判断用户是否相同用户
                if (!(alikeCount.Count() > 0))
                {
                    work.CreateRepository<VisitorInfo>().Insert(vist);
                    work.Save();
                }

                #endregion
                web = work.CreateRepository<WebInfo>().GetFirst(m => m.WebKey == key);//获取pv
                web.WebPv = web.WebPv + 1;
                web.WebUv = work.CreateRepository<VisitorInfo>().GetCount(m => m.WebInfo.Id == web.Id);
                int rate = work.CreateRepository<VisitorInfo>().GetCount(m => m.PageNumber == 0);
                decimal result = Math.Round((decimal)rate / web.WebPv, 4);
                web.BounceRate = result.ToString();
                List<VisitorInfo> webuv = work.CreateRepository<VisitorInfo>().GetList().ToList();//获取uv
                for (int i = 0; i < webuv.Count(); i++)
                {
                    for (int j = webuv.Count() - 1; j > i; j--)
                    {
                        if (webuv[i].IpAddress == webuv[j].IpAddress)
                        {
                            webuv.RemoveAt(j);
                        }
                    }
                }
                web.IpCount = webuv.Count;//获取ip数 去重
                work.CreateRepository<WebInfo>().Update(web);
                work.Save();
                //HttpContext.Current.Session["PageNumber"] = 0;
                //var n = HttpContext.Current.Session["PageNumber"].ToString();
            }
            else
            {

            }

            return new object[] { "持行成功" };
        }

        public void Put(int id, [FromBody]WebInfo model)
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
