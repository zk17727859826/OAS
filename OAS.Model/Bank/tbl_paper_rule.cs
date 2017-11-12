using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Model.Bank
{
    public class tbl_paper_rule
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
        /// 科目 KM1 KM4
        /// </summary>
        public string kemu { set; get; }

        /// <summary>
        /// 车型：小车/客车/货车/科目4
        /// </summary>
        public string chexing { set; get; }

        /// <summary>
        /// 单选题
        /// </summary>
        public int? single { set; get; }

        /// <summary>
        /// 判断题
        /// </summary>
        public int? judge { set; get; }

        /// <summary>
        /// 多选题
        /// </summary>
        public int? multi { set; get; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string creater { set; get; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? createdate { set; get; }

        /// <summary>
        /// 编辑人
        /// </summary>
        public string editer { set; get; }

        /// <summary>
        /// 编辑时间
        /// </summary>
        public DateTime? editdate { set; get; }
    }
}
