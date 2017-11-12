using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBFactoryEntity
{
    /// <summary>
    /// 实体信息
    /// </summary>
    public class DBMemberEntity
    {
        /// <summary>
        /// 字段归属 如表名. t. A.
        /// </summary>
        public string MemberBelong { set; get; }

        /// <summary>
        /// 成员名称
        /// </summary>
        public string MemberName { set; get; }

        /// <summary>
        /// 成员类型
        /// </summary>
        public DbType MemberType { set; get; }

        /// <summary>
        /// 成员值
        /// </summary>
        public Object MemberValue { set; get; }

        /// <summary>
        /// 查询类型
        /// </summary>
        public QueryTypeEnum QueryType { set; get; }
    }
}
