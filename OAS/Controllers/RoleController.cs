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
    public class RoleController : BaseController
    {
        public ActionResult Index()
        {
            ModuleNo = "RoleManage";
            ViewBag.ToolBar = BuildPowerButtons();
            return View();
        }

        [HttpPost]
        public JsonResult Query()
        {
            Permission permission = new Permission();
            OperateResultModel orm = permission.QueryRoles();
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        public PartialViewResult Edit(string roleno)
        {
            sysrole role = null;
            if (!string.IsNullOrEmpty(roleno))
            {
                Permission permission = new Permission();
                role = permission.QueryRole(roleno);
            }
            return PartialView(role);
        }

        [HttpPost]
        public JsonResult Add(sysrole role)
        {
            OperateResultModel orm = new OperateResultModel();
            orm.message = ValidInput(role);

            if (string.IsNullOrEmpty(orm.message))
            {
                UserModel um = SessionUser;
                DateTime dtnow = DateTime.Now;
                role.createdate = dtnow;
                role.creater = um.UserNo;
                role.editdate = dtnow;
                role.editer = um.UserNo;

                Permission permission = new Permission();
                orm = permission.InsertRole(role);
                orm.rows = role;
            }
            else
            {
                orm.success = false;
            }
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult Update(sysrole role)
        {
            OperateResultModel orm = new OperateResultModel();
            orm.message = ValidInput(role);
            if (string.IsNullOrEmpty(orm.message))
            {
                role.editdate = DateTime.Now;
                role.editer = SessionUser.UserNo;
                Permission permission = new Permission();
                orm = permission.UpdateRole(role);
                orm.rows = role;
            }
            else
            {
                orm.success = false;
            }
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult Delete(string roleno)
        {
            Permission permission = new Permission();
            OperateResultModel orm = permission.DeleteRole(roleno);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        /// <summary>
        /// 验证输入的用户信息是否正常
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        private string ValidInput(sysrole role)
        {
            string msg = string.Empty;

            if (string.IsNullOrEmpty(role.roleno))
            {
                msg += "请输入角色编号<br />";
            }
            if (string.IsNullOrEmpty(role.rolename))
            {
                msg += "请输入角色名称<br />";
            }
            if (string.IsNullOrEmpty(role.memo))
            {
                msg += "请选择角色描述<br />";
            }
            return msg;
        }
    }
}
