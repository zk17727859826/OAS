using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Model.Permission
{
    public class sysmenu_object
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
        /// 菜单编号
        /// </summary>
        public string menuno { set; get; }

        /// <summary>
        /// 对应权限对象
        /// </summary>
        public string forobject { set; get; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string creater { set; get; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? createdate { set; get; }

        /// <summary>
        /// 是否拥有权限
        /// </summary>
        public int? haspower { set; get; }
    }
}
