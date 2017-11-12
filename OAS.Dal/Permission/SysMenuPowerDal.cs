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
    public class SysMenuPowerDal : BaseDal<sysmenu_power>
    {
        /// <summary>
        /// 查询数据信息
        /// </summary>
        /// <param name="menuno">菜单编号</param>
        /// <returns></returns>
        public OperateResultModel Query(string menuno)
        {
            OperateResultModel orm = new OperateResultModel();
            try
            {
                List<DBMemberEntity> entities = new List<DBMemberEntity>();
                entities.AddMember("menuno", menuno);
                string strSQL = @"select a.*, b.menuno, b.pkid
                                from syspower a,sysmenu_power b
                               where a.powerno = b.powerno
                                 and b.menuno = @menuno";
                DataSet ds = dbhelper.Query(strSQL, entities);
                orm.success = true;
                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0] == null)
                {
                    orm.total = 0;
                    orm.rows = new List<sysmenu_power>();
                }
                else
                {
                    orm.total = ds.Tables[0].Rows.Count;
                    orm.rows = ModelHelper.ToModel<List<sysmenu_power>>(ds.Tables[0]);
                }
            }
            catch (Exception ex)
            {
                orm.success = false;
                orm.message = ex.Message;
            }
            return orm;
        }

        /// <summary>
        /// 插入菜单权限信息
        /// </summary>
        /// <param name="menupower">菜单权限实体</param>
        /// <returns></returns>
        public OperateResultModel Insert(sysmenu_power menupower)
        {
            return dbhelper.Insert(menupower);
        }

        /// <summary>
        /// 删除菜单权限信息
        /// </summary>
        /// <param name="pkid">键值</param>
        /// <returns></returns>
        public OperateResultModel Delete(string pkid)
        {
            sysmenu_power menupower = new sysmenu_power() { pkid = pkid };
            List<DBMemberEntity> entities = new List<DBMemberEntity>();
            return dbhelper.Delete(menupower);
        }
    }
}
