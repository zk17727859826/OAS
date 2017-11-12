using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Model.Permission
{
    public class sysuser
    {
        /// <summary>
        /// 唯一键
        /// </summary>
        public List<string> keyfields { get { return new List<string>() { "userno" }; } }

        /// <summary>
        /// 用户编号
        /// </summary>
        public string userno { set; get; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string username { set; get; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string password { set; get; }

        /// <summary>
        /// 用户性别
        /// </summary>
        public string xingbie { set; get; }

        /// <summary>
        /// 身份号码
        /// </summary>
        public string shenfhm { set; get; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string shoujhm { set; get; }

        /// <summary>
        /// 是否禁用
        /// </summary>
        public string isvalid { set; get; }

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
