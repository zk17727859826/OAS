using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace tg.Helper
{
    public class ModelHelper
    {
        /// <summary>
        /// 把字串转换成实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="input">Json字串</param>
        /// <returns></returns>
        public static T ToModel<T>(string input) where T : class
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<T>(input);
        }

        /// <summary>
        /// DataTable转换成Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <returns></returns>
        public static T ToModel<T>(DataRow row) where T : new()
        {
            string json = DataRowToJsonWithJavaScriptSerializer(row);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<T>(json);
        }

        /// <summary>
        /// DataTable转换成Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static T ToModel<T>(DataTable table) where T : new()
        {
            string json = DataTableToJsonWithJavaScriptSerializer(table);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<T>(json);
        }

        /// <summary>
        /// 把对象转换成Json字串
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public static string ToJson<T>(T model) where T : class
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(model);
        }

        /// <summary>
        /// 把DataTable转换成Json字串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToJson(DataTable dt)
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
            ArrayList arrayList = new ArrayList();
            foreach (DataRow dataRow in dt.Rows)
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();  //实例化一个参数集合
                foreach (DataColumn dataColumn in dt.Columns)
                {
                    dictionary.Add(dataColumn.ColumnName, dataRow[dataColumn.ColumnName].ToString());
                }
                arrayList.Add(dictionary); //ArrayList集合中添加键值
            }
            return javaScriptSerializer.Serialize(arrayList);  //返回一个json字符串
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
