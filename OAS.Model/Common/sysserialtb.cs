using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Model.Common
{
    public class sysserialtb
    {
        /// <summary>
        /// 唯一键
        /// </summary>
        public List<string> keyfields { get { return new List<string>() { "serialno", "serialfix" }; } }

        /// <summary>
        /// 流水号代号
        /// </summary>
        public string serialno { set; get; }

        /// <summary>
        /// 流水号前缀
        /// </summary>
        public string serialfix { set; get; }

        /// <summary>
        /// 当前流水
        /// </summary>
        public decimal? currentnumber { set; get; }
    }
}
