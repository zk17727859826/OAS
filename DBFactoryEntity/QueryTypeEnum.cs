using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBFactoryEntity
{
    public enum QueryTypeEnum
    {
        /// <summary>
        /// 等于
        /// </summary>
        equal = 0,

        /// <summary>
        /// 小于
        /// </summary>
        lt = 1,

        /// <summary>
        /// 大于
        /// </summary>
        grant = 2,

        /// <summary>
        /// 模糊查询
        /// </summary>
        fruzz = 3,

        /// <summary>
        /// 不等于
        /// </summary>
        notequal = 4
    }
}
