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
    public class SysRoleMenuPowerObjectDal : BaseDal<sysrole_menu_power_object>
    {
        /// <summary>
        /// 获得角色对应菜单的权限对应的对象信息
        /// </summary>
        /// <param name="roleno">角色编号</param>
        /// <param name="menuno">菜单编号</param>
        /// <param name="powerno">权限编号</param>
        /// <returns></returns>
        public List<sysmenu_object> QueryRoleObjects(string roleno, string menuno, string powerno)
        {
            string strSQL = @"select a.*,ISNULL(b.roleno,0) haspower
                                from sysmenu_object a
                                left join sysrole_menu_power_object b
                                  on a.menuno = b.menuno
                                 and a.forobject = b.forobject
                                 and b.powerno = @powerno
                                 and b.roleno = @roleno
                               where a.menuno = @menuno";
            List<DBMemberEntity> entities = new List<DBMemberEntity>();
            entities.AddMember("roleno", roleno);
            entities.AddMember("menuno", menuno);
            entities.AddMember("powerno", powerno);
            DataSet ds = dbhelper.Query(strSQL, entities);
            List<sysmenu_object> powers = new List<sysmenu_object>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
            {
                powers = ModelHelper.ToModel<List<sysmenu_object>>(ds.Tables[0]);
            }
            return powers;
        }

        /// <summary>
        /// 添加角色对应的菜单的权限
        /// </summary>
        /// <param name="updbhelper">数据库连接对象</param>
        /// <param name="roleno">角色编号</param>
        /// <param name="menuno">角单编号</param>
        /// <param name="powerno">权限编号</param>
        /// <param name="roleobjects">角色权限信息</param>
        /// <returns></returns>
        public bool InsertRoleObjects(DBFactory updbhelper, string roleno, string menuno, string powerno, List<sysrole_menu_power_object> roleobjects)
        {
            OperateResultModel orm = new OperateResultModel();
            List<DBMemberEntity> entities = new List<DBMemberEntity>();
            entities.AddMember("roleno", roleno);
            entities.AddMember("menuno", menuno);
            entities.AddMember("powerno", powerno);
            orm = Query(entities, null, null);
            DataTable oldroleobjects = orm.rows as DataTable;
            foreach (sysrole_menu_power_object roleobject in roleobjects)
            {
                if (oldroleobjects == null || oldroleobjects.AsEnumerable().Count(p => p.Field<string>("roleno") == roleobject.roleno && p.Field<string>("menuno") == roleobject.menuno && p.Field<string>("powerno") == roleobject.powerno && p.Field<string>("forobject") == roleobject.forobject) == 0)
                {
                    if (!updbhelper.Insert(updbhelper.Connection(), updbhelper.Transaction(), roleobject))
                    {
                        throw new Exception("插入角色对应菜单权限【" + roleobject.menuno + "|" + roleobject.powerno + "|" + roleobject.forobject + "】失败");
                    }
                }
            }

            if (oldroleobjects != null)
            {
                foreach (DataRow roleobject in oldroleobjects.Rows)
                {
                    if (roleobjects.Count(p => p.roleno == roleobject["roleno"].ToString() && p.menuno == roleobject["menuno"].ToString() && p.powerno == roleobject["powerno"].ToString() && p.forobject == roleobject["forobject"].ToString()) == 0)
                    {
                        sysrole_menu_power_object roleobjectmodel = new sysrole_menu_power_object()
                        {
                            pkid = roleobject["pkid"].ToString()
                        };
                        if (updbhelper.Delete(updbhelper.Connection(), updbhelper.Transaction(), roleobjectmodel) != 1)
                        {
                            throw new Exception("删除角色对应菜单【" + roleobject["menuno"] + "|" + roleobject["powerno"] + "|" + roleobject["forobject"] + "】失败");
                        }
                    }
                }
            }

            return true;
        }
    }
}
