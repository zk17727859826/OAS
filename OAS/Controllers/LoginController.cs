using DBFactoryEntity;
using OAS.Bll;
using OAS.Model.Permission;
using OAS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OAS.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Index(string userno, string password)
        {
            userno = userno.ToUpper();
            Permission permission = new Permission();
            OperateResultModel orm = permission.ValidUser(userno, password);
            sysuser user = orm.rows as sysuser;
            if (user == null)
            {

            }
            else
            {
                UserModel um = new UserModel()
                {
                    UserNo = user.userno,
                    UserName = user.username
                };
                BaseController bc = new BaseController();
                bc.SessionUser = um;
            }
            return JsonResultHelper.ConvertToJsonResult(orm);
        }
    }
}
