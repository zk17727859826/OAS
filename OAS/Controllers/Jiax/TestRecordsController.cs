using DBFactoryEntity;
using OAS.Bll;
using OAS.Model.Study;
using OAS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OAS.Controllers.Jiax
{
    public class TestRecordsController : BaseController
    {
        public ActionResult Index()
        {
            ModuleNo = "StudyRecords";
            ViewBag.ToolBar = BuildPowerButtons();
            return View();
        }

        [HttpPost]
        public JsonResult Index(string xueyh, string xingming, int page = 1, int rows = 20)
        {
            PaginModel pm = new PaginModel()
            {
                page = page,
                pagesize = rows
            };
            Study study = new Study();
            OperateResultModel orm = study.QueryTestRecords(xueyh, xingming, pm);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        //public PartialViewResult Edit(string testno)
        //{
        //    Study study = new Study();
        //    tbl_test_records model = null;
        //    if (!string.IsNullOrEmpty(testno))
        //    {
        //        model = study.QueryTestRecord(testno);
        //    }
        //    return PartialView(model);
        //}

        //[HttpPost]
        //public JsonResult Add(tbl_test_records model)
        //{
        //    model.creater = SessionUser.UserNo;
        //    model.createdate = DateTime.Now;
        //    Study study = new Study();
        //    OperateResultModel orm = study.InsertTestRecord(model);
        //    return JsonResultHelper.ConvertToJsonResult(orm);
        //}

        //[HttpPost]
        //public JsonResult Update(tbl_test_records model)
        //{
        //    Study study = new Study();
        //    OperateResultModel orm = study.UpdateTestRecord(model);
        //    return JsonResultHelper.ConvertToJsonResult(orm);
        //}
        //
        //[HttpPost]
        //public JsonResult Delete(string xueyh)
        //{
        //    Study study = new Study();
        //    OperateResultModel orm = study.DeleteTestRecord(xueyh);
        //    return JsonResultHelper.ConvertToJsonResult(orm);
        //}
    }
}
