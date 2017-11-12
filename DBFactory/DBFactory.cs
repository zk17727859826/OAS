using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DBFactoryEntity
{
    public abstract class DBFactory : IDisposable
    {
        public abstract DbConnection Connection();

        public abstract DbTransaction Transaction();

        public abstract BuildResultEntity DbInsertSqlBuilder<T>(T model) where T : new();

        public abstract BuildResultEntity DbUpdateSqlBuilder<T>(T model) where T : new();

        public abstract BuildResultEntity DbDeleteSqlBuilder<T>(T model) where T : new();

        public abstract string DbConditionSqlBuilder(List<DBMemberEntity> entities);

        public abstract OperateResultModel Query(string tablename, List<DBMemberEntity> entities, string orderby, PaginModel pm);

        public abstract DataSet Query(string strSQL, List<DBMemberEntity> entities);

        public abstract DataTable Query(DbConnection conn, DbTransaction trans, string strSQL, List<DbParameter> dbparams);

        public abstract object QueryScalar(string strSQL, List<DBMemberEntity> entites);

        public abstract object QueryScalar(DbConnection conn, DbTransaction trans, string strSQL, List<DBMemberEntity> entites);

        public abstract void Excute(string strSQL, List<DBMemberEntity> entites);

        public abstract void Excute(DbConnection conn, DbTransaction trans, string strSQL, List<DBMemberEntity> entites);

        public abstract OperateResultModel Insert<T>(T model) where T : new();

        public abstract bool Insert<T>(DbConnection conn, DbTransaction trans, T model) where T : new();

        public abstract OperateResultModel Update<T>(T model) where T : new();

        public abstract int Update<T>(DbConnection conn, DbTransaction trans, T model) where T : new();

        public abstract OperateResultModel Delete<T>(T model) where T : new();

        public abstract int Delete<T>(DbConnection conn, DbTransaction trans, T model) where T : new();

        public void Dispose()
        {
            return;
        }
    }
}
