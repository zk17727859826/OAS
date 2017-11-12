using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Model.Study
{
    public class tbl_test_records_details
    {
        /// <summary>
        /// 唯一键
        /// </summary>
        public List<string> keyfields { get { return new List<string>() { "pkid" }; } }

        /// <summary>
        /// 序号
        /// </summary>
        public int? seq { set; get; }

        /// <summary>
        /// 键值
        /// </summary>
        public string pkid { set; get; }

        /// <summary>
        /// 练习编号
        /// </summary>
        public string testno { set; get; }

        /// <summary>
        /// 题目编号
        /// </summary>
        public int qid { set; get; }

        /// <summary>
        /// 做题的答案
        /// </summary>
        public string answer { set; get; }
    }
}
