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
    public class SerialController : BaseController
    {
        public ActionResult Index()
        {
            ModuleNo = "Serial";
            ViewBag.ToolBar = BuildPowerButtons();
            return View();
        }

        [HttpPost]
        public JsonResult Index(string serialno, string serialname)
        {
            CommonBll commbll = new CommonBll();
            OperateResultModel orm = commbll.QuerySerialSetes(serialno, serialname);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        public PartialViewResult Edit(string serialno)
        {
            CommonBll commbll = new CommonBll();
            sysserialset model = null;
            if (!string.IsNullOrEmpty(serialno))
            {
                model = commbll.QuerySerialSet(serialno);
            }
            return PartialView(model);
        }

        [HttpPost]
        public JsonResult Add(sysserialset model)
        {
            CommonBll commbll = new CommonBll();
            OperateResultModel orm = commbll.InsertSerialSet(model);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult Update(sysserialset model)
        {
            CommonBll commbll = new CommonBll();
            OperateResultModel orm = commbll.UpdateSerialSet(model);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult Delete(string serialno)
        {
            CommonBll commbll = new CommonBll();
            OperateResultModel orm = commbll.DeleteSerialSet(serialno);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult Test(string serialno, string specialword)
        {
            CommonBll bll = new CommonBll();
            string result = bll.GetSerialNo(serialno, specialword);
            OperateResultModel orm = new OperateResultModel()
            {
                success = true,
                message = result
            };
            return JsonResultHelper.ConvertToJsonResult(orm);
        }
    }
}
