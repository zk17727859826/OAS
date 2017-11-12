using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBFactoryEntity
{
    public class BuildResultEntity
    {
        /// <summary>
        /// sql语句
        /// </summary>
        public string sql { set; get; }

        private List<DBMemberEntity> _entities = new List<DBMemberEntity>();
        /// <summary>
        /// 参数实体信息
        /// </summary>
        public List<DBMemberEntity> entities
        {
            set
            {
                _entities = value != null ? value : new List<DBMemberEntity>();
            }
            get
            {
                return _entities;
            }
        }

        private List<DBMemberEntity> _updateentities = new List<DBMemberEntity>();
        /// <summary>
        /// 更新的数据实体
        /// </summary>
        public List<DBMemberEntity> conditionentities
        {
            get { return _updateentities; }
            set { _updateentities = value; }
        }
    }
}
