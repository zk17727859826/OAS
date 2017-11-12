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
    public class TblXueyuanDal : BaseDal<tbl_xueyuan>
    {
        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="xueyh">学员号</param>
        /// <returns></returns>
        public tbl_xueyuan Query(string xueyh)
        {
            tbl_xueyuan model = new tbl_xueyuan() { xueyh = xueyh };
            model = QueryEntity(model);
            return model;
        }

        /// <summary>
        /// 查询数据信息
        /// </summary>
        /// <param name="xueyh">学员号</param>
        /// <param name="xingming">姓名</param>
        /// <param name="shenfhm">身份号码</param>
        /// <param name="pm">分页信息</param>
        /// <returns></returns>
        public OperateResultModel Query(string xueyh, string xingming, string shenfhm, PaginModel pm)
        {
            List<DBMemberEntity> entities = new List<DBMemberEntity>();
            if (!string.IsNullOrEmpty(xueyh))
            {
                entities.AddMember("xueyh", xueyh);
            }
            if (!string.IsNullOrEmpty(xingming))
            {
                entities.AddMember("xingming", xingming);
            }
            if (!string.IsNullOrEmpty(shenfhm))
            {
                entities.AddMember("shenfhm", shenfhm);
            }
            OperateResultModel orm = Query(entities, "xueyh desc", pm);
            if (orm.rows != null)
            {
                orm.rows = ModelHelper.ToModel<List<tbl_xueyuan>>((DataTable)orm.rows);
            }
            return orm;
        }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public OperateResultModel Insert(tbl_xueyuan model)
        {
            return dbhelper.Insert(model);
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public OperateResultModel Update(tbl_xueyuan model)
        {
            return dbhelper.Update(model);
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="xueyh">学员号</param>
        /// <returns></returns>
        public OperateResultModel Delete(string xueyh)
        {
            tbl_xueyuan model = new tbl_xueyuan() { xueyh = xueyh };
            return dbhelper.Delete(model);
        }
    }
}
