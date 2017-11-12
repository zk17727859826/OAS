using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tg.Helper
{
    public class ConfigHelper
    {
        /// <summary>
        /// 获取配置文件中的appSettings中的值
        /// </summary>
        /// <param name="key">key键</param>
        /// <returns></returns>
        public static string GetAppSettingsValue(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// 获得配置文件中的connectionString中的值
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns></returns>
        public static string GetConnectionString(string name)
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings[name].ToString();
        }
    }
}
