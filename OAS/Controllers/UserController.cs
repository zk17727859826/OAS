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
    public class UserController : BaseController
    {
        public ActionResult Index()
        {
            ModuleNo = "UserManage";
            ViewBag.ToolBar = BuildPowerButtons();
            return View();
        }

        [HttpPost]
        public JsonResult Index(string username, string userno, int page = 1, int pagesize = 30)
        {
            Permission permission = new Permission();
            PaginModel pm = new PaginModel()
            {
                page = page,
                pagesize = pagesize
            };
            OperateResultModel orm = permission.QueryUsers(userno, username, pm);

            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        public PartialViewResult Edit(string userno)
        {
            sysuser user = null;
            if (!string.IsNullOrEmpty(userno))
            {
                Permission permission = new Permission();
                user = permission.QueryUser(userno);
            }
            return PartialView(user);
        }

        [HttpPost]
        public JsonResult Add(sysuser user)
        {
            OperateResultModel orm = new OperateResultModel();
            orm.message = ValidInput(user);

            if (string.IsNullOrEmpty(orm.message))
            {
                UserModel um = SessionUser;
                user.password = "123456";
                DateTime dtnow = DateTime.Now;
                user.createdate = dtnow;
                user.creater = um.UserNo;
                user.editdate = dtnow;
                user.editer = um.UserNo;

                Permission permission = new Permission();
                orm = permission.InsertUser(user);
                orm.rows = user;
            }
            else
            {
                orm.success = false;
            }
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult Update(sysuser user)
        {
            OperateResultModel orm = new OperateResultModel();
            orm.message = ValidInput(user);
            if (string.IsNullOrEmpty(orm.message))
            {
                user.editdate = DateTime.Now;
                user.editer = SessionUser.UserNo;
                Permission permission = new Permission();
                orm = permission.UpdateUser(user);
                orm.rows = user;
            }
            else
            {
                orm.success = false;
            }
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult Delete(string userno)
        {
            Permission permission = new Permission();
            OperateResultModel orm = permission.DeleteUser(userno);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        public PartialViewResult Password(string userno)
        {
            Permission permission = new Permission();
            sysuser user = permission.QueryUser(userno);
            return PartialView(user);
        }

        [HttpPost]
        public JsonResult Password(string userno, string repeatpassword, string newpassword)
        {
            OperateResultModel orm = new OperateResultModel();
            try
            {
                if (string.IsNullOrEmpty(repeatpassword) || string.IsNullOrEmpty(newpassword))
                {
                    throw new Exception("密码不能为空");
                }

                if (repeatpassword != newpassword)
                {
                    throw new Exception("两次密码输入不一致");
                }

                Permission permission = new Permission();
                sysuser user = new sysuser()
                {
                    userno = userno,
                    password = newpassword,
                    editer = SessionUser.UserNo,
                    editdate = DateTime.Now
                };
                orm = permission.UpdateUser(user);
            }
            catch (Exception ex)
            {
                orm.message = ex.Message;
                orm.success = false;
            }

            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        /// <summary>
        /// 验证输入的用户信息是否正常
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string ValidInput(sysuser user)
        {
            string msg = string.Empty;

            if (string.IsNullOrEmpty(user.userno))
            {
                msg += "请输入用户编号<br />";
            }
            if (string.IsNullOrEmpty(user.username))
            {
                msg += "请输入用户姓名<br />";
            }
            if (string.IsNullOrEmpty(user.xingbie))
            {
                msg += "请选择用户性别<br />";
            }
            if (string.IsNullOrEmpty(user.shenfhm))
            {
                msg += "请输入身份号码<br />";
            }
            if (string.IsNullOrEmpty(user.isvalid))
            {
                msg += "请选择是否有效<br />";
            }
            return msg;
        }
    }
}
