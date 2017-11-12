using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Model.Bank
{
    public class tbl_paper_bank
    {
        /// <summary>
        /// 唯一键
        /// </summary>
        public List<string> keyfields { get { return new List<string>() { "pkid" }; } }

        /// <summary>
        /// 键值
        /// </summary>
        public string pkid { set; get; }

        /// <summary>
        /// 试卷ID
        /// </summary>
        public int? paperid { set; get; }

        /// <summary>
        /// 试卷ID
        /// </summary>
        public int? bankid { set; get; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string creater { set; get; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? createdate { set; get; }
    }
}
