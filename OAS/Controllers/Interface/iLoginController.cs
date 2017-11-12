using DBFactoryEntity;
using OAS.Bll;
using OAS.Model.Jiax;
using OAS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OAS.Controllers.Interface
{
    public class iLoginController : Controller
    {
        /// <summary>
        /// 验证用户是否合法
        /// </summary>
        /// <param name="xueyh">学员号</param>
        /// <param name="password">学员密码</param>
        /// <param name="jiaxid">驾校ID</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Valid(string xueyh, string password, string jiaxid = "1")
        {
            try
            {
                xueyh = (xueyh ?? "").ToUpper();
                JiaxInfo bll = new JiaxInfo();
                bool bolValid = bll.IsValid(xueyh, password, jiaxid);
                if (bolValid == false) throw new Exception("用户名或密码错");
                tbl_xueyuan user = bll.QueryXueyuan(xueyh);
                return Json(new
                {
                    success = true,
                    user = user
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }

        }

        /// <summary>
        /// 更新用户密码
        /// </summary>
        /// <param name="xueyh">学员号</param>
        /// <param name="oldpassword">旧密码</param>
        /// <param name="newoldpassword">新密码</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdatePassword(string xueyh, string oldpassword, string newpassword)
        {
            JiaxInfo bll = new JiaxInfo();
            OperateResultModel orm = bll.UpdatePassowrd(xueyh, oldpassword, newpassword, xueyh);
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        /// <summary>
        /// 查询学员信息
        /// </summary>
        /// <param name="xueyh">学员号</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult User(string xueyh)
        {
            try
            {
                JiaxInfo bll = new JiaxInfo();
                tbl_xueyuan user = bll.QueryXueyuan(xueyh);
                return Json(new
                {
                    success = true,
                    user = user
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        /// <summary>
        /// 驾校列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Jiax()
        {
            JiaxInfo jiaxinfo = new JiaxInfo();
            OperateResultModel orm = jiaxinfo.QueryJiaxes("", "");
            return JsonResultHelper.ConvertToJsonResult(orm);
        }
    }
}
