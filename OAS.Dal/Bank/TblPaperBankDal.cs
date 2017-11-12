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
    public class TblPaperBankDal : BaseDal<tbl_paper_bank>
    {
        /// <summary>
        /// 查询指定试卷对应的题目编号
        /// </summary>
        /// <param name="paperid">试卷ID</param>
        /// <returns></returns>
        public OperateResultModel Query(int paperid)
        {
            List<DBMemberEntity> members = new List<DBMemberEntity>();
            members.AddMember("paperid", paperid);
            return Query(members, "bankid", null);
        }

        /// <summary>
        /// 查询对应题目
        /// </summary>
        /// <param name="paperid"></param>
        /// <returns></returns>
        public OperateResultModel QueryBanks(int paperid)
        {
            OperateResultModel orm = new OperateResultModel();
            try
            {
                List<DBMemberEntity> members = new List<DBMemberEntity>();
                members.AddMember("paperid", paperid);
                string strSQL = @"select a.* 
                                    from tbl_paper_bank T ,tbl_bank a
                                   where t.bankid = a.id
                                     and T.paperid = @paperid";
                DataSet dt = dbhelper.Query(strSQL, members);
                if (dt != null && dt.Tables.Count > 0 && dt.Tables[0] != null)
                {
                    orm.success = true;
                    orm.rows = ModelHelper.ToModel<List<tbl_bank>>(dt.Tables[0]);
                    orm.total = dt.Tables[0].Rows.Count;
                }
                else
                {
                    throw new Exception("查询结果为0");
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
        /// 插入试卷与题目的对应关系
        /// </summary>
        /// <param name="paperid">试卷ID</param>
        /// <param name="bankidlst">题目ID列表</param>
        /// <param name="creater">创建人</param>
        /// <returns></returns>
        public OperateResultModel Insert(int paperid, List<int> bankidlst, string creater)
        {
            if (bankidlst == null || bankidlst.Count == 0)
            {
                throw new Exception("请选择题目");
            }
            DateTime dtnow = DateTime.Now;
            //查询指定试卷原有的题目
            OperateResultModel orm = Query(paperid);
            DataTable dt = orm.rows as DataTable;
            List<tbl_paper_bank> paperbanklst = ModelHelper.ToModel<List<tbl_paper_bank>>(dt);
            orm.rows = null;
            using (dbhelper)
            {
                try
                {
                    dbhelper.Connection();
                    dbhelper.Transaction();

                    foreach (int bankid in bankidlst)
                    {
                        if (paperbanklst == null || paperbanklst.Count(p => p.bankid == bankid) == 0)
                        {
                            tbl_paper_bank paperbank = new tbl_paper_bank()
                            {
                                bankid = bankid,
                                paperid = paperid,
                                createdate = dtnow,
                                creater = creater,
                                pkid = Guid.NewGuid().ToString().ToUpper().Replace("-", "")
                            };
                            dbhelper.Insert(dbhelper.Connection(), dbhelper.Transaction(), paperbank);
                        }
                    }

                    if (paperbanklst != null)
                    {
                        int iCount = 0;
                        foreach (tbl_paper_bank paperbank in paperbanklst)
                        {
                            if (bankidlst.Count(p => p == paperbank.bankid) == 0)
                            {
                                paperbank.createdate = null;
                                iCount = dbhelper.Delete(dbhelper.Connection(), dbhelper.Transaction(), paperbank);
                                if (iCount != 1)
                                {
                                    throw new Exception("更新试卷的题目失败");
                                }
                            }
                        }
                    }

                    dbhelper.Transaction().Commit();
                    orm.success = true;
                }
                catch (Exception ex)
                {
                    dbhelper.Transaction().Rollback();
                    orm.success = false;
                    orm.message = ex.Message;
                }
            }
            return orm;
        }

        /// <summary>
        /// 删除试卷与题目的对应关系
        /// </summary>
        /// <param name="factory">数据库连接工厂</param>
        /// <param name="paperid">试卷ID</param>
        /// <param name="bankid">题目ID</param>
        /// <returns></returns>
        public bool Delete(DBFactory factory, int? paperid, int? bankid)
        {
            tbl_paper_bank model = new tbl_paper_bank() { };

            if (paperid == null && bankid == null)
            {
                throw new Exception("试卷ID和题目ID不能同时为空");
            }

            if (paperid != null)
            {
                model.paperid = paperid;
            }

            if (bankid != null)
            {
                model.bankid = bankid;
            }
            factory.Delete(factory.Connection(), factory.Transaction(), model);
            return true;
        }
    }
}
