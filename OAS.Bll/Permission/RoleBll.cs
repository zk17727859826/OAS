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
        /// 查询角色信息
        /// </summary>
        /// <param name="roleno">角色编号</param>
        /// <returns></returns>
        public sysrole QueryRole(string roleno)
        {
            SysRoleDal dal = new SysRoleDal();
            return dal.Query(roleno);
        }

        /// <summary>
        /// 查询角色信息
        /// </summary>
        /// <returns></returns>
        public OperateResultModel QueryRoles()
        {
            SysRoleDal dal = new SysRoleDal();
            return dal.Query();
        }

        /// <summary>
        /// 插入角色信息
        /// </summary>
        /// <param name="role">角色实体</param>
        /// <returns></returns>
        public OperateResultModel InsertRole(sysrole role)
        {
            SysRoleDal dal = new SysRoleDal();
            return dal.Insert(role);
        }

        /// <summary>
        /// 更新角色信息
        /// </summary>
        /// <param name="role">角色实体</param>
        /// <returns></returns>
        public OperateResultModel UpdateRole(sysrole role)
        {
            SysRoleDal dal = new SysRoleDal();
            return dal.Update(role);
        }

        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="roleno">角色编号</param>
        /// <returns></returns>
        public OperateResultModel DeleteRole(string roleno)
        {
            SysRoleDal dal = new SysRoleDal();
            return dal.Delete(roleno);
        }
    }
}
