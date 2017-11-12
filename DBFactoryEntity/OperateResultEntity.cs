using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBFactoryEntity
{
    public class OperateResultModel
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool success { set; get; }

        /// <summary>
        /// 消息
        /// </summary>
        public string message { set; get; }

        /// <summary>
        /// 数据数量
        /// </summary>
        public int total { set; get; }

        /// <summary>
        /// 数据
        /// </summary>
        public object rows { set; get; }
    }
}
