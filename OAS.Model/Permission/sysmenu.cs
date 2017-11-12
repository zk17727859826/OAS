using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Model.Permission
{
    public class sysmenu
    {
        /// <summary>
        /// 唯一键
        /// </summary>
        public List<string> keyfields { get { return new List<string>() { "menuno" }; } }

        /// <summary>
        /// 菜单编号
        /// </summary>
        public string menuno { set; get; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string menuname { set; get; }

        /// <summary>
        /// 菜单样式
        /// </summary>
        public string menuclass { set; get; }

        /// <summary>
        /// 菜单排序
        /// </summary>
        public int? menusort { set; get; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public string isvalid { set; get; }

        /// <summary>
        /// 菜单对应的URL
        /// </summary>
        public string menuurl { set; get; }

        /// <summary>
        /// 上级菜单编号
        /// </summary>
        public string parentno { set; get; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string creater { set; get; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? createdate { set; get; }

        /// <summary>
        /// 编辑人
        /// </summary>
        public string editer { set; get; }

        /// <summary>
        /// 编辑时间
        /// </summary>
        public DateTime? editdate { set; get; }

        public List<sysmenu> _children = new List<sysmenu>();
        /// <summary>
        /// 菜单的子菜单信息
        /// </summary>
        public List<sysmenu> children
        {
            set
            {
                _children = value != null ? value : new List<sysmenu>();
            }
            get
            {
                return _children;
            }
        }
    }
}
