using GcStatistics.Sys.Dal;
using GcStatistics.Sys.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        private static string IsPostUrl = string.Empty;
        public IEnumerable<object> Get(string key, string VisitPage, string IpAddress, string Address)
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
                //因特网浏览器
                else { se = "IE"; }
                VisitorInfo vist = new VisitorInfo();
                vist.AccessTime = DateTime.Now;
                vist.VisitPage = VisitPage;
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
                //随机生成标识码(identification code) 32位字符
                string IC = Guid.NewGuid().ToString("N");
                var bo = work.CreateRepository<VisitorInfo>().GetList(m => m.IC == IC);
                if (bo.Count() > 0)
                {
                    IC = Guid.NewGuid().ToString("N");
                }
                vist.IC = IC;

                //Session传递开始时间  AccessTime
                //DateTime AccessTime = vist.AccessTime;
                //string aa = HttpContext.Current.Session["AccessTime"].ToString();
                //HttpContext.Current.Session["Id"] = vist.Id;
                //work.CreateRepository<VisitorInfo>().Update(vist,AccessTime.ToString());
                //var time = work.CreateRepository<VisitorInfo>().GetList(m => m.AccessTime == AccessTime); 

                var alikeCount = work.CreateRepository<VisitorInfo>().GetList(
                    m => m.IpAddress == vist.IpAddress && m.VisitPage == vist.VisitPage
                    );
                work.CreateRepository<VisitorInfo>().Insert(vist);
                work.Save();
                //判断用户是否相同用户
                if (!(alikeCount.Count() > 0))
                {
                    TimeSpan span = DateTime.Now - item.AccessTime;
                    //判断该ip地址访客访问时间是否超过24小时
                    int temp = Convert.ToInt32(span.TotalHours);
                    if (temp >= 24 && item.Lock == 0)
                    {
                        string sql = "update VisitorInfoes set ";
                        item.Lock = 1;
                        work.CreateRepository<VisitorInfo>().Update(item);
                        work.Save();
                        //item.PageNumber = 0;
                        work.CreateRepository<VisitorInfo>().Insert(vist);
                        //work.ExecuteNonQuery(sql);
                        work.Save();
                    }
                }
                
                #endregion
                web = work.CreateRepository<WebInfo>().GetFirst(m => m.WebKey == key);//获取pv
                web.WebPv = web.WebPv + 1;
                web.WebUv = work.CreateRepository<VisitorInfo>().GetCount(m => m.WebInfo.Id == web.Id);
                int rate = work.CreateRepository<VisitorInfo>().GetCount(m => m.PageNumber == 0);
                decimal rateResult = Math.Round((decimal)rate / web.WebPv, 4);
                web.BounceRate = (rateResult * 100).ToString().Length >= 5 ? (rateResult * 100).ToString().Substring(0, 5) + "%" : (rateResult * 100).ToString() + "%";
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
                List<VisitorInfo> list = work.CreateRepository<VisitorInfo>().GetList().ToList();
                var model = list.Where(p => p.IC == IC).FirstOrDefault();
                int id = model.Id;
                HttpContext.Current.Session["id"] = id;

                string dateDiff = null;
                TimeSpan ts1 = new TimeSpan(model.AccessTime.Ticks);
                TimeSpan ts2 = new TimeSpan(model.AccessEndTime.Ticks);
                TimeSpan ts = ts1.Subtract(ts2).Duration();
                model.Duration = double.Parse(ts.Seconds.ToString());

                work.CreateRepository<VisitorInfo>().Update(model);
                int sum = work.CreateRepository<VisitorInfo>().GetCount(m => m.Id != 0);
                //sum = list.Sum(a => a.Id);
                double duration = work.CreateRepository<VisitorInfo>().GetCount();
                duration = list.Sum(a => a.Duration);
                //平均访问时长
                web.WebTS = (duration / sum).ToString("0.00");
                
                work.CreateRepository<VisitorInfo>().Update(model);
                work.Save();

            }
            return new object[] { "持行成功" };
        }

        public void Put(int id, [FromBody]WebInfo model)
        {

        }

    }
}
