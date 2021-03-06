﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Model.Bank
{
    public class tbl_paper
    {
        /// <summary>
        /// 唯一键
        /// </summary>
        public List<string> keyfields { get { return new List<string>() { "paperid" }; } }

        /// <summary>
        /// 试卷ID
        /// </summary>
        public int? paperid { set; get; }

        /// <summary>
        /// 试卷名称
        /// </summary>
        public string papername { set; get; }

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
