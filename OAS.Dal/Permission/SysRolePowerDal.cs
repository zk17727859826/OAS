using DBFactoryEntity;
using OAS.Model.Common;
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
    public class SysRolePowerDal : BaseDal<sysrole_menu>
    {
        /// <summary>
        /// 查询角色菜单信息为建立树
        /// </summary>
        /// <param name="roleno">角色编号</param>
        /// <param name="getall">是否获得所有菜单，在有权限的菜单上加勾</param>
        /// <returns></returns>
        public List<JsonTree> QueryMenusOfRoleForTree(string roleno, bool getall = true)
        {
            string strSQL = @"select t.*, isnull(a.roleno,'Null') haspower
                                from sysmenu t
                           left join sysrole_menu a
                                  on t.menuno = a.menuno
                                 and a.roleno = @roleno";
            List<DBMemberEntity> entities = new List<DBMemberEntity>();
            entities.AddMember("roleno", roleno);
            DataSet ds = dbhelper.Query(strSQL, entities);
            List<JsonTree> trees = new List<JsonTree>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
            {
                var topmenu = ds.Tables[0].AsEnumerable().Where(p => p.Field<string>("parentno") == "0");
                foreach (var row in topmenu)
                {
                    JsonTree treenode = new JsonTree()
                    {
                        id = row["menuno"] as string,
                        text = row["menuname"] as string,
                        Checked = row["haspower"].ToString() != "Null"
                    };
                    if (getall || (!getall && row["haspower"].ToString() != "Null"))
                    {
                        trees.Add(treenode);
                    }
                    else
                    {
                        continue;
                    }

                    BuildTree(ds.Tables[0], treenode, getall);
                }
            }
            return trees;
        }

        /// <summary>
        /// 构建树
        /// </summary>
        /// <param name="dt">所有数据</param>
        /// <param name="treenode">当前节点</param>
        /// <param name="getall">是否获得所有菜单，在有权限的菜单上加勾</param>
        private void BuildTree(DataTable dt, JsonTree treenode, bool getall = true)
        {
            var rows = dt.AsEnumerable().Where(p => p.Field<string>("parentno") == treenode.id);
            foreach (var row in rows)
            {
                JsonTree node = new JsonTree()
                {
                    id = row["menuno"] as string,
                    text = row["menuname"] as string,
                    Checked = row["haspower"].ToString() != "Null"
                };
                if (getall || (!getall && row["haspower"].ToString() != "Null"))
                {
                    treenode.children.Add(node);
                    treenode.Checked = false;
                }
                else
                {
                    continue;
                }
                BuildTree(dt, node, getall);
            }
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
        public OperateResultModel Insert(string roleno, string menuno, string powerno, List<sysrole_menu> rolemenus, List<sysrole_menu_power> rolepowers, List<sysrole_menu_power_object> roleobjects)
        {
            OperateResultModel orm;
            using (dbhelper)
            {
                List<DBMemberEntity> entities = new List<DBMemberEntity>();
                entities.AddMember("roleno", roleno);
                orm = Query(entities, null, null);
                DataTable oldrolemenus = orm.rows as DataTable;
                try
                {
                    foreach (sysrole_menu rolemenu in rolemenus)
                    {
                        if (oldrolemenus == null || oldrolemenus.AsEnumerable().Count(p => p.Field<string>("roleno") == rolemenu.roleno && p.Field<string>("menuno") == rolemenu.menuno) == 0)
                        {
                            if (!dbhelper.Insert(dbhelper.Connection(), dbhelper.Transaction(), rolemenu))
                            {
                                throw new Exception("插入角色对应菜单【" + rolemenu.menuno + "】失败");
                            }
                        }
                    }

                    if (oldrolemenus != null)
                    {
                        foreach (DataRow rolemenu in oldrolemenus.Rows)
                        {
                            if (rolemenus.Count(p => p.roleno == rolemenu["roleno"].ToString() && p.menuno == rolemenu["menuno"].ToString()) == 0)
                            {
                                sysrole_menu rolemenumodel = new sysrole_menu()
                                {
                                    pkid = rolemenu["pkid"].ToString()
                                };
                                if (dbhelper.Delete(dbhelper.Connection(), dbhelper.Transaction(), rolemenumodel) != 1)
                                {
                                    throw new Exception("删除角色对应菜单【" + rolemenu["menuno"] + "】失败");
                                }
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(menuno))
                    {
                        SysRoleMenuPowerDal rolepowerdal = new SysRoleMenuPowerDal();
                        rolepowerdal.InsertRolePowers(dbhelper, roleno, menuno, rolepowers);
                    }

                    if (!string.IsNullOrEmpty(menuno) && !string.IsNullOrEmpty(powerno))
                    {
                        SysRoleMenuPowerObjectDal roleobjectdal = new SysRoleMenuPowerObjectDal();
                        roleobjectdal.InsertRoleObjects(dbhelper, roleno, menuno, powerno, roleobjects);
                    }

                    //删除角色没有权限却有对象的数据
                    string strSQL = @"delete from sysrole_menu_power
                                       where not exists(
                                         select '*'
                                           from sysrole_menu a
                                          where a.roleno = sysrole_menu_power.roleno
                                            and a.menuno = sysrole_menu_power.menuno
                                       );
                                      delete from sysrole_menu_power_object
                                       where not exists(
                                      	select '*'
                                      	  from sysrole_menu_power a
                                      	 where a.roleno = sysrole_menu_power_object.roleno
                                      	   and a.menuno = sysrole_menu_power_object.menuno
                                      	   and a.powerno = sysrole_menu_power_object.powerno
                                       )";

                    dbhelper.Excute(dbhelper.Connection(), dbhelper.Transaction(), strSQL, null);

                    dbhelper.Transaction().Commit();
                    orm.rows = null;
                    orm.success = true;
                }
                catch (Exception ex)
                {
                    dbhelper.Transaction().Rollback();
                    orm.rows = null;
                    orm.success = false;
                    orm.message = ex.Message;
                }
            }

            return orm;
        }
    }
}
