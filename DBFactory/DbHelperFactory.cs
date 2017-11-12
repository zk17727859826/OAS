using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBFactoryEntity
{
    public class DbHelperFactory
    {
        /// <summary>
        /// 获得数据库Helper
        /// </summary>
        /// <param name="dbflag">数据库类型（ORACLE,SQL SERVER,MYSQL）</param>
        /// <returns></returns>
        public DBFactory GetDbFactory(string dbflag)
        {
            switch (dbflag)
            {
                case "SQL SERVER":
                    return new DBSqlServiceFactory();
                default:
                    return null;
            }
        }
    }
}
