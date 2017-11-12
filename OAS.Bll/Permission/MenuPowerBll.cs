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
        /// 查询菜单对应的权限信息
        /// </summary>
        /// <param name="menuno">菜单编号</param>
        /// <returns></returns>
        public OperateResultModel QueryMenuPower(string menuno)
        {
            SysMenuPowerDal dal = new SysMenuPowerDal();
            return dal.Query(menuno);
        }

        /// <summary>
        /// 插入菜单权限信息
        /// </summary>
        /// <param name="menupower">菜单权限实体</param>
        /// <returns></returns>
        public OperateResultModel InsertMenuPower(sysmenu_power menupower)
        {
            SysMenuPowerDal dal = new SysMenuPowerDal();
            return dal.Insert(menupower);
        }

        /// <summary>
        /// 删除菜单权限信息
        /// </summary>
        /// <param name="pkid">键值</param>
        /// <returns></returns>
        public OperateResultModel DeleteMenuPower(string pkid)
        {
            SysMenuPowerDal dal = new SysMenuPowerDal();
            return dal.Delete(pkid);
        }
    }
}
