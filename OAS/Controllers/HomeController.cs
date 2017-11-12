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
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            UserModel um = SessionUser;
            Permission permission = new Permission();
            List<sysmenu> menus = permission.QueryUserMenus(um.UserNo);
            return View(menus);
        }
    }
}
