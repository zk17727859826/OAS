using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Model.Bank
{
    public class tbl_error_bank
    {
        /// <summary>
        /// 唯一键
        /// </summary>
        public List<string> keyfields { get { return new List<string>() { "errorno" }; } }

        /// <summary>
        /// 错题编号
        /// </summary>
        public string errorno { set; get; }

        /// <summary>
        /// 错题类型：重点错题(0)/用户错题(1)
        /// </summary>
        public string errortype { set; get; }

        /// <summary>
        /// 题目ID
        /// </summary>
        public int qid { set; get; }

        /// <summary>
        /// 学员号:errortype为用户错题(1)时才有值
        /// </summary>
        public string xueyh { set; get; }
    }
}
