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
    public class WebApiApplication : System.Web.HttpApplication
    {
        WorkOfUnit work = new WorkOfUnit();
        GcSiteDb db = new GcSiteDb();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
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
            //接受后台传递的Id
            var id = HttpContext.Current.Session["id"];
            List<VisitorInfo> list = work.CreateRepository<VisitorInfo>().GetList().ToList();
            var model = list.Where(p => p.Id == int.Parse(id.ToString())).FirstOrDefault();
            int vid = 0;
            vid = model.Id;
            startTime = model.AccessTime;
            model.AccessEndTime = DateTime.Now;
            endTime = model.AccessEndTime;

            string dateDiff = null;
            TimeSpan ts1 = new TimeSpan(startTime.Ticks);
            TimeSpan ts2 = new TimeSpan(endTime.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            model.Duration = double.Parse(ts.Seconds.ToString());
            work.CreateRepository<VisitorInfo>().Update(model);
            int sum = work.CreateRepository<VisitorInfo>().GetCount(m => m.Id != 0);
            //list.GroupBy();
            double duration = work.CreateRepository<VisitorInfo>().GetCount();
            duration = list.Sum(a => a.Duration);
            //平均时长
            //double sc = duration / sum;
            //WebPv.WebTS = duration / sum;

            work.CreateRepository<VisitorInfo>().Update(model);
            work.Save();



            //int sum = work.CreateRepository<VisitorInfo>().GetCount(m => m.Id != 0);
            //sum = list.Sum(a=>a.Id);
            //double duration = work.CreateRepository<VisitorInfo>().GetCount();
            //duration = list.Sum(a=>a.Duration);
        }
    }
}
