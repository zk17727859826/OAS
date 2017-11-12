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
    public class AreaController : BaseController
    {
        public ActionResult Index()
        {
            ModuleNo = "AreaList";
            ViewBag.ToolBar = BuildPowerButtons();
            return View();
        }

        [HttpPost]
        public JsonResult Index(string areano, string areaname)
        {
            CommonBll commbll = new CommonBll();
            OperateResultModel orm = commbll.QueryAreaInfos(areano, areaname);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        public PartialViewResult Edit(string areano)
        {
            CommonBll commbll = new CommonBll();
            tbl_areainfo model = null;
            if (!string.IsNullOrEmpty(areano))
            {
                model = commbll.QueryAreaInfo(areano);
            }
            return PartialView(model);
        }

        [HttpPost]
        public JsonResult Add(tbl_areainfo model)
        {
            model.creater = SessionUser.UserNo;
            model.createdate = DateTime.Now;
            model.editer = model.creater;
            model.editdate = model.createdate;
            CommonBll commbll = new CommonBll();
            OperateResultModel orm = commbll.InsertAreaInfo(model);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult Update(tbl_areainfo model)
        {
            model.editer = SessionUser.UserNo;
            model.editdate = DateTime.Now;
            CommonBll commbll = new CommonBll();
            OperateResultModel orm = commbll.UpdateAreaInfo(model);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult Delete(string areano)
        {
            CommonBll commbll = new CommonBll();
            OperateResultModel orm = commbll.DeleteAreaInfo(areano);
            return JsonResultHelper.ConvertToJsonResult(orm);
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
            OperateResultModel orm = commbll.QueryAreaInfos("", "");
            return PartialView(orm.rows);
        }
    }
}
