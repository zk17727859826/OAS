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
        /// 获得角色对应菜单的权限信息
        /// </summary>
        /// <param name="roleno">角色编号</param>
        /// <param name="menuno">菜单编号</param>
        /// <returns></returns>
        public List<syspower> QueryRolePowers(string roleno, string menuno)
        {
            SysRoleMenuPowerDal dal = new SysRoleMenuPowerDal();
            return dal.QueryRolePowers(roleno, menuno);
        }
    }
}
