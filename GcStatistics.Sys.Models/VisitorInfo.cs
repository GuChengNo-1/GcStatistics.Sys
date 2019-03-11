using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcStatistics.Sys.Models
{
    public class VisitorInfo:EntityBase
    {
        //访问页面、ip地址、年龄、地区、搜索引擎、访问时间、访问结束时间(网站流量表)
        public int MyProperty { get; set; }
    }
}
