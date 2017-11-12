using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Model.Common
{
    public class JsonTree
    {
        private string _id;
        private string _text;
        private string _state = "open";
        private Dictionary<string, string> _attributes = new Dictionary<string, string>();
        private List<JsonTree> _children = new List<JsonTree>();

        /// <summary>
        /// 键值
        /// </summary>
        public string id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// 显示值
        /// </summary>
        public string text
        {
            get { return _text; }
            set { _text = value; }
        }

        /// <summary>
        /// 节点状态，'open' 或 'closed'，默认：'open'。如果为'closed'的时候，将不自动展开该节点。
        /// </summary>
        public string state
        {
            get { return _state; }
            set { _state = value; }
        }

        /// <summary>
        /// 被添加到节点的自定义属性
        /// </summary>
        public Dictionary<string, string> attributes
        {
            get { return _attributes; }
            set { _attributes = value; }
        }

        public bool Checked { set; get; }

        /// <summary>
        /// 子节点
        /// </summary>
        public List<JsonTree> children
        {
            get { return _children; }
            set { _children = value; }
        }
    }
}
