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
        /// 查询实体
        /// </summary>
        /// <param name="pkid">键值</param>
        /// <returns></returns>
        public tbl_paper_rule QueryPaperRule(string pkid)
        {
            TblPaperRuleDal dal = new TblPaperRuleDal();
            return dal.Query(pkid);
        }

        /// <summary>
        /// 查询数据信息
        /// </summary>
        /// <returns></returns>
        public OperateResultModel QueryPaperRules()
        {
            TblPaperRuleDal dal = new TblPaperRuleDal();
            return dal.Query();
        }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public OperateResultModel InsertPaperRule(tbl_paper_rule model)
        {
            TblPaperRuleDal dal = new TblPaperRuleDal();
            return dal.Insert(model);
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public OperateResultModel UpdatePaperRule(tbl_paper_rule model)
        {
            TblPaperRuleDal dal = new TblPaperRuleDal();
            return dal.Update(model);
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="pkid">键值</param>
        /// <returns></returns>
        public OperateResultModel DeletePaperRule(string pkid)
        {
            TblPaperRuleDal dal = new TblPaperRuleDal();
            return dal.Delete(pkid);
        }
    }
}
