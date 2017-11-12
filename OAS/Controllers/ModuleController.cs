using DBFactoryEntity;
using OAS.Bll;
using OAS.Model.Permission;
using OAS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tg.Helper;

namespace OAS.Controllers
{
    public class ModuleController : BaseController
    {
        public ActionResult Index()
        {
            ModuleNo = "ModuleManage";
            ViewBag.ToolBar = BuildPowerButtons();
            return View();
        }

        [HttpPost]
        public JsonResult Query()
        {
            Permission permission = new Permission();
            OperateResultModel orm = permission.QueryMenus();
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        public PartialViewResult Edit(string menuno, string type)
        {
            if (type == "add")
            {
                if (menuno == null)
                {
                    @ViewBag.Top = "Y";//是否创建顶级菜单
                    menuno = "0";
                }
                else
                {
                    @ViewBag.Top = "N";
                }
            }

            Permission permission = new Permission();
            sysmenu menu = null;
            if (type == "edit")
            {                
                menu = permission.QueryMenu(menuno);
            }
            else if(type=="add")
            {
                if (menuno == "0")
                {
                    @ViewBag.ParentNo = menuno;
                    @ViewBag.MenuNo = menuno;
                }
                else
                {
                    menu = permission.QueryMenu(menuno);
                    @ViewBag.ParentNo = menu.parentno;
                    @ViewBag.MenuNo = menu.menuno;
                    menu = null;
                }
            }
            return PartialView(menu);
        }

        [HttpPost]
        public JsonResult Insert(sysmenu menu)
        {
            string msg = ValidInput(menu);
            OperateResultModel orm = new OperateResultModel();
            if (string.IsNullOrEmpty(msg))
            {
                DateTime dtnow = DateTime.Now;
                UserModel um = SessionUser;
                menu.creater = um.UserNo;
                menu.createdate = dtnow;
                menu.editer = um.UserNo;
                menu.editdate = dtnow;

                Permission permission = new Permission();
                orm = permission.InsertMenu(menu);
                orm.rows = menu;
            }
            else
            {
                orm.success = false;
                orm.message = msg;
            }
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult Update(sysmenu menu)
        {
            string msg = ValidInput(menu);
            OperateResultModel orm = new OperateResultModel();
            if (string.IsNullOrEmpty(msg))
            {
                menu.editdate = DateTime.Now;
                menu.editer = SessionUser.UserNo;
                Permission permission = new Permission();
                orm = permission.UpdateMenu(menu);
                orm.rows = menu;
            }
            else
            {
                orm.success = false;
                orm.message = msg;
            }
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult Delete(string menuno)
        {
            Permission permission = new Permission();
            OperateResultModel orm = permission.DeleteMenu(menuno);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult Power(string menuno)
        {
            Permission permission = new Permission();
            OperateResultModel orm = permission.QueryMenuPower(menuno);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult InsertPower(sysmenu_power menupower)
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(menupower.menuno))
            {
                msg += "菜单编号不能为空<br />";
            }
            if (string.IsNullOrEmpty(menupower.powerno))
            {
                msg += "权限编号不能为空<br />";
            }

            string powername = menupower.powername;
            OperateResultModel orm = new OperateResultModel();
            if (string.IsNullOrEmpty(msg))
            {
                menupower.pkid = NewGuid;
                menupower.creater = SessionUser.UserNo;
                menupower.createdate = DateTime.Now;
                menupower.powername = null;
                Permission permission = new Permission();
                orm = permission.InsertMenuPower(menupower);
                menupower.powername = powername;
                orm.rows = menupower;
            }
            else
            {
                orm.message = msg;
                orm.success = false;
            }
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult DeletePower(string pkid)
        {
            Permission permission = new Permission();
            OperateResultModel orm = permission.DeleteMenuPower(pkid);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult ForObject(string menuno)
        {
            Permission permission = new Permission();
            OperateResultModel orm = permission.QueryMenuObject(menuno);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult InsertObject(sysmenu_object menuobject)
        {
            menuobject.pkid = NewGuid;
            menuobject.creater = SessionUser.UserNo;
            menuobject.createdate = DateTime.Now;
            Permission permission = new Permission();
            OperateResultModel orm = permission.InsertMenuObject(menuobject);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult DeleteObject(string pkid)
        {
            Permission permission = new Permission();
            OperateResultModel orm = permission.DeleteMenuObject(pkid);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        private string ValidInput(sysmenu menu)
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(menu.parentno))
            {
                msg += "请选择同级还是下级<br />";
            }
            if (string.IsNullOrEmpty(menu.menuno))
            {
                msg += "请输入菜单编号<br />";
            }
            if (string.IsNullOrEmpty(menu.menuname))
            {
                msg += "请输入菜单名称<br />";
            }
            if (string.IsNullOrEmpty(menu.isvalid))
            {
                msg += "请选择是否有效<br />";
            }
            return msg;
        }
    }
}
