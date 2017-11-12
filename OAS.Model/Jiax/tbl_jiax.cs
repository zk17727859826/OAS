using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Model.Jiax
{
    public class tbl_jiax
    {
        /// <summary>
        /// 唯一键
        /// </summary>
        public List<string> keyfields { get { return new List<string>() { "jiaxid" }; } }

        /// <summary>
        /// 驾校ID
        /// </summary>
        public string jiaxid { set; get; }

        /// <summary>
        /// 驾校名称
        /// </summary>
        public string jiaxname { set; get; }

        /// <summary>
        /// 驾校联系人
        /// </summary>
        public string jiaxcontacter { set; get; }

        /// <summary>
        /// 固定电话
        /// </summary>
        public string jiaxtel { set; get; }

        /// <summary>
        /// 手机
        /// </summary>
        public string jiaxmobile { set; get; }

        /// <summary>
        /// 地址
        /// </summary>
        public string jiaxaddress { set; get; }

        /// <summary>
        /// 区域代码
        /// </summary>
        public string areano { set; get; }

        /// <summary>
        /// 区域名称
        /// </summary>
        public string areaname { set; get; }

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
