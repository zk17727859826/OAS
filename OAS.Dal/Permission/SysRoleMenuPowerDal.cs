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
    public class SysRoleMenuPowerDal : BaseDal<sysrole_menu_power>
    {
        /// <summary>
        /// 获得角色对应菜单的权限信息
        /// </summary>
        /// <param name="roleno">角色编号</param>
        /// <param name="menuno">菜单编号</param>
        /// <returns></returns>
        public List<syspower> QueryRolePowers(string roleno, string menuno)
        {
            string strSQL = @"select c.*,ISNULL(b.roleno,0) haspower
                                from sysmenu_power a
                                join syspower c
                                  on a.powerno = c.powerno
                                left join sysrole_menu_power b
                                  on a.menuno = b.menuno
                                 and a.powerno = b.powerno
                                 and b.roleno = @roleno
                               where a.menuno = @menuno";
            List<DBMemberEntity> entities = new List<DBMemberEntity>();
            entities.AddMember("roleno", roleno);
            entities.AddMember("menuno", menuno);
            DataSet ds = dbhelper.Query(strSQL, entities);
            List<syspower> powers = new List<syspower>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
            {
                powers = ModelHelper.ToModel<List<syspower>>(ds.Tables[0]);
            }
            return powers;
        }

        /// <summary>
        /// 添加角色对应的菜单的权限
        /// </summary>
        /// <param name="updbhelper">数据库连接对象</param>
        /// <param name="roleno">角色编号</param>
        /// <param name="menuno">角单编号</param>
        /// <param name="rolepowers">角色权限信息</param>
        /// <returns></returns>
        public bool InsertRolePowers(DBFactory updbhelper, string roleno, string menuno, List<sysrole_menu_power> rolepowers)
        {
            OperateResultModel orm = new OperateResultModel();
            List<DBMemberEntity> entities = new List<DBMemberEntity>();
            entities.AddMember("roleno", roleno);
            entities.AddMember("menuno", menuno);
            orm = Query(entities, null, null);
            DataTable oldrolemenus = orm.rows as DataTable;
            foreach (sysrole_menu_power rolepower in rolepowers)
            {
                if (oldrolemenus == null || oldrolemenus.AsEnumerable().Count(p => p.Field<string>("roleno") == rolepower.roleno && p.Field<string>("menuno") == rolepower.menuno && p.Field<string>("powerno") == rolepower.powerno) == 0)
                {
                    if (!updbhelper.Insert(updbhelper.Connection(), updbhelper.Transaction(), rolepower))
                    {
                        throw new Exception("插入角色对应菜单权限【" + rolepower.menuno + "|" + rolepower.powerno + "】失败");
                    }
                }
            }

            if (oldrolemenus != null)
            {
                foreach (DataRow rolepower in oldrolemenus.Rows)
                {
                    if (rolepowers.Count(p => p.roleno == rolepower["roleno"].ToString() && p.menuno == rolepower["menuno"].ToString() && p.powerno == rolepower["powerno"].ToString()) == 0)
                    {
                        sysrole_menu_power rolepowermodel = new sysrole_menu_power()
                        {
                            pkid = rolepower["pkid"].ToString()
                        };
                        if (updbhelper.Delete(updbhelper.Connection(), updbhelper.Transaction(), rolepowermodel) != 1)
                        {
                            throw new Exception("删除角色对应菜单【" + rolepower["menuno"] + "|" + rolepower["powerno"] + "】失败");
                        }
                    }
                }
            }

            return true;
        }
    }
}
