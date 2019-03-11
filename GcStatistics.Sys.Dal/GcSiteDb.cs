using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace GcStatistics.Sys.Dal
{
    public class GcSiteDb:DbContext
    {
        public GcSiteDb()
            :base("connStr")
        {

        }
    }
}
