using DBFactoryEntity;
using OAS.Model.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tg.Helper;

namespace OAS.Dal.Common
{
    public class TblCheXingDal : BaseDal<tbl_chexing>
    {
        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="chexing">车型编号</param>
        /// <returns></returns>
        public tbl_chexing Query(string chexing)
        {
            tbl_chexing chexingmodel = new tbl_chexing() { chexing = chexing };
            chexingmodel = QueryEntity(chexingmodel);
            return chexingmodel;
        }

        /// <summary>
        /// 查询数据信息
        /// </summary>
        /// <returns></returns>
        public OperateResultModel Query()
        {
            List<DBMemberEntity> entities = new List<DBMemberEntity>();
            OperateResultModel orm = Query(entities, "chexing", null);
            if (orm.rows != null)
            {
                orm.rows = ModelHelper.ToModel<List<tbl_chexing>>((DataTable)orm.rows);
            }
            return orm;
        }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public OperateResultModel Insert(tbl_chexing model)
        {
            return dbhelper.Insert(model);
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public OperateResultModel Update(tbl_chexing model)
        {
            return dbhelper.Update(model);
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="chexing">编号</param>
        /// <returns></returns>
        public OperateResultModel Delete(string chexing)
        {
            tbl_chexing model = new tbl_chexing() { chexing = chexing };
            return dbhelper.Delete(model);
        }
    }
}
