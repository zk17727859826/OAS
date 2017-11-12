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
        /// 查询流水号信息
        /// </summary>
        /// <param name="serialno">序列编号</param>
        /// <param name="serialfix">流水号前缀</param>
        /// <returns></returns>
        public string GetSerialNo(string serialno, string serialfix)
        {
            SysSerialsetDal dal = new SysSerialsetDal();
            return dal.GetSerialNo(serialno, serialfix);
        }

        /// <summary>
        /// 查询信息
        /// </summary>
        /// <param name="serialno">编号代码</param>
        /// <returns></returns>
        public sysserialset QuerySerialSet(string serialno)
        {
            SysSerialsetDal dal = new SysSerialsetDal();
            return dal.Query(serialno);
        }

        /// <summary>
        /// 查询信息
        /// </summary>
        /// <param name="serialno">编号代码</param>
        /// <param name="serialname">编号名称</param>
        /// <returns></returns>
        public OperateResultModel QuerySerialSetes(string serialno, string serialname)
        {
            SysSerialsetDal dal = new SysSerialsetDal();
            return dal.Query(serialno, serialname);
        }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="serialmodel">实体</param>
        /// <returns></returns>
        public OperateResultModel InsertSerialSet(sysserialset serialmodel)
        {
            SysSerialsetDal dal = new SysSerialsetDal();
            return dal.Insert(serialmodel);
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="serialmodel">实体</param>
        /// <returns></returns>
        public OperateResultModel UpdateSerialSet(sysserialset serialmodel)
        {
            SysSerialsetDal dal = new SysSerialsetDal();
            return dal.Update(serialmodel);
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="serialno">编号代码</param>
        /// <returns></returns>
        public OperateResultModel DeleteSerialSet(string serialno)
        {
            SysSerialsetDal dal = new SysSerialsetDal();
            return dal.Delete(serialno);
        }
    }
}
