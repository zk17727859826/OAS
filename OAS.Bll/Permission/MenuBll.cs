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
        /// 查询菜单信息
        /// </summary>
        /// <param name="menuno">菜单编号</param>
        /// <returns></returns>
        public sysmenu QueryMenu(string menuno)
        {
            SysMenuDal dal = new SysMenuDal();
            return dal.Query(menuno);
        }

        /// <summary>
        /// 查询菜单信息
        /// </summary>
        /// <returns></returns>
        public OperateResultModel QueryMenus()
        {
            SysMenuDal dal = new SysMenuDal();
            return dal.Query();
        }

        /// <summary>
        /// 插入菜单信息
        /// </summary>
        /// <param name="menu">菜单实体</param>
        /// <returns></returns>
        public OperateResultModel InsertMenu(sysmenu menu)
        {
            SysMenuDal dal = new SysMenuDal();
            return dal.Insert(menu);
        }

        /// <summary>
        /// 更新角色信息
        /// </summary>
        /// <param name="menu">菜单实体</param>
        /// <returns></returns>
        public OperateResultModel UpdateMenu(sysmenu menu)
        {
            SysMenuDal dal = new SysMenuDal();
            return dal.Update(menu);
        }

        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="menuno">菜单编号</param>
        /// <returns></returns>
        public OperateResultModel DeleteMenu(string menuno)
        {
            SysMenuDal dal = new SysMenuDal();
            return dal.Delete(menuno);
        }
    }
}
