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
    public class SysRoleDal : BaseDal<sysrole>
    {
        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="roleno">角色编号</param>
        /// <returns></returns>
        public sysrole Query(string roleno)
        {
            sysrole role = new sysrole() { roleno = roleno };
            role = QueryEntity(role);
            return role;
        }

        /// <summary>
        /// 查询数据信息
        /// </summary>
        /// <returns></returns>
        public OperateResultModel Query()
        {
            OperateResultModel orm = Query(null, null, null);
            if (orm.rows != null)
            {
                orm.rows = ModelHelper.ToModel<List<sysrole>>((DataTable)orm.rows);
            }
            return orm;
        }

        /// <summary>
        /// 插入用户信息
        /// </summary>
        /// <param name="role">角色实体</param>
        /// <returns></returns>
        public OperateResultModel Insert(sysrole role)
        {
            return dbhelper.Insert(role);
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="role">角色实体</param>
        /// <returns></returns>
        public OperateResultModel Update(sysrole role)
        {
            return dbhelper.Update(role);
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="roleno">角色编号</param>
        /// <returns></returns>
        public OperateResultModel Delete(string roleno)
        {
            sysrole role = new sysrole() { roleno = roleno };
            return dbhelper.Delete(role);
        }
    }
}
