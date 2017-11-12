using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Model.Common
{
    public class sysserialset
    {
        /// <summary>
        /// 唯一键
        /// </summary>
        public List<string> keyfields { get { return new List<string>() { "serialno" }; } }

        /// <summary>
        /// 编号代码
        /// </summary>
        public string serialno { set; get; }

        /// <summary>
        /// 编号名称
        /// </summary>
        public string serialname { set; get; }

        /// <summary>
        /// 前缀连接符
        /// </summary>
        public string prefix { set; get; }

        /// <summary>
        /// 前缀
        /// </summary>
        public string preflag { set; get; }

        /// <summary>
        /// 中缀连接符
        /// </summary>
        public string midfix { set; get; }

        /// <summary>
        /// 中缀
        /// </summary>
        public string midflag { set; get; }

        /// <summary>
        /// 后缀连接符
        /// </summary>
        public string lastfix { set; get; }

        /// <summary>
        /// 后缀
        /// </summary>
        public string lastflag { set; get; }

        /// <summary>
        /// 年字数
        /// </summary>
        public int? yearnum { set; get; }

        /// <summary>
        /// 月字数
        /// </summary>
        public int? monthnum { set; get; }

        /// <summary>
        /// 日字数
        /// </summary>
        public int? daynum { set; get; }

        /// <summary>
        /// 流水号字数
        /// </summary>
        public int? serialnum { set; get; }
    }
}
