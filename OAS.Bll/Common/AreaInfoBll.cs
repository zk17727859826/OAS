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
        /// <param name="areano">代码</param>
        /// <returns></returns>
        public tbl_areainfo QueryAreaInfo(string areano)
        {
            TblAreaInfoDal dal = new TblAreaInfoDal();
            return dal.Query(areano);
        }

        /// <summary>
        /// 查询信息
        /// </summary>
        /// <param name="areano">区域代码</param>
        /// <param name="areaname">区域名称</param>
        /// <returns></returns>
        public OperateResultModel QueryAreaInfos(string areano, string areaname)
        {
            TblAreaInfoDal dal = new TblAreaInfoDal();
            return dal.Query(areano, areaname);
        }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="areamodel">实体</param>
        /// <returns></returns>
        public OperateResultModel InsertAreaInfo(tbl_areainfo areamodel)
        {
            TblAreaInfoDal dal = new TblAreaInfoDal();
            return dal.Insert(areamodel);
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="areamodel">实体</param>
        /// <returns></returns>
        public OperateResultModel UpdateAreaInfo(tbl_areainfo areamodel)
        {
            TblAreaInfoDal dal = new TblAreaInfoDal();
            return dal.Update(areamodel);
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="areano">代码</param>
        /// <returns></returns>
        public OperateResultModel DeleteAreaInfo(string areano)
        {
            TblAreaInfoDal dal = new TblAreaInfoDal();
            return dal.Delete(areano);
        }
    }
}
