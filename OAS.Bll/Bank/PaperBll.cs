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
        public int GetNewPaperId()
        {
            TblPaperDal dal = new TblPaperDal();
            return dal.GetNewId();
        }

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="id">题目ID</param>
        /// <returns></returns>
        public tbl_paper QueryPaper(int id)
        {
            TblPaperDal dal = new TblPaperDal();
            return dal.Query(id);
        }

        /// <summary>
        /// 查询数据信息
        /// </summary>
        /// <param name="papername">试卷标题</param>
        /// <param name="pm">分页实体</param>
        /// <returns></returns>
        public OperateResultModel QueryPapers(string papername, PaginModel pm)
        {
            TblPaperDal dal = new TblPaperDal();
            return dal.Query(papername, pm);
        }

        /// <summary>
        /// 插入试卷信息
        /// </summary>
        /// <param name="paper">试卷实体</param>
        /// <returns></returns>
        public OperateResultModel InsertPaper(tbl_paper paper)
        {
            TblPaperDal dal = new TblPaperDal();
            return dal.Insert(paper);
        }

        /// <summary>
        /// 更新试卷信息
        /// </summary>
        /// <param name="paper">试卷实体</param>
        /// <returns></returns>
        public OperateResultModel UpdatePaper(tbl_paper paper)
        {
            TblPaperDal dal = new TblPaperDal();
            return dal.Update(paper);
        }

        /// <summary>
        /// 删除试卷信息
        /// </summary>
        /// <param name="id">试卷编号</param>
        /// <returns></returns>
        public OperateResultModel DeletePaper(int id)
        {
            TblPaperDal dal = new TblPaperDal();
            return dal.Delete(id);
        }

        /// <summary>
        /// 查询试卷对应的题目
        /// </summary>
        /// <param name="paperid">试卷ID</param>
        /// <returns></returns>
        public OperateResultModel QueryPaperBanks(int paperid)
        {
            TblPaperBankDal paperbankdal = new TblPaperBankDal();
            return paperbankdal.Query(paperid);
        }

        /// <summary>
        /// 查询试卷对应的题目(内容为题库信息)
        /// </summary>
        /// <param name="paperid">试卷ID</param>
        /// <returns></returns>
        public OperateResultModel QueryBanksOfPaper(int paperid)
        {
            TblPaperBankDal paperbankdal = new TblPaperBankDal();
            return paperbankdal.QueryBanks(paperid);
        }

        /// <summary>
        /// 保存试卷题目
        /// </summary>
        /// <param name="paperid">试卷ID</param>
        /// <param name="bankids">题目IDs</param>
        /// <param name="creater">创建人</param>
        /// <returns></returns>
        public OperateResultModel InsertPaperBanks(int paperid, List<int> bankids, string creater)
        {
            TblPaperBankDal paperbankdal = new TblPaperBankDal();
            return paperbankdal.Insert(paperid, bankids, creater);
        }
    }
}
