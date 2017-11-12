using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Model.Permission
{
    public class sysuser_role
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
        /// 用户编号
        /// </summary>
        public string userno { set; get; }

        /// <summary>
        /// 角色编号
        /// </summary>
        public string roleno { set; get; }

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
