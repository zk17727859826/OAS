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
        /// <param name="sid">章节ID</param>
        /// <param name="sectionname">章节名称</param>
        /// <returns></returns>
        public tbl_section QuerySection(string sid)
        {
            TblSectionDal dal = new TblSectionDal();
            return dal.Query(sid);
        }

        /// <summary>
        /// 查询数据信息
        /// </summary>
        /// <returns></returns>
        public OperateResultModel QuerySections()
        {
            TblSectionDal dal = new TblSectionDal();
            OperateResultModel orm = dal.Query();
            return orm;
        }

        /// <summary>
        /// 插入用户信息
        /// </summary>
        /// <param name="section">章节实体</param>
        /// <returns></returns>
        public OperateResultModel InsertSection(tbl_section section)
        {
            TblSectionDal dal = new TblSectionDal();
            return dal.Insert(section);
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="section">章节实体</param>
        /// <returns></returns>
        public OperateResultModel UpdateSection(tbl_section section)
        {
            TblSectionDal dal = new TblSectionDal();
            return dal.Update(section);
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="sid">章节编号</param>
        /// <returns></returns>
        public OperateResultModel DeleteSection(string sid)
        {
            TblSectionDal dal = new TblSectionDal();
            return dal.Delete(sid);
        }
    }
}
