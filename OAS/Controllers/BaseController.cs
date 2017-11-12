using OAS.Bll;
using OAS.Model.Permission;
using OAS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using tg.Helper;

namespace OAS.Controllers
{
    public class BaseController : Controller
    {
        Permission permission = new Permission();
        /// <summary>
        /// 新键值
        /// </summary>
        public string NewGuid
        {
            get { return Guid.NewGuid().ToString().ToUpper().Replace("-", ""); }
        }

        /// <summary>
        /// 模块代码
        /// </summary>
        public string ModuleNo
        {
            set;
            get;
        }

        private Dictionary<string, string> _GBL_BUTTON = null;
        /// <summary>
        /// 权限按钮显示文本
        /// </summary>
        public Dictionary<string, string> GBL_BUTTON
        {
            get
            {
                if (_GBL_BUTTON == null)
                {
                    _GBL_BUTTON = new Dictionary<string, string>();
                    _GBL_BUTTON["ADD"] = "添加";
                    _GBL_BUTTON["UPDATE"] = "修改";
                    _GBL_BUTTON["DELETE"] = "删除";
                    _GBL_BUTTON["POWER"] = "权限";
                    _GBL_BUTTON["FOROBJECT"] = "对象";
                    _GBL_BUTTON["PASSWORD"] = "设置密码";
                    _GBL_BUTTON["TEST"] = "测试";
                    _GBL_BUTTON["OTHER"] = "其它";
                }
                return _GBL_BUTTON;
            }
            set
            {
                _GBL_BUTTON = value;
            }
        }


        public UserModel SessionUser
        {
            set
            {
                if (value == null)
                {
                    FormsAuthentication.SignOut();
                }
                else
                {
                    UserModel ui = value;
                    string strUi = ModelHelper.ToJson(ui);
                    FormsAuthentication.SetAuthCookie(strUi, false);
                }
            }
            get
            {
                string strUserInfo = System.Web.HttpContext.Current.User.Identity.Name;
                if (string.IsNullOrEmpty(strUserInfo))
                {
                    return null;
                }

                FormsAuthentication.SetAuthCookie(strUserInfo, false);
                UserModel ui = ModelHelper.ToModel<UserModel>(strUserInfo);
                if (string.IsNullOrEmpty(ui.UserNo))
                {
                    FormsAuthentication.SignOut();
                    ui = null;
                }
                return ui;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 判断用户在此模块是否有指定功能的权限
        /// </summary>
        /// <param name="strUserNo">用户编号</param>
        /// <param name="strModuleNo">模块代码</param>
        /// <param name="strActionNo">模块功能代码</param>
        /// <returns></returns>
        //private bool IsAuthorized(string strUserNo, string strModuleNo, string strActionNo)
        //{
        //
        //}

        /// <summary>  
        /// 绑定页面权限按钮列表  
        /// </summary>  
        public string BuildPowerButtons()
        {
            List<syspower> powers = permission.QueryUserPower(SessionUser.UserNo, ModuleNo);
            StringBuilder sb = new StringBuilder();
            string linkbtn_template = "<a id=\"btn{0}\" class=\"easyui-linkbutton\" style=\"float:left\" plain=\"true\" href=\"javascript:;\" icon=\"{1}\" {2} title=\"{3}\">{4}</a>";
            sb.Append("<a id=\"btnRefresh\" class=\"easyui-linkbutton\" style=\"float:left\"  plain=\"true\" href=\"javascript:;\" icon=\"icon-reload\"  title=\"重新加载\">刷新</a> ");
            sb.Append("<div class='datagrid-btn-separator'></div> ");
            if (powers.Count(p => p.powerno == "ADD") > 0)
            {
                sb.Append(string.Format(linkbtn_template, "ShowAdd", "icon-add", "", "", GBL_BUTTON["ADD"]));
            }
            if (powers.Count(p => p.powerno == "UPDATE") > 0)
            {
                sb.Append(string.Format(linkbtn_template, "ShowEdit", "icon-edit", "", "", GBL_BUTTON["UPDATE"]));
            }
            if (powers.Count(p => p.powerno == "DELETE") > 0)
            {
                sb.Append(string.Format(linkbtn_template, "Delete", "icon-remove", "", "", GBL_BUTTON["DELETE"]));
            }
            if (powers.Count(p => p.powerno == "OTHER") > 0)
            {
                sb.Append(string.Format(linkbtn_template, "Other", "icon-other", "", "", GBL_BUTTON["OTHER"]));
            }
            bool hasSplit = false;
            if (powers.Count(p => p.powerno == "POWER") > 0)
            {
                if (!hasSplit)
                {
                    sb.Append("<div class='datagrid-btn-separator'></div> ");
                }
                sb.Append(string.Format(linkbtn_template, "Power", "icon-report-key", "", "设置菜单对应的权限", "权限"));
            }
            if (powers.Count(p => p.powerno == "FOROBJECT") > 0)
            {
                if (!hasSplit)
                {
                    sb.Append("<div class='datagrid-btn-separator'></div> ");
                }
                sb.Append(string.Format(linkbtn_template, "ForObject", "icon-package", "", "设置菜单对应的对象", "对象"));
            }
            if (powers.Count(p => p.powerno == "PASSWORD") > 0)
            {
                if (!hasSplit)
                {
                    sb.Append("<div class='datagrid-btn-separator'></div> ");
                }
                sb.Append(string.Format(linkbtn_template, "EditPassword", "icon-key", "", "设置选中用户密码", "设置密码"));
            }
            hasSplit = false;
            if (powers.Count(p => p.powerno == "TEST") > 0)
            {
                if (!hasSplit)
                {
                    sb.Append("<div class='datagrid-btn-separator'></div> ");
                }
                sb.Append(string.Format(linkbtn_template, "Test", "icon-test", "", "测试按钮", "测试"));
            }
            return sb.ToString();
        }
    }
}
