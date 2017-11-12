using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBFactoryEntity
{
    public static class DBFactoryExtension
    {
        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="MemberName">成员名称</param>
        /// <param name="MemberValue">成员值</param>
        /// <param name="MemberBelong">成员所属表 t,a,b等</param>
        /// <param name="MemberType">成员数据对象</param>
        /// <param name="qte">成员条件类型</param>
        public static void AddMember(this List<DBMemberEntity> entities, string MemberName, object MemberValue, QueryTypeEnum qte = QueryTypeEnum.equal, string MemberBelong = null, DbType MemberType = DbType.String)
        {
            DBMemberEntity entity = new DBMemberEntity()
            {
                MemberName = MemberName,
                MemberType = MemberType,
                MemberValue = MemberValue,
                QueryType = qte,
                MemberBelong = MemberBelong
            };
            entities.Add(entity);
        }
    }
}
