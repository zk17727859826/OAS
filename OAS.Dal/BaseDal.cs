using DBFactoryEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using tg.Helper;

namespace OAS.Dal
{
    public class BaseDal<T> where T : new()
    {
        private DBFactoryEntity.DBFactory _dbhelper;

        /// <summary>
        /// 数据库连接帮助对象
        /// </summary>
        public DBFactoryEntity.DBFactory dbhelper
        {
            get
            {
                if (_dbhelper == null)
                {
                    _dbhelper = GetDbHelper();
                }
                return _dbhelper;
            }
        }

        private DBFactoryEntity.DBFactory GetDbHelper()
        {
            DBFactoryEntity.DbHelperFactory _factory = new DBFactoryEntity.DbHelperFactory();
            return _factory.GetDbFactory("SQL SERVER");
        }

        public OperateResultModel Query(List<DBMemberEntity> entities, string orderby, PaginModel pm)
        {
            string tablename = typeof(T).Name;
            OperateResultModel orm = dbhelper.Query(tablename, entities, orderby, pm);
            return orm;
        }

        /// <summary>
        /// 通过实体查询实体信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public T QueryEntity(T model)
        {
            string tablename = GetTableName();
            List<DBMemberEntity> entities = GetEntityInfo(model);
            string strSQL = dbhelper.DbConditionSqlBuilder(entities);
            DataSet ds = dbhelper.Query(string.Format("select * from {0} {1}", tablename, strSQL), entities);
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count == 1)
                {
                    model = ToModel(ds.Tables[0].Rows[0]);
                    return model;
                }
            }
            return default(T);
        }

        /// <summary>
        /// 获得实体名称
        /// </summary>
        /// <returns></returns>
        public static string GetTableName()
        {
            return typeof(T).Name;
        }

        /// <summary>
        /// 获得不为空的实体信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<DBMemberEntity> GetEntityInfo(T model)
        {
            List<DBMemberEntity> entitylist = new List<DBMemberEntity>();
            PropertyInfo[] piinfo = model.GetType().GetProperties();
            foreach (PropertyInfo pi in piinfo)
            {
                if (pi.GetValue(model) == null) continue;
                if (typeof(List<>).Name == pi.PropertyType.Name) continue;

                DBMemberEntity entity = new DBMemberEntity();
                entity.MemberName = pi.Name;
                entity.MemberValue = pi.GetValue(model);

                //Int32 置空
                if (pi.GetType() == typeof(System.Int32))
                {
                    entity.MemberType = DbType.Int32;
                    if (Convert.ToInt32(entity.MemberValue) == Int32.MinValue)
                    {
                        entity.MemberValue = DBNull.Value;
                    }
                }

                //Decimal 置空
                if (pi.GetType() == typeof(System.Decimal))
                {
                    entity.MemberType = DbType.Decimal;
                    if (Convert.ToDecimal(entity.MemberValue) == Decimal.MinValue)
                    {
                        entity.MemberValue = DBNull.Value;
                    }
                }

                //Datetime 置空
                if (pi.GetType() == typeof(System.DateTime))
                {
                    entity.MemberType = DbType.DateTime;
                    if (Convert.ToDateTime(entity.MemberValue) == DateTime.MinValue)
                    {
                        entity.MemberValue = DBNull.Value;
                    }
                }

                entitylist.Add(entity);
            }

            return entitylist;
        }

        /// <summary>
        /// DataTable转换成ModelList
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public T ToModel(DataTable table)
        {
            string json = DataTableToJsonWithJavaScriptSerializer(table);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<T>(json);
        }

        /// <summary>
        /// DataRow转换成Model
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public T ToModel(DataRow row)
        {
            string json = DataRowToJsonWithJavaScriptSerializer(row);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<T>(json);
        }

        /// <summary>
        /// DataTable转换成Json
        /// </summary>
        /// <param name="table">表格</param>
        /// <returns></returns>
        public static string DataRowToJsonWithJavaScriptSerializer(DataRow row)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            childRow = new Dictionary<string, object>();
            foreach (DataColumn col in row.Table.Columns)
            {
                childRow.Add(col.ColumnName, row[col]);
            }
            return jsSerializer.Serialize(childRow);
        }

        /// <summary>
        /// DataTable转换成Json
        /// </summary>
        /// <param name="table">表格</param>
        /// <returns></returns>
        public static string DataTableToJsonWithJavaScriptSerializer(DataTable table)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in table.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return jsSerializer.Serialize(parentRow);
        }
    }
}
