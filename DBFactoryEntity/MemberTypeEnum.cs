using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBFactoryEntity
{
    public enum MemberTypeEnum
    {
        /// <summary>
        /// 文本类型
        /// </summary>
        String = 0,

        /// <summary>
        /// 类型
        /// </summary>
        Int = 1,

        /// <summary>
        /// decimal
        /// </summary>
        Decimal = 2,

        /// <summary>
        /// 日期
        /// </summary>
        DateTime = 3
    }
}
