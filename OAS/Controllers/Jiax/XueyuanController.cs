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
    public class XueyuanController : BaseController
    {
        public ActionResult Index()
        {
            ModuleNo = "JiaxXueyuan";
            ViewBag.ToolBar = BuildPowerButtons();
            return View();
        }

        [HttpPost]
        public JsonResult Index(string xueyh, string xingming, string shenfhm, int page = 1, int rows = 20)
        {
            PaginModel pm = new PaginModel()
            {
                page = page,
                pagesize = rows
            };
            JiaxInfo jiaxinfo = new JiaxInfo();
            OperateResultModel orm = jiaxinfo.QueryXueyuans(xueyh, xingming, shenfhm, pm);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        public PartialViewResult Edit(string xueyh)
        {
            JiaxInfo jiaxinfo = new JiaxInfo();
            tbl_xueyuan model = null;
            if (!string.IsNullOrEmpty(xueyh))
            {
                model = jiaxinfo.QueryXueyuan(xueyh);
            }
            return PartialView(model);
        }

        [HttpPost]
        public JsonResult Add(tbl_xueyuan model)
        {
            model.creater = SessionUser.UserNo;
            model.createdate = DateTime.Now;
            model.editer = model.creater;
            model.editdate = model.createdate;
            JiaxInfo jiaxinfo = new JiaxInfo();
            OperateResultModel orm = jiaxinfo.InsertXueyuan(model);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult Update(tbl_xueyuan model)
        {
            model.editer = SessionUser.UserNo;
            model.editdate = DateTime.Now;
            JiaxInfo jiaxinfo = new JiaxInfo();
            OperateResultModel orm = jiaxinfo.UpdateXueyuan(model);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult Delete(string xueyh)
        {
            JiaxInfo jiaxinfo = new JiaxInfo();
            OperateResultModel orm = jiaxinfo.DeleteXueyuan(xueyh);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }
    }
}
