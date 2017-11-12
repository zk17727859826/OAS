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
        /// 查询菜单权限对应的对象
        /// </summary>
        /// <param name="menuno">菜单编号</param>
        /// <returns></returns>
        public OperateResultModel QueryMenuObject(string menuno)
        {
            SysMenuObjectDal dal = new SysMenuObjectDal();
            return dal.Query(menuno);
        }

        /// <summary>
        /// 插入菜单权限信息
        /// </summary>
        /// <param name="menuobject">菜单对象的对象</param>
        /// <returns></returns>
        public OperateResultModel InsertMenuObject(sysmenu_object menuobject)
        {
            SysMenuObjectDal dal = new SysMenuObjectDal();
            return dal.Insert(menuobject);
        }

        /// <summary>
        /// 删除菜单权限信息
        /// </summary>
        /// <param name="pkid">键值</param>
        /// <returns></returns>
        public OperateResultModel DeleteMenuObject(string pkid)
        {
            SysMenuObjectDal dal = new SysMenuObjectDal();
            return dal.Delete(pkid);
        }
    }
}
