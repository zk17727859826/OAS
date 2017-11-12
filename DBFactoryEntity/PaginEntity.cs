using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBFactoryEntity
{
    public class PaginModel
    {
        private int _page = 1;
        /// <summary>
        /// 页码
        /// </summary>
        public int page
        {
            set
            {
                _page = value;
            }
            get
            {
                return _page;
            }
        }

        private int _pagesize = 2;
        /// <summary>
        /// 每页的数量
        /// </summary>
        public int pagesize
        {
            set
            {
                _pagesize = value;
            }
            get
            {
                return _pagesize;
            }
        }

        /// <summary>
        /// 开始的记录位置
        /// </summary>
        public int startpos
        {
            get
            {
                return (page - 1) * pagesize + 1;
            }
        }

        /// <summary>
        /// 结束的记录位置
        /// </summary>
        public int endpos
        {
            get
            {
                return startpos + pagesize - 1;
            }
        }
    }
}
