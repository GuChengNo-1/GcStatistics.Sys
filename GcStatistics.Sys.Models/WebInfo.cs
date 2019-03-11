using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcStatistics.Sys.Models
{
    /// <summary>
    /// 网站信息
    /// </summary>
    public class WebInfo:EntityBase
    {
        /// <summary>
        /// 密钥
        /// </summary>
        public string WebKey { get; set; }
        /// <summary>
        /// 网站域名
        /// </summary>
        public string WebDomain { get; set; }
        /// <summary>
        /// pv
        /// </summary>
        public string WebPv { get; set; }
        /// <summary>
        /// uv
        /// </summary>
        public string WebUv { get; set; }
        /// <summary>
        /// ip数
        /// </summary>
        public string IpCount { get; set; }
        /// <summary>
        /// 跳出率
        /// </summary>
        public string BounceRate { get; set; }
        /// <summary>
        /// 平均访问时长
        /// </summary>
        public string WebTS { get; set; }
        /// <summary>
        /// 转换次数
        /// </summary>
        public int WebConversion { get; set; }
        /// <summary>
        /// 外键(用户表)
        /// </summary>
        public virtual ICollection<UserInfo> Student { get; set; }
        public virtual VisitorInfo VisitorInfo { get; set; }
    }
}
