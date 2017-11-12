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
    public class ChapterController : BaseController
    {
        public ActionResult Index()
        {
            ModuleNo = "Chapter";
            ViewBag.ToolBar = BuildPowerButtons();
            return View();
        }

        [HttpPost]
        public JsonResult Query()
        {
            Bank bank = new Bank();
            OperateResultModel orm = bank.QuerySections();
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        public PartialViewResult Edit(string sid)
        {
            tbl_section section = null;
            if (!string.IsNullOrEmpty(sid))
            {
                Bank bank = new Bank();
                section = bank.QuerySection(sid);
            }
            return PartialView(section);
        }

        [HttpPost]
        public JsonResult Add(tbl_section section)
        {
            OperateResultModel orm = new OperateResultModel();
            orm.message = ValidInput(section);

            if (string.IsNullOrEmpty(orm.message))
            {
                UserModel um = SessionUser;
                DateTime dtnow = DateTime.Now;
                section.createdate = dtnow;
                section.creater = um.UserNo;
                section.editdate = dtnow;
                section.editer = um.UserNo;

                Bank bank = new Bank();
                orm = bank.InsertSection(section);
                orm.rows = section;
            }
            else
            {
                orm.success = false;
            }
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult Update(tbl_section section)
        {
            OperateResultModel orm = new OperateResultModel();
            orm.message = ValidInput(section);
            if (string.IsNullOrEmpty(orm.message))
            {
                section.editdate = DateTime.Now;
                section.editer = SessionUser.UserNo;
                Bank bank = new Bank();
                orm = bank.UpdateSection(section);
                orm.rows = section;
            }
            else
            {
                orm.success = false;
            }
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult Delete(string sid)
        {
            Bank bank = new Bank();
            OperateResultModel orm = bank.DeleteSection(sid);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        /// <summary>
        /// 验证输入的章节信息是否正常
        /// </summary>
        /// <param name="section">章节实体</param>
        /// <returns></returns>
        private string ValidInput(tbl_section section)
        {
            string msg = string.Empty;

            if (string.IsNullOrEmpty(section.sid))
            {
                msg += "请输入角色编号<br />";
            }
            if (string.IsNullOrEmpty(section.sectionname))
            {
                msg += "请输入角色名称<br />";
            }
            return msg;
        }
    }
}
