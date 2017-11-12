using DBFactoryEntity;
using OAS.Model.Bank;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tg.Helper;

namespace OAS.Dal.Bank
{
    public class TblErrorBankDal : BaseDal<tbl_error_bank>
    {
        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="errorno">错误编号</param>
        /// <returns></returns>
        public tbl_error_bank Query(string errorno)
        {
            tbl_error_bank model = new tbl_error_bank() { errorno = errorno };
            model = QueryEntity(model);
            return model;
        }

        /// <summary>
        /// 查询数据信息
        /// </summary>
        /// <param name="errortype">错题类型</param>
        /// <param name="xueyh">学员号</param>
        /// <returns></returns>
        public OperateResultModel Query(string errortype, string xueyh)
        {
            List<DBMemberEntity> entities = new List<DBMemberEntity>();
            entities.AddMember("errortype", errortype);
            if (!string.IsNullOrEmpty(xueyh))
            {
                entities.AddMember("xueyh", xueyh);
            }
            OperateResultModel orm = Query(entities, "qid", null);
            if (orm.rows != null)
            {
                orm.rows = ModelHelper.ToModel<List<tbl_error_bank>>((DataTable)orm.rows);
            }
            return orm;
        }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public OperateResultModel Insert(tbl_error_bank model)
        {
            return dbhelper.Insert(model);
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public OperateResultModel Update(tbl_error_bank model)
        {
            return dbhelper.Update(model);
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="errorno">错误编号</param>
        /// <returns></returns>
        public OperateResultModel Delete(string errorno)
        {
            tbl_error_bank model = new tbl_error_bank() { errorno = errorno };
            return dbhelper.Delete(model);
        }
    }
}
