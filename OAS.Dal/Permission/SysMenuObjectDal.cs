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
    public class SysMenuObjectDal : BaseDal<sysmenu_object>
    {
        /// <summary>
        /// 查询数据信息
        /// </summary>
        /// <param name="menuno">菜单编号</param>
        /// <returns></returns>
        public OperateResultModel Query(string menuno)
        {
            List<DBMemberEntity> entities = new List<DBMemberEntity>();
            entities.AddMember("menuno", menuno);
            OperateResultModel orm = Query(entities, null, null);
            orm.rows = ModelHelper.ToModel<List<sysmenu_object>>((DataTable)orm.rows);
            return orm;
        }

        /// <summary>
        /// 插入菜单权限信息
        /// </summary>
        /// <param name="menuobject">菜单对象的对象</param>
        /// <returns></returns>
        public OperateResultModel Insert(sysmenu_object menuobject)
        {
            return dbhelper.Insert(menuobject);
        }

        /// <summary>
        /// 删除菜单权限信息
        /// </summary>
        /// <param name="pkid">键值</param>
        /// <returns></returns>
        public OperateResultModel Delete(string pkid)
        {
            sysmenu_object menuobject = new sysmenu_object() { pkid = pkid };
            List<DBMemberEntity> entities = new List<DBMemberEntity>();
            return dbhelper.Delete(menuobject);
        }
    }
}
