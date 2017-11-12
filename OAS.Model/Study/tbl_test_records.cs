using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Model.Study
{
    public class tbl_test_records
    {
        /// <summary>
        /// 唯一键
        /// </summary>
        public List<string> keyfields { get { return new List<string>() { "testno" }; } }

        /// <summary>
        /// 练习编号
        /// </summary>
        public string testno { set; get; }

        /// <summary>
        /// 学员号
        /// </summary>
        public string xueyh { set; get; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string xingming { set; get; }

        /// <summary>
        /// 练习类型：练习/考试
        /// </summary>
        public string testtype { set; get; }

        /// <summary>
        /// 练习子类型：练习类型：/顺序/随机/图片等
        /// </summary>
        public string projecttype { set; get; }

        /// <summary>
        /// 总练习数量
        /// </summary>
        public int? totalnum { set; get; }

        /// <summary>
        /// 正确的数量
        /// </summary>
        public int? oknum { set; get; }

        /// <summary>
        /// KM1：科目一
        /// KM4：科目四
        /// KMA：客车
        /// KMB：货车
        /// </summary>
        public string kemu { set; get; }

        /// <summary>
        /// 来源 
        /// 1：网页 
        /// 2：APP 
        /// 3：小程序
        /// </summary>
        public string source { set; get; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string creater { set; get; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? createdate { set; get; }

        /// <summary>
        /// 做题明细
        /// </summary>
        public List<tbl_test_records_details> details { set; get; }
    }
}
