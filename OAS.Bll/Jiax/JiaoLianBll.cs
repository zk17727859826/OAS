using DBFactoryEntity;
using OAS.Dal.Jiax;
using OAS.Model.Jiax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Bll
{
    public partial class JiaxInfo
    {
        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="jiaolh">教练号</param>
        /// <returns></returns>
        public tbl_jiaolian QueryJiaolian(string jiaolh)
        {
            TblJiaolianDal dal = new TblJiaolianDal();
            return dal.Query(jiaolh);
        }

        /// <summary>
        /// 查询数据信息
        /// </summary>
        /// <param name="jiaolh">教练号</param>
        /// <param name="xingming">姓名</param>
        /// <param name="shenfhm">身份号码</param>
        /// <param name="pm">分页信息</param>
        /// <returns></returns>
        public OperateResultModel QueryJiaolians(string jiaolh, string xingming, string shenfhm, PaginModel pm)
        {
            TblJiaolianDal dal = new TblJiaolianDal();
            return dal.Query(jiaolh, xingming, shenfhm, pm);
        }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public OperateResultModel InsertJiaolian(tbl_jiaolian model)
        {
            TblJiaolianDal dal = new TblJiaolianDal();
            return dal.Insert(model);
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public OperateResultModel UpdateJiaolian(tbl_jiaolian model)
        {
            TblJiaolianDal dal = new TblJiaolianDal();
            return dal.Update(model);
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="jiaolh">教练号</param>
        /// <returns></returns>
        public OperateResultModel DeleteJiaolian(string jiaolh)
        {
            TblJiaolianDal dal = new TblJiaolianDal();
            return dal.Delete(jiaolh);
        }
    }
}
