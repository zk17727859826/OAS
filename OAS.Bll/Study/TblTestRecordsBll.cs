using DBFactoryEntity;
using OAS.Dal.Study;
using OAS.Model.Study;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Bll
{
    public partial class Study
    {
        /// <summary>
        /// 查询练习记录
        /// </summary>
        /// <param name="testno">测试ID</param>
        /// <returns></returns>
        public tbl_test_records QueryTestRecord(string testno)
        {
            TblTestRecordsDal dal = new TblTestRecordsDal();
            return dal.Query(testno);
        }

        /// <summary>
        /// 查询练习记录
        /// </summary>
        /// <param name="xueyh">学员号</param>
        /// <param name="xingming">姓名</param>
        /// <returns></returns>
        public OperateResultModel QueryTestRecords(string xueyh, string xingming, PaginModel pm)
        {
            TblTestRecordsDal dal = new TblTestRecordsDal();
            return dal.Query(xueyh, xingming, pm);
        }

        /// <summary>
        /// 插入练习记录
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public OperateResultModel InsertTestRecord(tbl_test_records model)
        {
            TblTestRecordsDal dal = new TblTestRecordsDal();
            return dal.Insert(model);
        }

        /// <summary>
        /// 更新练习记录
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public OperateResultModel UpdateTestRecord(tbl_test_records model)
        {
            TblTestRecordsDal dal = new TblTestRecordsDal();
            return dal.Update(model);
        }

                /// <summary>
        /// 更新用户做题结果
        /// </summary>
        /// <param name="model">主表信息</param>
        /// <param name="qid">题目编号</param>
        /// <param name="answer">做题答案</param>
        /// <returns></returns>
        public OperateResultModel UpdateTestSelect(tbl_test_records model, int qid, string answer)
        {
            TblTestRecordsDal dal = new TblTestRecordsDal();
            return dal.Update(model, qid, answer);
        }

        /// <summary>
        /// 删除练习记录
        /// </summary>
        /// <param name="testno">编号</param>
        /// <returns></returns>
        public OperateResultModel DeleteTestRecord(string testno)
        {
            TblTestRecordsDal dal = new TblTestRecordsDal();
            return dal.Delete(testno);
        }
    }
}
