using DBFactoryEntity;
using OAS.Dal.Permission;
using OAS.Model.Common;
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
        /// 查询角色菜单信息为建立树
        /// </summary>
        /// <param name="roleno">角色编号</param>
        /// <param name="getall">是否获得所有菜单，在有权限的菜单上加勾</param>
        /// <returns></returns>
        public List<JsonTree> QueryRoleMenusForTree(string roleno, bool getall = true)
        {
            SysRolePowerDal rolepower = new SysRolePowerDal();
            List<JsonTree> trees = rolepower.QueryMenusOfRoleForTree(roleno, getall);
            return trees;
        }

        /// <summary>
        /// 新增角色对应权限信息
        /// </summary>        
        /// <param name="roleno">角色编号</param>
        /// <param name="menuno">菜单编号</param>
        /// <param name="powerno">权限编号</param>
        /// <param name="rolemenus">角色对应菜单</param>
        /// <param name="rolepowers">角色对应的权限</param>
        /// <param name="roleobjects">角色对应的对象</param>
        /// <returns></returns>
        public OperateResultModel InsertRolePowers(string roleno, string menuno, string powerno, List<sysrole_menu> rolemenus, List<sysrole_menu_power> rolepowers, List<sysrole_menu_power_object> roleobjects)
        {
            SysRolePowerDal dal = new SysRolePowerDal();
            return dal.Insert(roleno, menuno, powerno, rolemenus, rolepowers, roleobjects);
        }
    }
}
