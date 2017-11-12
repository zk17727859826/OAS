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
        /// <param name="jiaxid">驾校ID</param>
        /// <returns></returns>
        public tbl_jiax QueryJiax(string jiaxid)
        {
            TblJiaxDal dal = new TblJiaxDal();
            return dal.Query(jiaxid);
        }

        /// <summary>
        /// 查询数据信息
        /// </summary>
        /// <param name="jiaxname">驾校ID</param>
        /// <param name="areano">区域代码</param>
        /// <returns></returns>
        public OperateResultModel QueryJiaxes(string jiaxname, string areano)
        {
            TblJiaxDal dal = new TblJiaxDal();
            return dal.Query(jiaxname, areano);
        }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="jiaxmodel">实体</param>
        /// <returns></returns>
        public OperateResultModel InsertJiax(tbl_jiax jiaxmodel)
        {
            TblJiaxDal dal = new TblJiaxDal();
            return dal.Insert(jiaxmodel);
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="jiaxmodel">实体</param>
        /// <returns></returns>
        public OperateResultModel UpdateJiax(tbl_jiax jiaxmodel)
        {
            TblJiaxDal dal = new TblJiaxDal();
            return dal.Update(jiaxmodel);
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="jiaxid">编号</param>
        /// <returns></returns>
        public OperateResultModel DeleteJiax(string jiaxid)
        {
            TblJiaxDal dal = new TblJiaxDal();
            return dal.Delete(jiaxid);
        }
    }
}
