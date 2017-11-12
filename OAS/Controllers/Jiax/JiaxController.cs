using DBFactoryEntity;
using OAS.Bll;
using OAS.Model.Jiax;
using OAS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OAS.Controllers.Jiax
{
    public class JiaxController : BaseController
    {
        public ActionResult Index()
        {
            ModuleNo = "JiaxList";
            ViewBag.ToolBar = BuildPowerButtons();
            return View();
        }

        [HttpPost]
        public JsonResult Index(string jiaxname, string areano)
        {
            JiaxInfo jiaxinfo = new JiaxInfo();
            OperateResultModel orm = jiaxinfo.QueryJiaxes(jiaxname, areano);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        public PartialViewResult Edit(string jiaxid)
        {
            JiaxInfo jiaxinfo = new JiaxInfo();
            tbl_jiax model = null;
            if (!string.IsNullOrEmpty(jiaxid))
            {
                model = jiaxinfo.QueryJiax(jiaxid);
            }
            return PartialView(model);
        }

        [HttpPost]
        public JsonResult Add(tbl_jiax model)
        {
            model.creater = SessionUser.UserNo;
            model.createdate = DateTime.Now;
            model.editer = model.creater;
            model.editdate = model.createdate;
            JiaxInfo jiaxinfo = new JiaxInfo();
            OperateResultModel orm = jiaxinfo.InsertJiax(model);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult Update(tbl_jiax model)
        {
            model.editer = SessionUser.UserNo;
            model.editdate = DateTime.Now;
            JiaxInfo jiaxinfo = new JiaxInfo();
            OperateResultModel orm = jiaxinfo.UpdateJiax(model);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult Delete(string jiaxid)
        {
            JiaxInfo jiaxinfo = new JiaxInfo();
            OperateResultModel orm = jiaxinfo.DeleteJiax(jiaxid);
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

            JiaxInfo jiaxinfo = new JiaxInfo();
            OperateResultModel orm = jiaxinfo.QueryJiaxes("", "");
            return PartialView(orm.rows);
        }
    }
}
