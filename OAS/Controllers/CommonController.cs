using DBFactoryEntity;
using OAS.Bll;
using OAS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OAS.Controllers
{
    public class CommonController : Controller
    {
        [HttpPost]
        public JsonResult Power()
        { 
            Permission permission = new Permission();
            OperateResultModel orm = permission.QueryPower();
            return JsonResultHelper.ConvertToJsonResult(orm);
        }
    }
}
