using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Model.Jiax
{
    public class tbl_xueyuan
    {
        /// <summary>
        /// 唯一键
        /// </summary>
        public List<string> keyfields { get { return new List<string>() { "xueyh" }; } }

        /// <summary>
        /// 学员号
        /// </summary>
        public string xueyh { set; get; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string xingming { set; get; }

        /// <summary>
        /// 身份号码
        /// </summary>
        public string shenfhm { set; get; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string cardno { set; get; }

        /// <summary>
        /// 手机
        /// </summary>
        public string mobile { set; get; }

        /// <summary>
        /// 驾校ID
        /// </summary>
        public string jiaxid { set; get; }

        /// <summary>
        /// 驾校名称
        /// </summary>
        public string jiaxname { set; get; }

        /// <summary>
        /// 是否管理员
        /// </summary>
        public string isadmin { set; get; }

        /// <summary>
        /// 密码：初始密码：123456
        /// </summary>
        public string password { set; get; }

        /// <summary>
        /// 性别
        /// </summary>
        public string xingbie { set; get; }

        /// <summary>
        /// 固定电话
        /// </summary>
        public string tel { set; get; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string email { set; get; }

        /// <summary>
        /// 家庭地址
        /// </summary>
        public string address { set; get; }

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
