using DBFactoryEntity;
using OAS.Model.Bank;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tg.Helper;

namespace OAS.Dal.Bank
{
    public class TblSectionDal : BaseDal<tbl_section>
    {
        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="sid">章节ID</param>
        /// <param name="sectionname">章节名称</param>
        /// <returns></returns>
        public tbl_section Query(string sid)
        {
            tbl_section section = new tbl_section()
            {
                sid = sid
            };
            section = QueryEntity(section);
            return section;
        }

        /// <summary>
        /// 查询数据信息
        /// </summary>
        /// <returns></returns>
        public OperateResultModel Query()
        {
            OperateResultModel orm = Query(null, null, null);
            if (orm.rows != null)
            {
                orm.rows = ModelHelper.ToModel<List<tbl_section>>((DataTable)orm.rows);
            }
            return orm;
        }

        /// <summary>
        /// 插入用户信息
        /// </summary>
        /// <param name="section">章节实体</param>
        /// <returns></returns>
        public OperateResultModel Insert(tbl_section section)
        {
            return dbhelper.Insert(section);
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="section">章节实体</param>
        /// <returns></returns>
        public OperateResultModel Update(tbl_section section)
        {
            return dbhelper.Update(section);
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="sid">章节编号</param>
        /// <returns></returns>
        public OperateResultModel Delete(string sid)
        {
            tbl_section section = new tbl_section() { sid = sid };
            return dbhelper.Delete(section);
        }
    }
}
