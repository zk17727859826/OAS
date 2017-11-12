using OAS.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Dal.Common
{
    public class SysSerialtbDal : BaseDal<sysserialtb>
    {
        /// <summary>
        /// 查询流水号信息
        /// </summary>
        /// <param name="serialno">序列编号</param>
        /// <param name="serialfix">流水号前缀</param>
        /// <returns></returns>
        public sysserialtb Query(string serialno, string serialfix)
        {
            sysserialtb tb = new sysserialtb()
            {
                serialno = serialno,
                serialfix = serialfix
            };
            return QueryEntity(tb);
        }

        /// <summary>
        /// 添加流水当前信息
        /// </summary>
        /// <param name="dbfactory">连接数据库对象</param>
        /// <param name="tb">实体</param>
        /// <returns></returns>
        public bool InsertSerialTb(DBFactoryEntity.DBFactory dbfactory, sysserialtb tb)
        {
            return dbfactory.Insert(dbfactory.Connection(), dbfactory.Transaction(), tb);
        }

        /// <summary>
        /// 更新流水当前信息
        /// </summary>
        /// <param name="dbfactory">连接数据库对象</param>
        /// <param name="tb">实体</param>
        /// <returns></returns>
        public bool UpdateSerialTb(DBFactoryEntity.DBFactory dbfactory, sysserialtb tb)
        {
            return dbfactory.Update(dbfactory.Connection(), dbfactory.Transaction(), tb) == 1;
        }
    }
}
