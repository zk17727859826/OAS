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
    public class TblTestRecordsDal : BaseDal<tbl_test_records>
    {
        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="testno">测试ID</param>
        /// <returns></returns>
        public tbl_test_records Query(string testno)
        {
            tbl_test_records jiaxmodel = new tbl_test_records() { testno = testno };
            jiaxmodel = QueryEntity(jiaxmodel);
            return jiaxmodel;
        }

        /// <summary>
        /// 查询数据信息
        /// </summary>
        /// <param name="xueyh">学员号</param>
        /// <param name="xingming">姓名</param>
        /// <returns></returns>
        public OperateResultModel Query(string xueyh, string xingming, PaginModel pm)
        {
            List<DBMemberEntity> entities = new List<DBMemberEntity>();
            if (!string.IsNullOrEmpty(xueyh))
            {
                entities.AddMember("xueyh", xueyh, QueryTypeEnum.fruzz);
            }
            if (!string.IsNullOrEmpty(xingming))
            {
                entities.AddMember("xingming", xingming, QueryTypeEnum.fruzz);
            }
            OperateResultModel orm = Query(entities, "createdate desc", pm);
            if (orm.rows != null)
            {
                orm.rows = ModelHelper.ToModel<List<tbl_test_records>>((DataTable)orm.rows);
            }
            return orm;
        }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public OperateResultModel Insert(tbl_test_records model)
        {
            OperateResultModel orm = new OperateResultModel();
            using (dbhelper)
            {
                try
                {
                    bool bolSuccess = dbhelper.Insert(dbhelper.Connection(), dbhelper.Transaction(), model);
                    if (bolSuccess)
                    {
                        if (model.details != null && model.details.Count > 0)
                        {
                            foreach (tbl_test_records_details tb in model.details)
                            {
                                if (!dbhelper.Insert(dbhelper.Connection(), dbhelper.Transaction(), tb))
                                {
                                    throw new Exception("插入练习试题失败");
                                }
                            }
                        }
                    }

                    orm.success = true;
                    dbhelper.Transaction().Commit();
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
        /// 更新信息
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public OperateResultModel Update(tbl_test_records model)
        {
            return dbhelper.Update(model);
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="testno">编号</param>
        /// <returns></returns>
        public OperateResultModel Delete(string testno)
        {
            tbl_test_records model = new tbl_test_records() { testno = testno };
            return dbhelper.Delete(model);
        }

        /// <summary>
        /// 更新用户做题结果
        /// </summary>
        /// <param name="model">主表信息</param>
        /// <param name="qid">题目键值</param>
        /// <param name="answer">做题答案</param>
        /// <returns></returns>
        public OperateResultModel Update(tbl_test_records model, int qid, string answer)
        {
            OperateResultModel orm = new OperateResultModel();
            TblTestRecordsDetailsDal detailsdal = new TblTestRecordsDetailsDal();
            tbl_test_records_details detail = new tbl_test_records_details()
            {
                testno = model.testno,
                qid = qid
            };
            detail = detailsdal.QueryEntity(detail);
            using (dbhelper)
            {
                try
                {
                    dbhelper.Update(dbhelper.Connection(), dbhelper.Transaction(), model);
                    if (detail == null)
                    {
                        detail = new tbl_test_records_details()
                        {
                            testno = model.testno,
                            pkid = Guid.NewGuid().ToString().Replace("-", "").ToUpper(),
                            qid = qid,
                            seq = 1,
                            answer = answer
                        };
                        if (dbhelper.Insert(dbhelper.Connection(), dbhelper.Transaction(), detail) == false)
                        {
                            throw new Exception("保存选择结果失败");
                        }
                    }
                    else
                    {
                        detail = new tbl_test_records_details()
                        {
                            pkid = detail.pkid,
                            answer = answer
                        };
                        if (dbhelper.Update(dbhelper.Connection(), dbhelper.Transaction(), detail) != 1)
                        {
                            throw new Exception("保存选择结果失败");
                        }
                    }

                    dbhelper.Transaction().Commit();
                }
                catch (Exception ex)
                {
                    dbhelper.Transaction().Rollback();
                    orm.message = ex.Message;
                    orm.success = false;
                }
            }

            return orm;
        }
    }
}
