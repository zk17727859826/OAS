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
        /// <param name="errorno">错误编号</param>
        /// <returns></returns>
        public tbl_error_bank QueryErrorBank(string errorno)
        {
            TblErrorBankDal dal = new TblErrorBankDal();
            return dal.Query(errorno);
        }

        /// <summary>
        /// 查询数据信息
        /// </summary>
        /// <param name="errortype">错题类型</param>
        /// <param name="xueyh">学员号</param>
        /// <returns></returns>
        public OperateResultModel QueryErrorBanks(string errortype, string xueyh)
        {
            TblErrorBankDal dal = new TblErrorBankDal();
            return dal.Query(errortype, xueyh);
        }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public OperateResultModel Insert(tbl_error_bank model)
        {
            TblErrorBankDal dal = new TblErrorBankDal();
            return dal.Insert(model);
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public OperateResultModel Update(tbl_error_bank model)
        {
            TblErrorBankDal dal = new TblErrorBankDal();
            return dal.Update(model);
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="errorno">错误编号</param>
        /// <returns></returns>
        public OperateResultModel Delete(string errorno)
        {
            TblErrorBankDal dal = new TblErrorBankDal();
            return dal.Delete(errorno);
        }
    }
}
