using GcStatistics.Sys.Dal;
using GcStatistics.Sys.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            //接受后台传递的Id
            string id = HttpContext.Current.Session["PageNumber"].ToString();
            work.CreateRepository<VisitorInfo>().Update(visitor, id);

            //查询访客的浏览开始时间
            int a = work.ExecuteNonQuery(string.Format("select * from VisitorInfo where Id={0}", id));



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
    }
}
