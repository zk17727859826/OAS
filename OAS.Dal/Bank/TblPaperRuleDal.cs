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
    public class TblPaperRuleDal : BaseDal<tbl_paper_rule>
    {
        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="pkid">键值</param>
        /// <returns></returns>
        public tbl_paper_rule Query(string pkid)
        {
            tbl_paper_rule model = new tbl_paper_rule() { pkid = pkid };
            model = QueryEntity(model);
            return model;
        }

        /// <summary>
        /// 查询数据信息
        /// </summary>
        /// <returns></returns>
        public OperateResultModel Query()
        {
            List<DBMemberEntity> entities = new List<DBMemberEntity>();
            OperateResultModel orm = Query(entities, "kemu", null);
            if (orm.rows != null)
            {
                orm.rows = ModelHelper.ToModel<List<tbl_paper_rule>>((DataTable)orm.rows);
            }
            return orm;
        }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public OperateResultModel Insert(tbl_paper_rule model)
        {
            return dbhelper.Insert(model);
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public OperateResultModel Update(tbl_paper_rule model)
        {
            return dbhelper.Update(model);
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="pkid">键值</param>
        /// <returns></returns>
        public OperateResultModel Delete(string pkid)
        {
            tbl_paper_rule model = new tbl_paper_rule() { pkid = pkid };
            return dbhelper.Delete(model);
        }
    }
}
