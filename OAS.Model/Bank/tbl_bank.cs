using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Model.Bank
{
    public class tbl_bank
    {
        /// <summary>
        /// 唯一键
        /// </summary>
        public List<string> keyfields { get { return new List<string>() { "id" }; } }

        /// <summary>
        /// 题目编号
        /// </summary>
        public int? id { set; get; }

        /// <summary>
        /// 题目标题
        /// </summary>
        public string title { set; get; }

        /// <summary>
        /// 题目类型
        /// </summary>
        public string qtype { set; get; }

        /// <summary>
        /// 图片路径
        /// </summary>
        public string picpath { set; get; }

        /// <summary>
        /// 动画路径
        /// </summary>
        public string animepath { set; get; }

        /// <summary>
        /// 供选项
        /// </summary>
        public string options { set; get; }

        /// <summary>
        /// 答案
        /// </summary>
        public string answer { set; get; }

        /// <summary>
        /// 答案解释
        /// </summary>
        public string answerdesc { set; get; }

        /// <summary>
        /// 答案图片路径
        /// </summary>
        public string answerpicpath { set; get; }

        /// <summary>
        /// 章节ID
        /// </summary>
        public string section { set; get; }

        /// <summary>
        /// 章节名称
        /// </summary>
        public string sectionname { set; get; }

        /// <summary>
        ///  归属类型，以","分割
        ///  A：客车
        ///  B：货车
        ///  C：小车
        ///  D：科目4
        /// </summary>
        public string belongtype { set; get; }

        /// <summary>
        /// 错题类型
        /// A：重点错题
        /// </summary>
        public string errortype { set; get; }

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
