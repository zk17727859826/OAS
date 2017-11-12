using DBFactoryEntity;
using OAS.Dal.Permission;
using OAS.Model.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Bll
{
    public partial class Permission
    {
        /// <summary>
        /// 查询数据信息
        /// </summary>
        /// <returns></returns>
        public OperateResultModel QueryPower()
        {
            SysPowerDal dal = new SysPowerDal();
            return dal.Query();
        }

        /// <summary>
        /// 插入用户信息
        /// </summary>
        /// <param name="power">权限实体</param>
        /// <returns></returns>
        public OperateResultModel InsertPower(syspower power)
        {
            SysPowerDal dal = new SysPowerDal();
            return dal.Insert(power);
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="power">权限实体</param>
        /// <returns></returns>
        public OperateResultModel UpdatePower(syspower power)
        {
            SysPowerDal dal = new SysPowerDal();
            return dal.Update(power);
        }
    }
}
