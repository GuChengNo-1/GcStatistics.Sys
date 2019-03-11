using GcStatistics.Sys.Dal;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace GcStatistics.Sys
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            //删除数据库重新创建数据库
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<GcSiteDb>());
            //当models发生改变时修改数据库
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<GcSiteDb, GcStatistics.Sys.Dal.Migrations.Configuration>());
        }
    }
}
