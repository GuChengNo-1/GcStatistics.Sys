﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using GcStatistics.Sys.Models;

namespace GcStatistics.Sys.Dal
{
    public class GcSiteDb:DbContext
    {
        public GcSiteDb()
            :base("connStr")
         {

        }
        public IDbSet<UserInfo> UserInfo { get; set; }

        public IDbSet<WebInfo> WebInfo { get; set; }

        public IDbSet<VisitorInfo> VisitorInfo { get; set; }
    }
}
