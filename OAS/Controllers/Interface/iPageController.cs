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

namespace OAS.Controllers.Interface
{
    public class iPageController : Controller
    {
        /// <summary>
        /// 试卷列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult List()
        {
            Bank bank = new Bank();
            OperateResultModel orm = bank.QueryPapers(null, null);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        /// <summary>
        /// 试卷对应的题目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Bank(int id)
        {
            Bank bank = new Bank();
            OperateResultModel orm = bank.QueryPaperBanks(id);
            orm.rows = ModelHelper.ToModel<List<tbl_paper_bank>>(orm.rows as DataTable);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }
    }
}
