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
    public class SysUserDal : BaseDal<sysuser>
    {
        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="userno">用户编号</param>
        /// <returns></returns>
        public sysuser Query(string userno)
        {
            sysuser user = new sysuser() { userno = userno };
            user = QueryEntity(user);
            return user;
        }

        /// <summary>
        /// 查询数据信息
        /// </summary>
        /// <param name="userno">用户编号</param>
        /// <param name="username">用户姓名</param>
        /// <param name="pm">分页信息</param>
        /// <returns></returns>
        public OperateResultModel Query(string userno, string username, PaginModel pm)
        {
            List<DBMemberEntity> entitys = new List<DBMemberEntity>();
            if (!string.IsNullOrEmpty(userno))
            {
                entitys.AddMember("userno", userno, QueryTypeEnum.fruzz);
            }
            if (!string.IsNullOrEmpty(username))
            {
                entitys.AddMember("username", username, QueryTypeEnum.fruzz);
            }

            OperateResultModel orm = Query(entitys, "userno", pm);
            if (orm.rows != null)
            {
                orm.rows = ModelHelper.ToModel<List<sysuser>>((DataTable)orm.rows);
            }
            return orm;
        }

        /// <summary>
        /// 插入用户信息
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <returns></returns>
        public OperateResultModel Insert(sysuser user)
        {
            return dbhelper.Insert(user);
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <returns></returns>
        public OperateResultModel Update(sysuser user)
        {
            return dbhelper.Update(user);
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="userno">用户编号</param>
        /// <returns></returns>
        public OperateResultModel Delete(string userno)
        {
            sysuser user = new sysuser() { userno = userno };
            return dbhelper.Delete(user);
        }

        /// <summary>
        /// 查询用户所属的菜单信息
        /// </summary>
        /// <param name="userno">用户编码</param>
        /// <returns></returns>
        public List<sysmenu> QueryMenus(string userno)
        {
            SysUserRoleDal userroledal = new SysUserRoleDal();
            sysuser_role userrole = new sysuser_role() { userno = userno, roleno = "Administrator" };
            userrole = userroledal.QueryEntity(userrole);
            string strSQL = "select a.* from sysmenu a order by a.menusort";
            List<DBMemberEntity> entitys = new List<DBMemberEntity>();
            if (userrole == null)
            {
                strSQL = @"select a.*
                             from sysmenu a,sysuser_role b,sysrole_menu c
                            where a.menuno = c.menuno
                              and c.roleno = b.roleno
                              and b.userno = @userno
                            order by a.menusort";
                entitys.AddMember("userno", userno);
            }
            DataSet ds = dbhelper.Query(strSQL, entitys);
            List<sysmenu> menus = new List<sysmenu>();
            if (ds != null && ds.Tables.Count > 0)
            {
                menus = ModelHelper.ToModel<List<sysmenu>>(ds.Tables[0]);
            }
            return menus;
        }

        /// <summary>
        /// 获得用户对应菜单的权限
        /// </summary>
        /// <param name="userno">用户编号</param>
        /// <param name="menuno">菜单编号</param>
        /// <returns></returns>
        public List<syspower> QueryUserPower(string userno, string menuno)
        {
            SysUserRoleDal userroledal = new SysUserRoleDal();
            sysuser_role userrole = new sysuser_role() { userno = userno, roleno = "Administrator" };
            userrole = userroledal.QueryEntity(userrole);
            string strSQL = @"select a.* 
                                from syspower a,sysmenu_power b
                               where a.powerno = b.powerno
                                 and b.menuno = @menuno";
            List<DBMemberEntity> entitys = new List<DBMemberEntity>();
            if (userrole == null)
            {
                strSQL = @"select d.*
                             from syspower a,sysuser_role b,sysrole_menu c,sysrole_menu_power d
                            where b.roleno = c.roleno
                              and c.roleno = d.roleno
                              and c.menuno = d.menuno
                              and d.powerno = a.powerno
                              and b.userno = @userno
                              and c.menuno = @menuno";
                entitys.AddMember("userno", userno);
                entitys.AddMember("menuno", menuno);
            }
            else
            {
                entitys.AddMember("menuno", menuno);
            }
            DataSet ds = dbhelper.Query(strSQL, entitys);
            List<syspower> powers = new List<syspower>();
            if (ds != null && ds.Tables.Count > 0)
            {
                powers = ModelHelper.ToModel<List<syspower>>(ds.Tables[0]);
            }
            return powers;
        }

        /// <summary>
        /// 验证用户是否合法
        /// </summary>
        /// <param name="userno">用户编码</param>
        /// <param name="password">用户密码</param>
        /// <returns></returns>
        public OperateResultModel ValidUser(string userno, string password)
        {
            OperateResultModel orm = new OperateResultModel();
            try
            {
                sysuser user = Query(userno);
                if (user == null)
                {
                    throw new Exception("用户不存在");
                }

                if (user.password != password)
                {
                    throw new Exception("用户名或密码不正确");
                }

                if (user.isvalid != "Y")
                {
                    throw new Exception("用户已被禁用");
                }

                orm.success = true;
                orm.rows = user;
                orm.message = "验证成功";
            }
            catch (Exception ex)
            {
                orm.success = false;
                orm.message = ex.Message;
            }
            return orm;
        }
    }
}