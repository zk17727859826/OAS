using DBFactoryEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DBFactoryEntity
{
    public class DBSqlServiceFactory : DBFactory
    {
        public string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

        private SqlConnection _conn;

        private SqlTransaction _trans;

        /// <summary>
        /// 数据库连接对象
        /// </summary>
        /// <returns></returns>
        public override System.Data.Common.DbConnection Connection()
        {
            if (_conn == null)
            {
                _conn = new SqlConnection(connectionstring);
            }
            return _conn;
        }

        /// <summary>
        /// 数据库事务对象
        /// </summary>
        /// <returns></returns>
        public override System.Data.Common.DbTransaction Transaction()
        {
            if (_trans == null)
            {
                _conn.Open();
                _trans = _conn.BeginTransaction();
            }
            return _trans;
        }

        public DBSqlServiceFactory() { }

        public override BuildResultEntity DbInsertSqlBuilder<T>(T model)
        {
            BuildResultEntity bre = new BuildResultEntity();
            string tablename = typeof(T).Name;
            List<DBMemberEntity> entitylist = GetEntityInfo(model);
            bre.entities = entitylist;

            if (entitylist.Count == 0)
            {

            }
            else
            {
                string strNameSQL = string.Empty;
                string strValueSQL = string.Empty;
                foreach (DBMemberEntity entity in entitylist)
                {
                    strNameSQL += entity.MemberName + ",";
                    strValueSQL += "@" + entity.MemberName + ",";
                }
                bre.sql = string.Format("insert into {0}({1}) values ({2})", tablename, strNameSQL.TrimEnd(','), strValueSQL.TrimEnd(','));
            }
            return bre;
        }

        public override BuildResultEntity DbUpdateSqlBuilder<T>(T model)
        {
            BuildResultEntity bre = new BuildResultEntity();
            string tablename = typeof(T).Name;
            List<DBMemberEntity> entitylist = GetEntityInfo(model);
            bre.entities = entitylist;

            if (entitylist.Count == 0)
            {

            }
            else
            {
                PropertyInfo pi = model.GetType().GetProperty("keyfields");
                List<string> keyfields = pi.GetValue(model) as List<string>;
                List<DBMemberEntity> conditiontities = new List<DBMemberEntity>();
                string valuesql = string.Empty;
                for (int i = 0; i < entitylist.Count; i++)
                {
                    if (keyfields.Contains(entitylist[i].MemberName))
                    {
                        conditiontities.Add(entitylist[i]);
                    }
                    else
                    {
                        valuesql += entitylist[i].MemberName + "=@" + entitylist[i].MemberName + ",";
                    }
                }

                string wheresql = DbConditionSqlBuilder(conditiontities);
                bre.sql = string.Format("update {0} set {1} {2}", tablename, valuesql.TrimEnd(','), wheresql);
            }
            return bre;
        }

        public override BuildResultEntity DbDeleteSqlBuilder<T>(T model)
        {
            BuildResultEntity bre = new BuildResultEntity();
            string tablename = typeof(T).Name;
            List<DBMemberEntity> entitylist = GetEntityInfo(model);
            bre.entities = entitylist;

            if (entitylist.Count == 0)
            {

            }
            else
            {
                string strCondition = DbConditionSqlBuilder(entitylist);

                bre.sql = string.Format("DELETE FROM {0} {1}", tablename, strCondition);
            }
            return bre;
        }

        public override string DbConditionSqlBuilder(List<DBMemberEntity> entities)
        {
            StringBuilder sb = new StringBuilder();
            if (entities == null || entities.Count == 0)
            {
                return null;
            }
            sb.Append(" where ");
            for (int i = 0; i < entities.Count; i++)
            {
                DBMemberEntity entity = entities[i];
                if (!string.IsNullOrEmpty(entity.MemberBelong))
                {
                    sb.Append(entity.MemberBelong);
                    sb.Append(".");
                }
                sb.Append(entity.MemberName);

                switch (entity.QueryType)
                {
                    case QueryTypeEnum.equal:
                        sb.Append("=");
                        break;
                    case QueryTypeEnum.lt:
                        sb.Append("<");
                        break;
                    case QueryTypeEnum.grant:
                        sb.Append(">");
                        break;
                    case QueryTypeEnum.fruzz:
                        sb.Append(" like ");
                        break;
                    case QueryTypeEnum.notequal:
                        sb.Append(" <> ");
                        break;
                    default:
                        break;
                }

                sb.Append("@");
                sb.Append(entity.MemberName);
                if (entity.QueryType == QueryTypeEnum.fruzz)
                {
                    entity.MemberValue = string.Format("%{0}%", entity.MemberValue);
                }

                if (i < entities.Count - 1)
                {
                    sb.Append(" and ");
                }
            }
            return sb.ToString();
        }

        public override OperateResultModel Query(string tablename, List<DBMemberEntity> entities, string orderby, PaginModel pm)
        {
            string strSQL = "select count(0) from {0} {1};{2} select * {3} from {0} {1} {4}";
            string format2 = "";
            string format3 = "";
            string format4 = "";
            if (pm != null)
            {
                format2 = "select aa.* from(";
                format3 = ",row_number() over(order by " + orderby + ") row_no";
                format4 = ") aa where aa.row_no between {0} and {1}";
                format4 = string.Format(format4, pm.startpos, pm.endpos);
            }
            else
            {
                if (!string.IsNullOrEmpty(orderby))
                {
                    format4 = " order by " + orderby;
                }
            }
            string condition = DbConditionSqlBuilder(entities);
            DataSet ds = Query(string.Format(strSQL, tablename, condition, format2, format3, format4), entities);
            OperateResultModel orm = new OperateResultModel();
            orm.total = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            orm.success = true;
            orm.rows = ds.Tables[1];
            return orm;
        }

        public override DataSet Query(string strSQL, List<DBMemberEntity> entities)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                SqlCommand cmd = new SqlCommand(strSQL, conn);
                if (entities != null)
                {
                    foreach (DBMemberEntity m in entities)
                    {
                        SqlParameter param = new SqlParameter(m.MemberName, m.MemberValue);
                        cmd.Parameters.Add(param);
                    }
                }

                DataSet ds = new DataSet();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
                return ds;
            }
        }

        public override DataTable Query(System.Data.Common.DbConnection conn, System.Data.Common.DbTransaction trans, string strSQL, List<System.Data.Common.DbParameter> dbparams)
        {
            throw new NotImplementedException();
        }

        public override object QueryScalar(string strSQL, List<DBMemberEntity> entities)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        object obj = QueryScalar(conn, trans, strSQL, entities);
                        trans.Commit();
                        return obj;
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        public override object QueryScalar(System.Data.Common.DbConnection conn, System.Data.Common.DbTransaction trans, string strSQL, List<DBMemberEntity> entities)
        {
            SqlCommand cmd = new SqlCommand(strSQL, (SqlConnection)conn);
            cmd.Transaction = (SqlTransaction)trans;
            if (entities != null)
            {
                foreach (DBMemberEntity m in entities)
                {
                    SqlParameter param = new SqlParameter(m.MemberName, m.MemberValue);
                    cmd.Parameters.Add(param);
                }
            }
            return cmd.ExecuteScalar();
        }

        public override void Excute(string strSQL, List<DBMemberEntity> entities)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        Excute(conn, trans, strSQL, entities);
                        trans.Commit();
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        public override void Excute(System.Data.Common.DbConnection conn, System.Data.Common.DbTransaction trans, string strSQL, List<DBMemberEntity> entities)
        {
            SqlCommand cmd = new SqlCommand(strSQL, (SqlConnection)conn);
            cmd.Transaction = (SqlTransaction)trans;
            if (entities != null)
            {
                foreach (DBMemberEntity m in entities)
                {
                    SqlParameter param = new SqlParameter(m.MemberName, m.MemberValue);
                    cmd.Parameters.Add(param);
                }
            }
            cmd.ExecuteNonQuery();
        }

        public override OperateResultModel Insert<T>(T model)
        {
            OperateResultModel orm = new OperateResultModel();
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        Insert(conn, trans, model);
                        trans.Commit();
                        orm.success = true;
                        orm.rows = model;
                        orm.message = "插入成功";
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        orm.success = false;
                        if (ex.Message.IndexOf("重复") > -1)
                        {
                            orm.message = "不可重复添加";
                        }
                        else
                        {
                            orm.message = ex.Message;
                        }
                    }
                }
            }
            return orm;
        }

        public override bool Insert<T>(System.Data.Common.DbConnection conn, System.Data.Common.DbTransaction trans, T model)
        {
            BuildResultEntity bre = DbInsertSqlBuilder(model);

            SqlCommand cmd = new SqlCommand(bre.sql, (SqlConnection)conn);
            cmd.Transaction = (SqlTransaction)trans;
            if (bre.entities != null)
            {
                foreach (DBMemberEntity m in bre.entities)
                {
                    SqlParameter param = new SqlParameter(m.MemberName, m.MemberValue);
                    cmd.Parameters.Add(param);
                }
            }

            cmd.ExecuteNonQuery();

            return true;
        }

        public override OperateResultModel Update<T>(T model)
        {
            OperateResultModel orm = new OperateResultModel();
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        if (Update(conn, trans, model) == 1)
                        {
                            trans.Commit();
                            orm.success = true;
                            orm.rows = model;
                            orm.message = "更新成功";
                        }
                        else
                        {
                            throw new Exception("更新失败");
                        }
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        orm.success = false;
                        orm.message = ex.Message;
                    }
                }
            }
            return orm;
        }

        public override int Update<T>(System.Data.Common.DbConnection conn, System.Data.Common.DbTransaction trans, T model)
        {
            BuildResultEntity bre = DbUpdateSqlBuilder(model);

            SqlCommand cmd = new SqlCommand(bre.sql, (SqlConnection)conn);
            cmd.Transaction = (SqlTransaction)trans;
            if (bre.entities != null)
            {
                foreach (DBMemberEntity m in bre.entities)
                {
                    SqlParameter param = new SqlParameter(m.MemberName, m.MemberValue);
                    cmd.Parameters.Add(param);
                }
            }

            return cmd.ExecuteNonQuery();
        }

        public override OperateResultModel Delete<T>(T model)
        {
            OperateResultModel orm = new OperateResultModel();
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        if (Delete(conn, trans, model) == 1)
                        {
                            trans.Commit();
                            orm.success = true;
                            orm.message = "删除成功";
                        }
                        else
                        {
                            throw new Exception("删除失败");
                        }
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        orm.success = true;
                        orm.message = ex.Message;
                    }
                }
            }
            return orm;
        }

        public override int Delete<T>(System.Data.Common.DbConnection conn, System.Data.Common.DbTransaction trans, T model)
        {
            BuildResultEntity bre = DbDeleteSqlBuilder(model);

            SqlCommand cmd = new SqlCommand(bre.sql, (SqlConnection)conn);
            cmd.Transaction = (SqlTransaction)trans;
            if (bre.entities != null)
            {
                foreach (DBMemberEntity m in bre.entities)
                {
                    SqlParameter param = new SqlParameter(m.MemberName, m.MemberValue);
                    cmd.Parameters.Add(param);
                }
            }

            return cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 获得不为空的实体信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<DBMemberEntity> GetEntityInfo<T>(T model) where T : new()
        {
            List<DBMemberEntity> entitylist = new List<DBMemberEntity>();
            PropertyInfo[] piinfo = typeof(T).GetProperties();
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
    }
}
