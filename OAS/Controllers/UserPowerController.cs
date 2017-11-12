using DBFactoryEntity;
using OAS.Bll;
using OAS.Model.Common;
using OAS.Model.Permission;
using OAS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tg.Helper;

namespace OAS.Controllers
{
    public class UserPowerController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Module(string roleno)
        {
            Permission permission = new Permission();
            List<JsonTree> trees = permission.QueryRoleMenusForTree(roleno, false);
            string json = ModelHelper.ToJson(trees);
            return Content(json);
        }

        [HttpPost]
        public JsonResult RolePowers(string roleno, string menuno)
        {
            OperateResultModel orm = new OperateResultModel();
            try
            {
                Permission permission = new Permission();
                List<syspower> rolepowers = permission.QueryRolePowers(roleno, menuno);
                var powers = rolepowers.Where(p => p.haspower != 0);
                orm.rows = powers;
                orm.total = powers.Count();
                orm.success = true;
            }
            catch (Exception ex)
            {
                orm.success = false;
                orm.message = ex.Message;
            }
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult RoleObjects(string roleno, string menuno, string powerno)
        {
            OperateResultModel orm = new OperateResultModel();
            try
            {
                Permission permission = new Permission();
                List<sysmenu_object> roleobjects = permission.QueryRoleObjects(roleno, menuno, powerno);
                var objects = roleobjects.Where(p => p.haspower != 0);
                orm.rows = objects;
                orm.total = objects.Count();
                orm.success = true;
            }
            catch (Exception ex)
            {
                orm.success = false;
                orm.message = ex.Message;
            }
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult InserUserRoles(string userno, string rolenos)
        {
            UserModel um = SessionUser;
            DateTime dtNow = DateTime.Now;
            OperateResultModel orm = new OperateResultModel();
            Permission permission = new Permission();
            string[] rolenoarr = rolenos.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            List<sysuser_role> userroles = new List<sysuser_role>();
            foreach (string roleno in rolenoarr)
            {
                userroles.Add(new sysuser_role()
                {
                    pkid = NewGuid,
                    roleno = roleno,
                    userno = userno,
                    createdate = dtNow,
                    creater = um.UserNo
                });
            }
            orm = permission.InsertUserRoles(userno, userroles);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult QueryUserRoles(string userno)
        {
            Permission permission = new Permission();
            OperateResultModel orm = permission.QueryUserRoels(userno);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }
    }
}
