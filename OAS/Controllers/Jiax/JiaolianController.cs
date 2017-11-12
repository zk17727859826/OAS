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
    public class JiaolianController : BaseController
    {
        public ActionResult Index()
        {
            ModuleNo = "JiaxJiaolian";
            ViewBag.ToolBar = BuildPowerButtons();
            return View();
        }

        [HttpPost]
        public JsonResult Index(string jiaolh, string xingming, string shenfhm, int page = 1, int rows = 20)
        {
            PaginModel pm = new PaginModel()
            {
                page = page,
                pagesize = rows
            };
            JiaxInfo jiaxinfo = new JiaxInfo();
            OperateResultModel orm = jiaxinfo.QueryJiaolians(jiaolh, xingming, shenfhm, pm);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        public PartialViewResult Edit(string jiaolh)
        {
            JiaxInfo jiaxinfo = new JiaxInfo();
            tbl_jiaolian model = null;
            if (!string.IsNullOrEmpty(jiaolh))
            {
                model = jiaxinfo.QueryJiaolian(jiaolh);
            }
            return PartialView(model);
        }

        [HttpPost]
        public JsonResult Add(tbl_jiaolian model)
        {
            model.creater = SessionUser.UserNo;
            model.createdate = DateTime.Now;
            model.editer = model.creater;
            model.editdate = model.createdate;
            JiaxInfo jiaxinfo = new JiaxInfo();
            OperateResultModel orm = jiaxinfo.InsertJiaolian(model);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult Update(tbl_jiaolian model)
        {
            model.editer = SessionUser.UserNo;
            model.editdate = DateTime.Now;
            JiaxInfo jiaxinfo = new JiaxInfo();
            OperateResultModel orm = jiaxinfo.UpdateJiaolian(model);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult Delete(string jiaolh)
        {
            JiaxInfo jiaxinfo = new JiaxInfo();
            OperateResultModel orm = jiaxinfo.DeleteJiaolian(jiaolh);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }
    }
}
