using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Model.Permission
{
    public class sysrole
    {
        /// <summary>
        /// 唯一键
        /// </summary>
        public List<string> keyfields { get { return new List<string>() { "roleno" }; } }

        /// <summary>
        /// 角色编号
        /// </summary>
        public string roleno { set; get; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string rolename { set; get; }

        /// <summary>
        /// 角色描述
        /// </summary>
        public string memo { set; get; }

        /// <summary>
        /// 角色是否固定不能编辑
        /// </summary>
        public string isfix { set; get; }

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
