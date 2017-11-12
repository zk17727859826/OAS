using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Model.Common
{
    public class tbl_areainfo
    {
        /// <summary>
        /// 唯一键
        /// </summary>
        public List<string> keyfields { get { return new List<string>() { "areano" }; } }

        /// <summary>
        /// 区域编号
        /// </summary>
        public string areano { set; get; }

        /// <summary>
        /// 区域名称
        /// </summary>
        public string areaname { set; get; }

        /// <summary>
        /// 区域排序
        /// </summary>
        public int areaorder { set; get; }

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
