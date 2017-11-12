using DBFactoryEntity;
using OAS.Model.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tg.Helper;

namespace OAS.Dal.Common
{
    public class TblAreaInfoDal : BaseDal<tbl_areainfo>
    {
        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="areano">区域编号</param>
        /// <returns></returns>
        public tbl_areainfo Query(string areano)
        {
            tbl_areainfo areamodel = new tbl_areainfo() { areano = areano };
            areamodel = QueryEntity(areamodel);
            return areamodel;
        }

        /// <summary>
        /// 查询数据信息
        /// </summary>
        /// <param name="areano">区域代码</param>
        /// <param name="areaname">区域名称</param>
        /// <returns></returns>
        public OperateResultModel Query(string areano, string areaname)
        {
            List<DBMemberEntity> entities = new List<DBMemberEntity>();
            if (!string.IsNullOrEmpty(areano))
            {
                entities.AddMember("areano", areano);
            }
            if (!string.IsNullOrEmpty(areaname))
            {
                entities.AddMember("areaname", areaname);
            }
            OperateResultModel orm = Query(entities, "areaorder", null);
            if (orm.rows != null)
            {
                orm.rows = ModelHelper.ToModel<List<tbl_areainfo>>((DataTable)orm.rows);
            }
            return orm;
        }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="areamodel">实体</param>
        /// <returns></returns>
        public OperateResultModel Insert(tbl_areainfo areamodel)
        {
            return dbhelper.Insert(areamodel);
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="areamodel">实体</param>
        /// <returns></returns>
        public OperateResultModel Update(tbl_areainfo areamodel)
        {
            return dbhelper.Update(areamodel);
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="areano">编号</param>
        /// <returns></returns>
        public OperateResultModel Delete(string areano)
        {
            tbl_areainfo role = new tbl_areainfo() { areano = areano };
            return dbhelper.Delete(role);
        }
    }
}
