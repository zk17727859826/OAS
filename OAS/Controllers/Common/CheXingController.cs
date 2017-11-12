using DBFactoryEntity;
using OAS.Bll;
using OAS.Model.Common;
using OAS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OAS.Controllers.Common
{
    public class CheXingController : BaseController
    {
        public ActionResult Index()
        {
            ModuleNo = "CheXing";
            ViewBag.ToolBar = BuildPowerButtons();
            return View();
        }

        public JsonResult Query()
        {
            CommonBll common = new CommonBll();
            OperateResultModel orm = common.QueryCheXings();
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        public PartialViewResult Edit(string chexing)
        {
            tbl_chexing model = null;
            if (!string.IsNullOrEmpty(chexing))
            {
                CommonBll common = new CommonBll();
                model = common.QueryCheXing(chexing);
            }
            return PartialView(model);
        }

        public PartialViewResult Select(string id, string name, string classname, string value)
        {
            if (!string.IsNullOrEmpty(id))
            {
                ViewBag.ID = id;
            }
            if (!string.IsNullOrEmpty(name))
            {
                ViewBag.Name = name;
            }
            if (!string.IsNullOrEmpty(classname))
            {
                ViewBag.ClassName = classname;
            }
            ViewBag.Value = value;

            CommonBll commbll = new CommonBll();
            OperateResultModel orm = commbll.QueryCheXings();
            return PartialView(orm.rows);
        }

        [HttpPost]
        public JsonResult Add(tbl_chexing model)
        {
            OperateResultModel orm = new OperateResultModel();
            orm.message = ValidInput(model);

            if (string.IsNullOrEmpty(orm.message))
            {
                UserModel um = SessionUser;
                DateTime dtnow = DateTime.Now;
                model.createdate = dtnow;
                model.creater = um.UserNo;
                model.editdate = dtnow;
                model.editer = um.UserNo;

                CommonBll common = new CommonBll();
                orm = common.InsertCheXing(model);
                orm.rows = model;
            }
            else
            {
                orm.success = false;
            }
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult Update(tbl_chexing model)
        {
            OperateResultModel orm = new OperateResultModel();
            orm.message = ValidInput(model);
            if (string.IsNullOrEmpty(orm.message))
            {
                model.editdate = DateTime.Now;
                model.editer = SessionUser.UserNo;
                CommonBll common = new CommonBll();
                orm = common.UpdateCheXing(model);
                orm.rows = model;
            }
            else
            {
                orm.success = false;
            }
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult Delete(string chexing)
        {
            CommonBll common = new CommonBll();
            OperateResultModel orm = common.DeleteCheXing(chexing);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        /// <summary>
        /// 验证输入的是否正常
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        private string ValidInput(tbl_chexing model)
        {
            string msg = string.Empty;

            if (string.IsNullOrEmpty(model.chexing))
            {
                msg += "请输入编号车型<br />";
            }
            return msg;
        }
    }
}
