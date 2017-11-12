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
        /// 查询用户信息
        /// </summary>
        /// <param name="userno"></param>
        /// <returns></returns>
        public sysuser QueryUser(string userno)
        {
            SysUserDal dal = new SysUserDal();
            return dal.Query(userno);
        }

        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <param name="username">用户姓名</param>
        /// <param name="userno">用户编号</param>
        /// <param name="pm">分页信息</param>
        /// <returns></returns>
        public OperateResultModel QueryUsers(string userno, string username, PaginModel pm)
        {
            SysUserDal dal = new SysUserDal();
            return dal.Query(userno, username, pm);
        }

        /// <summary>
        /// 插入用户信息
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <returns></returns>
        public OperateResultModel InsertUser(sysuser user)
        {
            SysUserDal dal = new SysUserDal();
            return dal.Insert(user);
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <returns></returns>
        public OperateResultModel UpdateUser(sysuser user)
        {
            SysUserDal dal = new SysUserDal();
            return dal.Update(user);
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="userno">用户编号</param>
        /// <returns></returns>
        public OperateResultModel DeleteUser(string userno)
        {
            SysUserDal dal = new SysUserDal();
            return dal.Delete(userno);
        }

        /// <summary>
        /// 验证用户是否合法
        /// </summary>
        /// <param name="userno">用户编码</param>
        /// <param name="password">用户密码</param>
        /// <returns></returns>
        public OperateResultModel ValidUser(string userno, string password)
        {
            SysUserDal dal = new SysUserDal();
            return dal.ValidUser(userno, password);
        }

        /// <summary>
        /// 插入用户角色信息
        /// </summary>
        /// <param name="userno">用户编号</param>
        /// <param name="userroles">用户实体集</param>
        /// <returns></returns>
        public OperateResultModel InsertUserRoles(string userno, List<sysuser_role> userroles)
        {
            SysUserRoleDal dal = new SysUserRoleDal();
            return dal.Insert(userno, userroles);
        }

        /// <summary>
        /// 查询用户对应的角色信息
        /// </summary>
        /// <param name="userno">用户编号</param>
        /// <returns></returns>
        public OperateResultModel QueryUserRoels(string userno)
        {
            SysUserRoleDal dal = new SysUserRoleDal();
            return dal.Query(userno);
        }

        /// <summary>
        /// 查询用户所属的菜单信息
        /// </summary>
        /// <param name="userno">用户编码</param>
        /// <returns></returns>
        public List<sysmenu> QueryUserMenus(string userno)
        {
            SysUserDal dal = new SysUserDal();
            List<sysmenu> menus = dal.QueryMenus(userno);
            var topmenus = menus.Where(p => p.parentno == "0");
            foreach (sysmenu menu in menus)
            {
                BuildMenusTree(menus, menu);
            }
            return topmenus.ToList();
        }

        /// <summary>
        /// 获得用户对应菜单的权限
        /// </summary>
        /// <param name="userno">用户编号</param>
        /// <param name="menuno">菜单编号</param>
        /// <returns></returns>
        public List<syspower> QueryUserPower(string userno, string menuno)
        {
            SysUserDal dal = new SysUserDal();
            return dal.QueryUserPower(userno, menuno);
        }

        /// <summary>
        /// 建立菜单树
        /// </summary>
        /// <param name="menus">菜单集合</param>
        /// <param name="menu">当前菜单</param>
        public void BuildMenusTree(List<sysmenu> menus, sysmenu menu)
        {
            var submenus = menus.Where(p => p.parentno == menu.menuno);
            foreach (sysmenu sub in submenus)
            {
                BuildMenusTree(menus, sub);
            }
            menu.children = submenus.ToList();
        }
    }
}
