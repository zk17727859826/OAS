using DBFactoryEntity;
using OAS.Bll;
using OAS.Model.Bank;
using OAS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tg.Helper;

namespace OAS.Controllers.BankManage
{
    public class PaperController : BaseController
    {
        public ActionResult Index()
        {
            ModuleNo = "TestPaper";
            ViewBag.ToolBar = BuildPowerButtons();
            return View();
        }

        [HttpPost]
        public JsonResult BankInfo()
        {
            Bank bank = new Bank();
            OperateResultModel orm = bank.QueryBanks(null, null, null, null, true, null, null);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult Query(string papername, int page = 1, int rows = 20)
        {
            Bank bank = new Bank();
            PaginModel pm = new PaginModel()
            {
                page = page,
                pagesize = rows
            };
            OperateResultModel orm = bank.QueryPapers(papername, pm);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        public PartialViewResult Edit(int? paperid)
        {
            tbl_paper paper = null;
            if (paperid != null)
            {
                Bank bank = new Bank();
                paper = bank.QueryPaper(paperid.Value);
            }
            return PartialView(paper);
        }

        [HttpPost]
        public JsonResult Add(tbl_paper paper)
        {
            OperateResultModel orm = new OperateResultModel();
            orm.message = ValidInput(paper);

            if (string.IsNullOrEmpty(orm.message))
            {
                UserModel um = SessionUser;
                DateTime dtnow = DateTime.Now;
                paper.createdate = dtnow;
                paper.creater = um.UserNo;
                paper.editdate = dtnow;
                paper.editer = um.UserNo;

                Bank bank = new Bank();
                paper.paperid = bank.GetNewPaperId();
                orm = bank.InsertPaper(paper);
                orm.rows = paper;
            }
            else
            {
                orm.success = false;
            }
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult Update(tbl_paper paper)
        {
            OperateResultModel orm = new OperateResultModel();
            orm.message = ValidInput(paper);
            if (string.IsNullOrEmpty(orm.message))
            {
                paper.editdate = DateTime.Now;
                paper.editer = SessionUser.UserNo;
                Bank bank = new Bank();
                orm = bank.UpdatePaper(paper);
                orm.rows = paper;
            }
            else
            {
                orm.success = false;
            }
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult Delete(int paperid)
        {
            Bank bank = new Bank();
            OperateResultModel orm = bank.DeletePaper(paperid);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult QueryPaperBank(int paperid)
        {
            Bank bank = new Bank();
            OperateResultModel orm = bank.QueryPaperBanks(paperid);
            orm.rows = ModelHelper.ToModel<List<tbl_paper_bank>>(orm.rows as DataTable);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult InsertPaperBank(int paperid, string bankids)
        {
            string[] bankidarr = bankids.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            List<int> banklist = new List<int>();
            foreach (string bankid in bankidarr)
            {
                banklist.Add(Convert.ToInt32(bankid));
            }
            Bank bank = new Bank();
            OperateResultModel orm = bank.InsertPaperBanks(paperid, banklist, SessionUser.UserNo);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        /// <summary>
        /// 验证输入的试卷信息是否正常
        /// </summary>
        /// <param name="paper">实体</param>
        /// <returns></returns>
        private string ValidInput(tbl_paper paper)
        {
            string msg = string.Empty;

            if (string.IsNullOrEmpty(paper.papername))
            {
                msg += "请输入试卷名称";
            }
            return msg;
        }
    }
}
