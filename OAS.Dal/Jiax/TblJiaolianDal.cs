using DBFactoryEntity;
using OAS.Model.Jiax;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tg.Helper;

namespace OAS.Dal.Jiax
{
    public class TblJiaolianDal : BaseDal<tbl_jiaolian>
    {
        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="jiaolh">教练号</param>
        /// <returns></returns>
        public tbl_jiaolian Query(string jiaolh)
        {
            tbl_jiaolian model = new tbl_jiaolian() { jiaolh = jiaolh };
            model = QueryEntity(model);
            return model;
        }

        /// <summary>
        /// 查询数据信息
        /// </summary>
        /// <param name="jiaolh">教练号</param>
        /// <param name="xingming">姓名</param>
        /// <param name="shenfhm">身份号码</param>
        /// <param name="pm">分页信息</param>
        /// <returns></returns>
        public OperateResultModel Query(string jiaolh, string xingming, string shenfhm, PaginModel pm)
        {
            List<DBMemberEntity> entities = new List<DBMemberEntity>();
            if (!string.IsNullOrEmpty(jiaolh))
            {
                entities.AddMember("jiaolh", jiaolh);
            }
            if (!string.IsNullOrEmpty(xingming))
            {
                entities.AddMember("xingming", xingming);
            }
            if (!string.IsNullOrEmpty(shenfhm))
            {
                entities.AddMember("shenfhm", shenfhm);
            }
            OperateResultModel orm = Query(entities, "jiaolh desc", pm);
            if (orm.rows != null)
            {
                orm.rows = ModelHelper.ToModel<List<tbl_jiaolian>>((DataTable)orm.rows);
            }
            return orm;
        }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public OperateResultModel Insert(tbl_jiaolian model)
        {
            return dbhelper.Insert(model);
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public OperateResultModel Update(tbl_jiaolian model)
        {
            return dbhelper.Update(model);
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="jiaolh">教练号</param>
        /// <returns></returns>
        public OperateResultModel Delete(string jiaolh)
        {
            tbl_jiaolian model = new tbl_jiaolian() { jiaolh = jiaolh };
            return dbhelper.Delete(model);
        }
    }
}
