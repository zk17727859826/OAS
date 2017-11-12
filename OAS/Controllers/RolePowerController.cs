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
    public class RolePowerController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Role()
        {
            Permission permission = new Permission();
            OperateResultModel orm = permission.QueryRoles();
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public ActionResult Module(string roleno)
        {
            Permission permission = new Permission();
            List<JsonTree> trees = permission.QueryRoleMenusForTree(roleno);
            string json = ModelHelper.ToJson(trees);
            json = json.Replace("Checked", "checked");
            return Content(json);
        }

        [HttpPost]
        public JsonResult InserRoleModule(string info, string roleno, string menuno, string powerno, string rolepowers, string roleobjects)
        {
            UserModel um = SessionUser;
            DateTime dtNow = DateTime.Now;
            List<sysrole_menu> rolemenus = ModelHelper.ToModel<List<sysrole_menu>>(info);
            foreach (sysrole_menu rolemenu in rolemenus)
            {
                rolemenu.pkid = NewGuid;
                rolemenu.creater = um.UserNo;
                rolemenu.createdate = dtNow;
                rolemenu.roleno = roleno;
            }

            List<sysrole_menu_power> rolepowerlist = new List<sysrole_menu_power>();
            string[] powers = rolepowers.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string power in powers)
            {
                rolepowerlist.Add(new sysrole_menu_power()
                {
                    roleno = roleno,
                    menuno = menuno,
                    createdate = dtNow,
                    creater = um.UserNo,
                    pkid = NewGuid,
                    powerno = power
                });
            }

            List<sysrole_menu_power_object> roleobjectlist = new List<sysrole_menu_power_object>();
            string[] objects = roleobjects.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string obj in objects)
            {
                roleobjectlist.Add(new sysrole_menu_power_object()
                {
                    createdate = dtNow,
                    creater = um.UserNo,
                    forobject = obj,
                    menuno = menuno,
                    pkid = NewGuid,
                    powerno = powerno,
                    roleno = roleno
                });
            }

            Permission permission = new Permission();
            OperateResultModel orm = permission.InsertRolePowers(roleno, menuno, powerno, rolemenus, rolepowerlist, roleobjectlist);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult RolePowers(string roleno, string menuno)
        {
            OperateResultModel orm = new OperateResultModel();
            try
            {
                Permission permission = new Permission();
                List<syspower> rolepowers = permission.QueryRolePowers(roleno, menuno);
                orm.rows = rolepowers;
                orm.total = rolepowers.Count;
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
                orm.rows = roleobjects;
                orm.total = roleobjects.Count;
                orm.success = true;
            }
            catch (Exception ex)
            {
                orm.success = false;
                orm.message = ex.Message;
            }
            return JsonResultHelper.ConvertToJsonResult(orm);
        }
    }
}
