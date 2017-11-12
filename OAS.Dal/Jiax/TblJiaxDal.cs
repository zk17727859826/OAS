using DBFactoryEntity;
using OAS.Model.Jiax;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tg.Helper;

namespace OAS.Dal.Jiax
{
    public class TblJiaxDal : BaseDal<tbl_jiax>
    {
        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="jiaxid">驾校ID</param>
        /// <returns></returns>
        public tbl_jiax Query(string jiaxid)
        {
            tbl_jiax jiaxmodel = new tbl_jiax() { jiaxid = jiaxid };
            jiaxmodel = QueryEntity(jiaxmodel);
            return jiaxmodel;
        }

        /// <summary>
        /// 查询数据信息
        /// </summary>
        /// <param name="jiaxname">驾校ID</param>
        /// <param name="areano">区域代码</param>
        /// <returns></returns>
        public OperateResultModel Query(string jiaxname, string areano)
        {
            List<DBMemberEntity> entities = new List<DBMemberEntity>();
            if (!string.IsNullOrEmpty(jiaxname))
            {
                entities.AddMember("jiaxname", jiaxname, QueryTypeEnum.fruzz);
            }
            if (!string.IsNullOrEmpty(areano))
            {
                entities.AddMember("areano", areano);
            }
            OperateResultModel orm = Query(entities, "jiaxname", null);
            if (orm.rows != null)
            {
                orm.rows = ModelHelper.ToModel<List<tbl_jiax>>((DataTable)orm.rows);
            }
            return orm;
        }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="jiaxmodel">实体</param>
        /// <returns></returns>
        public OperateResultModel Insert(tbl_jiax jiaxmodel)
        {
            return dbhelper.Insert(jiaxmodel);
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="jiaxmodel">实体</param>
        /// <returns></returns>
        public OperateResultModel Update(tbl_jiax jiaxmodel)
        {
            return dbhelper.Update(jiaxmodel);
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="jiaxid">编号</param>
        /// <returns></returns>
        public OperateResultModel Delete(string jiaxid)
        {
            tbl_jiax model = new tbl_jiax() { jiaxid = jiaxid };
            return dbhelper.Delete(model);
        }
    }
}
