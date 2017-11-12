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
    public class TblPaperDal : BaseDal<tbl_paper>
    {
        /// <summary>
        /// 获得最新的试卷ID
        /// </summary>
        /// <returns></returns>
        public int GetNewId()
        {
            string strSQL = @"select isnull(max(paperid),0) from tbl_paper t";
            DataSet ds = dbhelper.Query(strSQL, null);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                return Convert.ToInt32(dt.Rows[0][0]) + 1;
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="paperid">试卷名称</param>
        /// <returns></returns>
        public tbl_paper Query(int paperid)
        {
            tbl_paper model = new tbl_paper() { paperid = paperid };
            return QueryEntity(model);
        }

        /// <summary>
        /// 查询数据信息
        /// </summary>
        /// <param name="papername">试卷名称</param>
        /// <param name="pm">分页实体</param>
        /// <returns></returns>
        public OperateResultModel Query(string papername, PaginModel pm)
        {
            List<DBMemberEntity> members = new List<DBMemberEntity>();
            members.AddMember("papername", papername, QueryTypeEnum.fruzz);
            OperateResultModel orm = Query(members, "papername", pm);
            if (orm.rows != null)
            {
                orm.rows = ModelHelper.ToModel<List<tbl_paper>>((DataTable)orm.rows);
            }
            return orm;
        }

        /// <summary>
        /// 插入试卷信息
        /// </summary>
        /// <param name="paper">试卷实体</param>
        /// <returns></returns>
        public OperateResultModel Insert(tbl_paper paper)
        {
            return dbhelper.Insert(paper);
        }

        /// <summary>
        /// 更新试卷信息
        /// </summary>
        /// <param name="paper">试卷实体</param>
        /// <returns></returns>
        public OperateResultModel Update(tbl_paper paper)
        {
            return dbhelper.Update(paper);
        }

        /// <summary>
        /// 删除试卷信息
        /// </summary>
        /// <param name="id">试卷编号</param>
        /// <returns></returns>
        public OperateResultModel Delete(int id)
        {
            using (dbhelper)
            {
                try
                {
                    dbhelper.Connection();
                    dbhelper.Transaction();
                    tbl_paper bank = new tbl_paper() { paperid = id };
                    if (dbhelper.Delete(dbhelper.Connection(), dbhelper.Transaction(), bank) != 1)
                    {
                        throw new Exception("删除试卷失败");
                    }

                    TblPaperBankDal paperbankdal = new TblPaperBankDal();
                    paperbankdal.Delete(dbhelper, id, null);
                    dbhelper.Transaction().Commit();
                    return new OperateResultModel()
                    {
                        success = true
                    };
                }
                catch (Exception)
                {
                    dbhelper.Transaction().Rollback();
                    throw;
                }
            }
            
        }
    }
}
