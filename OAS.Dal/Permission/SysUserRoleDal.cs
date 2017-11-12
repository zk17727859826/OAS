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
    public class SysUserRoleDal : BaseDal<sysuser_role>
    {
        /// <summary>
        /// 查询数据信息
        /// </summary>
        /// <param name="userno">用户编号</param>
        /// <returns></returns>
        public OperateResultModel Query(string userno)
        {
            List<DBMemberEntity> entitys = new List<DBMemberEntity>();
            entitys.AddMember("userno", userno);

            OperateResultModel orm = Query(entitys, null, null);
            if (orm.rows != null)
            {
                orm.rows = ModelHelper.ToModel<List<sysuser_role>>((DataTable)orm.rows);
            }
            return orm;
        }

        /// <summary>
        /// 插入用户角色信息
        /// </summary>
        /// <param name="userno">用户编号</param>
        /// <param name="userroles">用户实体集</param>
        /// <returns></returns>
        public OperateResultModel Insert(string userno, List<sysuser_role> userroles)
        {
            OperateResultModel orm = new OperateResultModel();
            using (dbhelper)
            {
                dbhelper.Connection();
                List<DBMemberEntity> entities = new List<DBMemberEntity>();
                entities.AddMember("userno", userno);
                orm = Query(entities, null, null);
                DataTable oldrolemenus = orm.rows as DataTable;
                try
                {
                    foreach (sysuser_role userrole in userroles)
                    {
                        if (oldrolemenus == null || oldrolemenus.AsEnumerable().Count(p => p.Field<string>("userno") == userrole.userno && p.Field<string>("roleno") == userrole.roleno) == 0)
                        {
                            if (!dbhelper.Insert(dbhelper.Connection(), dbhelper.Transaction(), userrole))
                            {
                                throw new Exception("插入用户对应角色【" + userrole.roleno + "】失败");
                            }
                        }
                    }

                    if (oldrolemenus != null)
                    {
                        foreach (DataRow userrole in oldrolemenus.Rows)
                        {
                            if (userroles.Count(p => p.userno == userrole["userno"].ToString() && p.roleno == userrole["roleno"].ToString()) == 0)
                            {
                                sysuser_role userrolemodel = new sysuser_role()
                                {
                                    pkid = userrole["pkid"].ToString()
                                };
                                if (dbhelper.Delete(dbhelper.Connection(), dbhelper.Transaction(), userrolemodel) != 1)
                                {
                                    throw new Exception("删除角色对应角色【" + userrole["userno"] + "】失败");
                                }
                            }
                        }
                    }

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
