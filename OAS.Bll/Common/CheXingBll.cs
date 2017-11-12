using DBFactoryEntity;
using OAS.Dal.Common;
using OAS.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Bll
{
    public partial class CommonBll
    {
        /// <summary>
        /// 查询信息
        /// </summary>
        /// <param name="chexing">代码</param>
        /// <returns></returns>
        public tbl_chexing QueryCheXing(string chexing)
        {
            TblCheXingDal dal = new TblCheXingDal();
            return dal.Query(chexing);
        }

        /// <summary>
        /// 查询信息
        /// </summary>
        /// <returns></returns>
        public OperateResultModel QueryCheXings()
        {
            TblCheXingDal dal = new TblCheXingDal();
            return dal.Query();
        }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public OperateResultModel InsertCheXing(tbl_chexing model)
        {
            TblCheXingDal dal = new TblCheXingDal();
            return dal.Insert(model);
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public OperateResultModel UpdateCheXing(tbl_chexing model)
        {
            TblCheXingDal dal = new TblCheXingDal();
            return dal.Update(model);
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="chexing">代码</param>
        /// <returns></returns>
        public OperateResultModel DeleteCheXing(string chexing)
        {
            TblCheXingDal dal = new TblCheXingDal();
            return dal.Delete(chexing);
        }
    }
}
