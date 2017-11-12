using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Model.Permission
{
    public class syspower
    {
        /// <summary>
        /// 唯一键
        /// </summary>
        public List<string> keyfields { get { return new List<string>() { "powerno" }; } }

        /// <summary>
        /// 权限编号
        /// </summary>
        public string powerno { set; get; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string powername { set; get; }

        /// <summary>
        /// 权限描述
        /// </summary>
        public string memo { set; get; }

        /// <summary>
        /// 是否有权限
        /// </summary>
        public int haspower { set; get; }
    }
}
