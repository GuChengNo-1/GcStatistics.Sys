using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcStatistics.Sys.Models
{
    /// <summary>
    /// 访客信息
    /// </summary>
    public class VisitorInfo:EntityBase
    {
        /// <summary>
        /// 访问页面
        /// </summary>
        public string VisitPage { get; set; }
        /// <summary>
        /// ip地址
        /// </summary>
        public string IpAddress { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// 地区
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 搜索引擎
        /// </summary>
        public string VisitSE { get; set; }
        /// <summary>
        /// 访问时间
        /// </summary>
        public DateTime AccessTime { get; set; }
        /// <summary>
        /// 访问结束时间
        /// </summary>
        public DateTime AccessEndTime { get; set; }
        /// <summary>
        /// 外键(网站流量表)
        /// </summary>
        public virtual ICollection<WebInfo> WebInfo { get; set; }
    }
}
