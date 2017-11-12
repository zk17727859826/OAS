using DBFactoryEntity;
using OAS.Model.Permission;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tg.Helper;

namespace OAS.Dal.Permission
{
    public class SysPowerDal : BaseDal<syspower>
    {
        /// <summary>
        /// 查询数据信息
        /// </summary>
        /// <returns></returns>
        public OperateResultModel Query()
        {
            OperateResultModel orm = Query(null, null, null);
            if (orm.rows != null)
            {
                orm.rows = ModelHelper.ToModel<List<syspower>>((DataTable)orm.rows);
            }
            return orm;
        }

        /// <summary>
        /// 插入用户信息
        /// </summary>
        /// <param name="power">权限实体</param>
        /// <returns></returns>
        public OperateResultModel Insert(syspower power)
        {
            return dbhelper.Insert(power);
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="power">权限实体</param>
        /// <returns></returns>
        public OperateResultModel Update(syspower power)
        {
            return dbhelper.Update(power);
        }
    }
}
