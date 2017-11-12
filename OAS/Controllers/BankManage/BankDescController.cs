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
    public class BankDescController : BaseController
    {
        public ActionResult Index()
        {
            GBL_BUTTON["OTHER"] = "设为重点错题";
            ModuleNo = "BankDesc";
            ViewBag.ToolBar = BuildPowerButtons();
            return View();
        }

        [HttpPost]
        public JsonResult Query(string title, string qtype, string ynpic, string ynanimal, int page = 1, int rows = 20)
        {
            Bank bank = new Bank();
            PaginModel pm = new PaginModel()
            {
                page = page,
                pagesize = rows
            };

            bool? ispic = null;
            if (!string.IsNullOrEmpty(ynpic))
            {
                ispic = ynpic == "Y";
            }

            bool? isanimal = null;
            if (!string.IsNullOrEmpty(ynanimal))
            {
                isanimal = ynanimal == "Y";
            }

            OperateResultModel orm = bank.QueryBanks(title, qtype, ispic, isanimal, true, null, pm);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        public PartialViewResult Edit(int? id)
        {
            Bank bank = new Bank();

            tbl_bank bankmodel = null;
            if (id != null)
            {
                bankmodel = bank.QueryBank(id.Value);
            }
            OperateResultModel orm = bank.QuerySections();
            List<tbl_section> sections = (List<tbl_section>)orm.rows;
            SelectList sectionlist = new SelectList(sections, "sid", "sectionname", bankmodel == null ? "" : bankmodel.section);
            ViewData["sectionlist"] = sectionlist;
            return PartialView(bankmodel);
        }

        [HttpPost]
        public JsonResult Add(tbl_bank bankmodel)
        {
            OperateResultModel orm = new OperateResultModel();
            orm.message = ValidInput(bankmodel);
            if (string.IsNullOrEmpty(orm.message))
            {
                UserModel um = SessionUser;
                DateTime dtnow = DateTime.Now;
                bankmodel.createdate = dtnow;
                bankmodel.creater = um.UserNo;
                bankmodel.editdate = dtnow;
                bankmodel.editer = um.UserNo;

                Bank bank = new Bank();
                bankmodel.id = bank.GetNewBankId();
                orm = bank.InsertBank(bankmodel);
                orm.rows = bankmodel;
            }
            else
            {
                orm.success = false;
            }
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult Update(tbl_bank bankmodel)
        {
            OperateResultModel orm = new OperateResultModel();
            bankmodel.picpath = bankmodel.picpath == null ? "" : bankmodel.picpath;
            bankmodel.animepath = bankmodel.animepath == null ? "" : bankmodel.animepath;
            bankmodel.answerdesc = bankmodel.answerdesc == null ? "" : bankmodel.answerdesc;
            orm.message = ValidInput(bankmodel);
            if (string.IsNullOrEmpty(orm.message))
            {
                bankmodel.editdate = DateTime.Now;
                bankmodel.editer = SessionUser.UserNo;
                Bank bank = new Bank();
                orm = bank.UpdateBank(bankmodel);
                orm.rows = bankmodel;
            }
            else
            {
                orm.success = false;
            }
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            Bank bank = new Bank();
            OperateResultModel orm = bank.DeleteBank(id);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult Single(int id)
        {
            Bank bank = new Bank();
            tbl_bank bankmodel = bank.QueryBank(id);
            return Json(new
            {
                success = true,
                data = bankmodel
            });
        }

        [HttpPost]
        public JsonResult seterror(int id)
        {
            Bank bank = new Bank();
            OperateResultModel orm = bank.SetErrorType(id, SessionUser.UserNo);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        /// <summary>
        /// 验证输入的章节信息是否正常
        /// </summary>
        /// <param name="bankmodel">题库实体</param>
        /// <returns></returns>
        private string ValidInput(tbl_bank bankmodel)
        {
            string msg = string.Empty;

            if (string.IsNullOrEmpty(bankmodel.title))
            {
                msg += "请输入标题<br />";
            }
            if (string.IsNullOrEmpty(bankmodel.qtype))
            {
                msg += "请选择题目类型<br />";
            }
            if (string.IsNullOrEmpty(bankmodel.options))
            {
                msg += "请输入供选项目<br />";
            }
            if (string.IsNullOrEmpty(bankmodel.answer))
            {
                msg += "请选择答案<br />";
            }
            if (string.IsNullOrEmpty(bankmodel.section))
            {
                msg += "请选择章节<br />";
            }
            if (bankmodel.qtype == "单选题")
            {
                if (bankmodel.answer.IndexOf(",") > -1)
                {
                    msg += "单选题只能选择一个答案";
                }
                else if (bankmodel.answer.IndexOf("错") > -1 || bankmodel.answer.IndexOf("对") > -1)
                {
                    msg += "单选题只能在[A、B、C、D]中进行选择";
                }
            }
            else if (bankmodel.qtype == "多选题")
            {
                if (bankmodel.answer.IndexOf("错") > -1 || bankmodel.answer.IndexOf("对") > -1)
                {
                    msg += "多选题只能在[A、B、C、D]中进行选择";
                }
            }
            else if (bankmodel.qtype == "判断题")
            {
                if (bankmodel.answer.IndexOf(",") > -1)
                {
                    msg += "判断题只能选择一个答案";
                }
                else if (bankmodel.answer != "错" && bankmodel.answer != "对")
                {
                    msg += "判断题只能在[错、对]中进行选择";
                }
            }
            if (!string.IsNullOrEmpty(bankmodel.picpath) && !string.IsNullOrEmpty(bankmodel.animepath))
            {
                msg += "图片路径 和 动画路径不能同时存在";
            }

            if (!string.IsNullOrEmpty(bankmodel.picpath))
            {
                bankmodel.picpath = bankmodel.picpath.Replace("\\", "/");
            }

            if (!string.IsNullOrEmpty(bankmodel.animepath))
            {
                bankmodel.animepath = bankmodel.animepath.Replace("\\", "/");
            }

            if (!string.IsNullOrEmpty(bankmodel.answerpicpath))
            {
                bankmodel.answerpicpath = bankmodel.answerpicpath.Replace("\\", "/");
            }

            return msg;
        }
    }
}
