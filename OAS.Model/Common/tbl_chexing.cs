using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Model.Common
{
    public class tbl_chexing
    {
        /// <summary>
        /// 车型
        /// </summary>
        public string chexing { set; get; }

        /// <summary>
        /// 车型描述
        /// </summary>
        public string chexingdesc { set; get; }

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
    }
}
