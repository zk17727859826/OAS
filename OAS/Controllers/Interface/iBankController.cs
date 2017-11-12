using DBFactoryEntity;
using OAS.Bll;
using OAS.Model.Bank;
using OAS.Model.Jiax;
using OAS.Model.Permission;
using OAS.Model.Study;
using OAS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OAS.Controllers.Interface
{
    public class iBankController : Controller
    {
        /// <summary>
        /// 获得练习内容
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="xueyh">学员号</param>
        /// <param name="xingming">姓名</param>
        /// <param name="source">
        /// 来源 
        /// 1：网页 
        /// 2：APP 
        /// 3：小程序
        /// </param>
        /// <param name="kemu">
        /// 科目
        /// KM1：科目一
        /// KM4：科目四
        /// KMA：客车
        /// KMB：货车
        /// </param>
        /// <returns></returns>
        public JsonResult bank(string type, string xueyh, string xingming, string source, string kemu, string paperid)
        {
            try
            {
                JiaxInfo jiax = new JiaxInfo();
                tbl_xueyuan xueyuan = jiax.QueryXueyuan(xueyh);
                if (xueyuan != null)
                {
                    xingming = xueyuan.xingming;
                }


                Bank bll = new Bank();
                OperateResultModel orm = null;
                string papername = "";
                type = Server.UrlDecode(type);
                switch (type)
                {
                    case "顺序练习":
                    case "1-1"://所有数据-顺序
                        papername = "顺序练习";
                        orm = bll.QueryBanks(null, null, null, null, true, kemu, null);
                        break;
                    case "随机练习":
                    case "1-2"://所有数据-随机
                        papername = "随机练习";
                        orm = bll.QueryBanks(null, null, null, null, false, kemu, null);
                        break;
                    case "判断题":
                    case "1-3"://判断题-顺序
                        papername = "判断题";
                        orm = bll.QueryBanks(null, "判断题", null, null, true, kemu, null);
                        break;
                    case "1-4"://判断题-随机
                        orm = bll.QueryBanks(null, "判断题", null, null, false, kemu, null);
                        break;
                    case "单选题":
                    case "1-5"://单选题-顺序
                        papername = "单选题";
                        orm = bll.QueryBanks(null, "单选题", null, null, true, kemu, null);
                        break;
                    case "1-6"://单选题-随机
                        orm = bll.QueryBanks(null, "单选题", null, null, false, kemu, null);
                        break;
                    case "多选题":
                    case "1-7"://多选题-顺序
                        papername = "多选题";
                        orm = bll.QueryBanks(null, "多选题", null, null, true, kemu, null);
                        break;
                    case "1-8"://多选题-随机
                        orm = bll.QueryBanks(null, "多选题", null, null, false, kemu, null);
                        break;
                    case "图片题":
                    case "1-9"://图片题-顺序
                        papername = "图片题";
                        orm = bll.QueryBanks(null, null, true, null, true, kemu, null);
                        break;
                    case "1-10"://图片题-随机
                        orm = bll.QueryBanks(null, null, true, null, false, kemu, null);
                        break;
                    case "动画题":
                    case "1-11"://动画题-顺序
                        papername = "动画题";
                        orm = bll.QueryBanks(null, null, null, true, true, kemu, null);
                        break;
                    case "1-12"://动画题-随机
                        orm = bll.QueryBanks(null, null, null, true, false, kemu, null);
                        break;
                    case "未做题练习":
                    case "1-13"://未做题练习
                        papername = "未做题练习";
                        orm = bll.QueryBanksForNotStudy(xueyh);
                        break;
                    case "模拟考试":
                    case "随机试卷":
                    case "2-1":
                        orm = bll.QueryRandomPaper(kemu);
                        papername = "随机试卷";
                        break;
                    case "重点试卷":
                    case "2-2":
                        orm = bll.QueryBanksOfPaper(Convert.ToInt32(paperid));
                        tbl_paper papermodel = bll.QueryPaper(Convert.ToInt32(paperid));
                        papername = papermodel == null ? "重点试卷" : papermodel.papername;
                        break;
                    default:
                        throw new Exception("题目类型选择错误");
                }

                CommonBll commonbll = new CommonBll();
                string testno = commonbll.GetSerialNo("Record", "");
                bool istest = type.IndexOf("考试") > 0 || type.IndexOf("试卷") > 0;
                //把查询到的记录插入用户练习记录
                tbl_test_records record = new tbl_test_records()
                {
                    createdate = DateTime.Now,
                    creater = xueyh,
                    xingming = xingming,
                    oknum = 0,
                    source = source,
                    kemu = kemu,
                    testno = testno,
                    projecttype = papername,
                    testtype = istest ? "考试" : "练习",
                    totalnum = orm.total,
                    xueyh = xueyh
                };
                Study study = new Study();
                var tblist = (List<tbl_bank>)orm.rows;
                OperateResultModel ormtest = new OperateResultModel();
                ormtest.success = true;
                if (tblist != null && tblist.Count() != 0)
                {
                    ormtest = study.InsertTestRecord(record);
                }
                if (ormtest.success)
                {
                    orm.message = testno;
                    return JsonResultHelper.ConvertToJsonResult(orm);
                }
                else
                {
                    return JsonResultHelper.ConvertToJsonResult(ormtest);
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message,
                    total = 0
                });
            }
        }

        /// <summary>
        /// 保存练习或考试记录
        /// </summary>
        /// <param name="model">主表实体</param>
        /// <param name="qid">题目ID</param>
        /// <param name="answer">答案</param>
        /// <returns></returns>
        public JsonResult save(tbl_test_records model, int qid, string answer)
        {
            Study study = new Study();
            OperateResultModel orm = study.UpdateTestSelect(model, qid, answer);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        /// <summary>
        /// 学习记录
        /// </summary>
        /// <param name="xueyh">学员号</param>
        /// <returns></returns>
        public JsonResult score(string xueyh, int page = 1)
        {
            PaginModel pm = new PaginModel()
            {
                page = page,
                pagesize = 15
            };
            Study study = new Study();
            OperateResultModel orm = study.QueryTestRecords(xueyh, "", pm);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }
    }
}
