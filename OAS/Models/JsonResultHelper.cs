using DBFactoryEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OAS.Models
{
    public static class JsonResultHelper
    {
        public static JsonResult ConvertToJsonResult(this OperateResultModel orm)
        {
            JsonResult jr = new JsonResult()
            {
                Data = new
                {
                    success = orm.success,
                    rows = orm.rows,
                    total = orm.total,
                    message = orm.message
                }
            };

            return jr;
        }
    }
}