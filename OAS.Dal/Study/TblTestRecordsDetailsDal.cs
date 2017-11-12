using DBFactoryEntity;
using OAS.Model.Study;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tg.Helper;

namespace OAS.Dal.Study
{
    public class TblTestRecordsDetailsDal : BaseDal<tbl_test_records_details>
    {
        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="pkid">键值</param>
        /// <returns></returns>
        public tbl_test_records_details Query(string pkid)
        {
            tbl_test_records_details model = new tbl_test_records_details() { pkid = pkid };
            model = QueryEntity(model);
            return model;
        }

        /// <summary>
        /// 查询数据信息
        /// </summary>
        /// <param name="testno">测试编号</param>
        /// <returns></returns>
        public OperateResultModel Query(string testno, int? qid)
        {
            List<DBMemberEntity> entities = new List<DBMemberEntity>();
            entities.AddMember("testno", testno);
            if (qid != null)
            {
                entities.AddMember("qid", qid.Value);
            }
            OperateResultModel orm = Query(entities, "seq", null);
            if (orm.rows != null)
            {
                orm.rows = ModelHelper.ToModel<List<tbl_test_records_details>>((DataTable)orm.rows);
            }
            return orm;
        }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public OperateResultModel Insert(tbl_test_records_details model)
        {
            return dbhelper.Insert(model);
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public OperateResultModel Update(tbl_test_records_details model)
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
            tbl_test_records_details model = new tbl_test_records_details() { pkid = pkid };
            return dbhelper.Delete(model);
        }
    }
}
