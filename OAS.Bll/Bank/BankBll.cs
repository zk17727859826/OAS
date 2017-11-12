using DBFactoryEntity;
using OAS.Dal.Bank;
using OAS.Model.Bank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Bll
{
    public partial class Bank
    {
        /// <summary>
        /// 获得最新的题目ID
        /// </summary>
        /// <returns></returns>
        public int GetNewBankId()
        {
            TblBankDal dal = new TblBankDal();
            return dal.GetNewId();
        }

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="id">题目ID</param>
        /// <returns></returns>
        public tbl_bank QueryBank(int id)
        {
            TblBankDal dal = new TblBankDal();
            return dal.Query(id);
        }

        /// <summary>
        /// 查询数据信息
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="qtype">题型</param>
        /// <param name="ispic">是否图片题</param>
        /// <param name="isanimal">是否动画题</param>
        /// <param name="isorder">true：顺序  false：随机</param>
        /// <param name="kemu">科目</param>
        /// <param name="pm">分页实体</param>
        /// <returns></returns>
        public OperateResultModel QueryBanks(string title, string qtype, bool? ispic, bool? isanimal, bool isorder, string kemu, PaginModel pm)
        {
            TblBankDal dal = new TblBankDal();
            return dal.Query(title, qtype, ispic, isanimal, isorder, kemu, pm);
        }

        /// <summary>
        /// 查询未做题
        /// </summary>
        /// <param name="xueyh">学员号</param>
        /// <returns></returns>
        public OperateResultModel QueryBanksForNotStudy(string xueyh)
        {
            TblBankDal dal = new TblBankDal();
            return dal.QueryForNotStudy(xueyh);
        }

        /// <summary>
        /// 获得随机模拟试卷
        /// </summary>
        /// <param name="kemu">
        /// 科目
        /// KM1：科目一
        /// KM4：科目四
        /// KMA：客车
        /// KMB：货车
        /// </param>
        /// <returns></returns>
        public OperateResultModel QueryRandomPaper(string kemu)
        {
            TblBankDal dal = new TblBankDal();
            return dal.QueryRandomPaper(kemu);
        }

        /// <summary>
        /// 插入题目信息
        /// </summary>
        /// <param name="bank">题目实体</param>
        /// <returns></returns>
        public OperateResultModel InsertBank(tbl_bank bank)
        {
            TblBankDal dal = new TblBankDal();
            return dal.Insert(bank);
        }

        /// <summary>
        /// 更新题目信息
        /// </summary>
        /// <param name="bank">题目实体</param>
        /// <returns></returns>
        public OperateResultModel UpdateBank(tbl_bank bank)
        {
            TblBankDal dal = new TblBankDal();
            return dal.Update(bank);
        }

        /// <summary>
        /// 删除题目信息
        /// </summary>
        /// <param name="id">题目编号</param>
        /// <returns></returns>
        public OperateResultModel DeleteBank(int id)
        {
            TblBankDal dal = new TblBankDal();
            tbl_bank bank = new tbl_bank() { id = id };
            return dal.Delete(id);
        }

        /// <summary>
        /// 设置题目的错误类型
        /// </summary>
        /// <param name="id">题目编号</param>
        /// <param name="editer">操作人员</param>
        /// <returns></returns>
        public OperateResultModel SetErrorType(int id, string editer)
        {
            TblBankDal dal = new TblBankDal();
            return dal.SetErrorType(id, editer);
        }
    }
}
