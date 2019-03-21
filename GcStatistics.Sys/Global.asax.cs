using GcStatistics.Sys.App_Start;
using GcStatistics.Sys.Dal;
using GcStatistics.Sys.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;

namespace GcStatistics.Sys
{
    public class WebApiApplication : System.Web.HttpApplication, System.Web.SessionState.IRequiresSessionState
    {
        WorkOfUnit work = new WorkOfUnit();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            //启用异常过滤
            //GlobalConfiguration.Configuration.Filters.Add(new WebApiExceptionFilterAttribute());
            //删除数据库重新创建数据库
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<GcSiteDb>());
            //当models发生改变时修改数据库
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<GcSiteDb, GcStatistics.Sys.Dal.Migrations.Configuration>());
        }

        protected void Application_End()
        {
            //获取当前时间（结束时间）
            DateTime EndTime = DateTime.Now;
            //修改表中的结束时间
            VisitorInfo visitor = new VisitorInfo();
            //string time = HttpContext.Current.Session["AccessTime"].ToString(); 
            //work.CreateRepository<VisitorInfo>().Update(visitor,time);

            //查询访客的浏览开始时间
            //int a = work.ExecuteNonQuery(string.Format("select * from VisitorInfo where Id={0}", id));

        }
        public override void Init()
        {
            PostAuthenticateRequest += MvcApplication_PostAuthenticateRequest;
            base.Init();
        }

        void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
        {
            HttpContext.Current.SetSessionStateBehavior(
                SessionStateBehavior.Required);
        }

        protected void Session_Start()
        {
            Context.Session.Timeout = 1;
        }
        protected void Session_End()
        {
            DateTime startTime, endTime;
            //接受后台传递的Idid = Session["id"].ToString() == null ? 0 : int.Parse(Session["id"].ToString());
            var id = 0;
            id = int.Parse(Session["id"].ToString());
            if (id != 0)
            {
                List<VisitorInfo> list = work.CreateRepository<VisitorInfo>().GetList().ToList();
                var model = list.Where(p => p.Id == int.Parse(id.ToString())).FirstOrDefault();
                int vid = 0;
                vid = model.Id;
                startTime = model.AccessTime;
                model.AccessEndTime = DateTime.Now;
                endTime = model.AccessEndTime;
                TimeSpan ts1 = new TimeSpan(startTime.Ticks);
                TimeSpan ts2 = new TimeSpan(endTime.Ticks);
                TimeSpan ts = ts1.Subtract(ts2).Duration();
                string aa = ts.ToString();
                string H = aa.Split(':')[0].ToString();
                string M = aa.Split(':')[1].ToString();
                string S = aa.Split(':')[2].ToString();
                Double Duration = Double.Parse(H) * 3600 + Double.Parse(M) * 60 + Double.Parse(S);
                model.Duration = Duration;
                work.CreateRepository<VisitorInfo>().Update(model);
                int sum = work.CreateRepository<VisitorInfo>().GetCount(m => m.Id != 0);
                double duration = work.CreateRepository<VisitorInfo>().GetCount();
                duration = list.Sum(a => a.Duration);
                work.CreateRepository<VisitorInfo>().Update(model);
                List<WebInfo> weblist = work.CreateRepository<WebInfo>().GetList().ToList();
                var web = weblist.Where(p => p.Id == model.WebInfo.Id).FirstOrDefault();
                //平均时长
                double sc = duration / sum;
                //WebPv.WebTS = d         uration / sum;
                web.WebTS = (sc).ToString();
                work.CreateRepository<WebInfo>().Update(web);
                work.Save();
            }
        }
    }
}
