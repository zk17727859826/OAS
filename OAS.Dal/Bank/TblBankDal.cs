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
    public class TblBankDal : BaseDal<tbl_bank>
    {
        /// <summary>
        /// 获得最新的题目ID
        /// </summary>
        /// <returns></returns>
        public int GetNewId()
        {
            string strSQL = @"select max(id) from tbl_bank t";
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
        /// <param name="id">题目ID</param>
        /// <returns></returns>
        public tbl_bank Query(int id)
        {
            string strSQL = @"select t.*, a.sectionname 
                                from tbl_bank t, tbl_section a 
                               where t.section = a.sid
                                 and t.id = @id";
            List<DBMemberEntity> entities = new List<DBMemberEntity>();
            entities.AddMember("id", id);
            DataSet ds = dbhelper.Query(strSQL, entities);
            tbl_bank bank = null;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                bank = ModelHelper.ToModel<tbl_bank>(ds.Tables[0].Rows[0]);
            }
            return bank;
        }

        /// <summary>
        /// 查询数据信息
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="qtype">题型</param>
        /// <param name="ispic">是否图片题</param>
        /// <param name="isanimal">是否动画题</param>
        /// <param name="isOrder">true:顺序 false:随机</param>
        /// <param name="kemu">
        /// 科目
        /// KM1：科目一
        /// KM4：科目四
        /// KMA：客车
        /// KMB：货车
        /// </param>
        /// <param name="pm">分页实体</param>
        /// <returns></returns>
        public OperateResultModel Query(string title, string qtype, bool? ispic, bool? isanimal, bool isOrder, string kemu, PaginModel pm)
        {
            List<DBMemberEntity> members = new List<DBMemberEntity>();
            members.AddMember("title", title, QueryTypeEnum.fruzz);
            if (!string.IsNullOrEmpty(qtype))
            {
                members.AddMember("qtype", qtype);
            }
            if (ispic != null)
            {
                members.AddMember("picpath", "", QueryTypeEnum.notequal);
            }
            if (isanimal != null)
            {
                members.AddMember("animepath", "", QueryTypeEnum.notequal);
            }
            if (!string.IsNullOrEmpty(kemu))
            {
                switch (kemu)
                {
                    case "KM1":
                        kemu = "C";
                        break;
                    case "KM4":
                        kemu = "D";
                        break;
                    case "KMA":
                        kemu = "A";
                        break;
                    case "KMB":
                        kemu = "B";
                        break;
                    default:
                        break;
                }
                members.AddMember("belongtype", kemu, QueryTypeEnum.fruzz);
            }
            string orderby = "id";
            if (!isOrder)
            {
                orderby = " NEWID() ";
            }
            OperateResultModel orm = Query(members, orderby, pm);
            if (orm.rows != null)
            {
                orm.rows = ModelHelper.ToModel<List<tbl_bank>>((DataTable)orm.rows);
            }
            return orm;
        }

        /// <summary>
        /// 查询未做题
        /// </summary>
        /// <param name="xueyh">学员号</param>
        /// <returns></returns>
        public OperateResultModel QueryForNotStudy(string xueyh)
        {
            OperateResultModel orm = new OperateResultModel();
            try
            {
                string strSQL = @"select a.*
                                    from tbl_bank a 
                                    left join (select distinct b.qid 
                                                 from tbl_test_records c, tbl_test_records_details b
                                                where c.testno = b.testno
                                                  and c.xueyh = @xueyh) BB
                                    on a.id=BB.qid 
                                   where isnull(BB.qid,0) = 0";
                List<DBMemberEntity> entities = new List<DBMemberEntity>();
                entities.AddMember("xueyh", xueyh);
                DataSet ds = dbhelper.Query(strSQL, entities);
                orm.rows = ModelHelper.ToModel<List<tbl_bank>>(ds.Tables[0]);
                orm.success = true;
            }
            catch (Exception ex)
            {
                orm.success = false;
                orm.message = ex.Message;
            }
            return orm;
        }

        /// <summary>
        /// 获得随机模拟试卷
        /// </summary>
        /// <returns></returns>
        public OperateResultModel QueryRandomPaper(string kemu)
        {
            OperateResultModel orm = new OperateResultModel();
            try
            {
                string strSQL = "select * from tbl_paper_rule t where t.kemu=@kemu";
                List<DBMemberEntity> entities = new List<DBMemberEntity>();
                entities.AddMember("kemu", kemu);
                DataSet dsset = dbhelper.Query(strSQL, entities);
                if (dsset == null || dsset.Tables.Count == 0 || dsset.Tables[0] == null || dsset.Tables[0].Rows.Count == 0)
                {
                    throw new Exception("科目比例没有设置");
                }

                switch (kemu)
                {
                    case "KM1":
                        kemu = "C";
                        break;
                    case "KM4":
                        kemu = "D";
                        break;
                    case "KMA":
                        kemu = "A";
                        break;
                    case "KMB":
                        kemu = "B";
                        break;
                    default:
                        break;
                }

                strSQL = @"select top(@num) t.*
                             from tbl_bank t
                            where t.qtype = @qtype
                              and t.belongtype like '%'+@belongtype+'%'
                            order by NEWID()";
                entities.Clear();
                entities.AddMember("num", Convert.ToInt32(dsset.Tables[0].Rows[0]["single"]));
                entities.AddMember("belongtype", kemu);
                entities.AddMember("qtype", "单选题");
                DataSet ds = new DataSet();
                ds = dbhelper.Query(strSQL, entities);
                List<tbl_bank> results = new List<tbl_bank>();
                results.AddRange(ModelHelper.ToModel<List<tbl_bank>>(ds.Tables[0]));

                entities.Clear();
                entities.AddMember("num", Convert.ToInt32(dsset.Tables[0].Rows[0]["judge"]));
                entities.AddMember("qtype", "判断题");
                entities.AddMember("belongtype", kemu);
                ds = dbhelper.Query(strSQL, entities);
                results.AddRange(ModelHelper.ToModel<List<tbl_bank>>(ds.Tables[0]));

                entities.Clear();
                entities.AddMember("num", Convert.ToInt32(dsset.Tables[0].Rows[0]["multi"]));
                entities.AddMember("qtype", "多选题");
                entities.AddMember("belongtype", kemu);
                ds = dbhelper.Query(strSQL, entities);
                results.AddRange(ModelHelper.ToModel<List<tbl_bank>>(ds.Tables[0]));

                orm.rows = results;
                orm.total = results.Count;
                orm.success = true;
            }
            catch (Exception ex)
            {
                orm.success = false;
                orm.message = ex.Message;
            }
            return orm;
        }

        /// <summary>
        /// 插入题目信息
        /// </summary>
        /// <param name="bank">题目实体</param>
        /// <returns></returns>
        public OperateResultModel Insert(tbl_bank bank)
        {
            return dbhelper.Insert(bank);
        }

        /// <summary>
        /// 更新题目信息
        /// </summary>
        /// <param name="bank">题目实体</param>
        /// <returns></returns>
        public OperateResultModel Update(tbl_bank bank)
        {
            return dbhelper.Update(bank);
        }

        /// <summary>
        /// 删除题目信息
        /// </summary>
        /// <param name="id">题目编号</param>
        /// <returns></returns>
        public OperateResultModel Delete(int id)
        {
            using (dbhelper)
            {
                try
                {
                    dbhelper.Connection();
                    dbhelper.Transaction();

                    tbl_bank bank = new tbl_bank() { id = id };
                    if (dbhelper.Delete(dbhelper.Connection(), dbhelper.Transaction(), bank) != 1)
                    {
                        throw new Exception("删除题目失败");
                    }

                    TblPaperBankDal paperbankdal = new TblPaperBankDal();
                    paperbankdal.Delete(dbhelper, null, id);

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

        /// <summary>
        /// 设置题目的错误类型
        /// </summary>
        /// <param name="id">题目编号</param>
        /// <param name="editer">操作人员</param>
        /// <returns></returns>
        public OperateResultModel SetErrorType(int id, string editer)
        {
            OperateResultModel orm = new OperateResultModel();
            try
            {
                tbl_bank model = Query(id);
                if (model == null)
                {
                    throw new Exception("题目不存在");
                }
                if (model.errortype == "A")
                {
                    model.errortype = "";
                }
                else
                {
                    model.errortype = "A";
                }
                model.editdate = DateTime.Now;
                model.editer = editer;
                model.sectionname = null;
                orm = dbhelper.Update(model);
            }
            catch (Exception ex)
            {
                orm.success = false;
                orm.message = ex.Message;
            }
            return orm;
        }
    }
}
