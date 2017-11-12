using DBFactoryEntity;
using OAS.Bll;
using OAS.Model.Bank;
using OAS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OAS.Controllers.BankManage
{
    public class PaperRuleController : BaseController
    {
        public ActionResult Index()
        {
            ModuleNo = "PaperRule";
            ViewBag.ToolBar = BuildPowerButtons();
            return View();
        }

        public JsonResult Query()
        {
            Bank bank = new Bank();
            OperateResultModel orm = bank.QueryPaperRules();
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        public PartialViewResult Edit(string pkid)
        {
            tbl_paper_rule paperrule = null;
            if (!string.IsNullOrEmpty(pkid))
            {
                Bank bank = new Bank();
                paperrule = bank.QueryPaperRule(pkid);
            }
            return PartialView(paperrule);
        }

        [HttpPost]
        public JsonResult Add(tbl_paper_rule paperrule)
        {
            OperateResultModel orm = new OperateResultModel();
            orm.message = ValidInput(paperrule);

            if (string.IsNullOrEmpty(orm.message))
            {
                UserModel um = SessionUser;
                DateTime dtnow = DateTime.Now;
                paperrule.createdate = dtnow;
                paperrule.creater = um.UserNo;
                paperrule.editdate = dtnow;
                paperrule.editer = um.UserNo;
                paperrule.pkid = NewGuid;

                Bank bank = new Bank();
                orm = bank.InsertPaperRule(paperrule);
                orm.rows = paperrule;
            }
            else
            {
                orm.success = false;
            }
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult Update(tbl_paper_rule paperrule)
        {
            OperateResultModel orm = new OperateResultModel();
            orm.message = ValidInput(paperrule);
            if (string.IsNullOrEmpty(orm.message))
            {
                paperrule.editdate = DateTime.Now;
                paperrule.editer = SessionUser.UserNo;
                Bank bank = new Bank();
                orm = bank.UpdatePaperRule(paperrule);
                orm.rows = paperrule;
            }
            else
            {
                orm.success = false;
            }
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult Delete(string pkid)
        {
            Bank bank = new Bank();
            OperateResultModel orm = bank.DeletePaperRule(pkid);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        /// <summary>
        /// 验证输入的章节信息是否正常
        /// </summary>
        /// <param name="paperrule">章节实体</param>
        /// <returns></returns>
        private string ValidInput(tbl_paper_rule paperrule)
        {
            string msg = string.Empty;

            if (string.IsNullOrEmpty(paperrule.chexing))
            {
                msg += "请选择车型<br />";
            }
            if (string.IsNullOrEmpty(paperrule.kemu))
            {
                msg += "请选择科目<br />";
            }
            if (paperrule.single==null)
            {
                msg += "请输入单选题比例<br />";
            }
            if (paperrule.judge == null)
            {
                msg += "请输入判断题比例<br />";
            }
            if (paperrule.multi == null)
            {
                msg += "请输入多选题比例<br />";
            }

            if (paperrule.single + paperrule.judge + paperrule.multi != 100)
            {
                msg += "比例之和不为100";
            }

            return msg;
        }
    }
}
