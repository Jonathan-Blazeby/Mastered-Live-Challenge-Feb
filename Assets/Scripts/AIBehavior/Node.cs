using System.Collections.Generic;

namespace BehaviorTree
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    public class Node
    {
        #region Fields
        protected NodeState state;

        public Node parent;
        protected List<Node> children = new List<Node>();

        private Dictionary<string, object> _dataContext = new Dictionary<string, object>();
        #endregion

        #region Constructors
        public Node()
        {
            parent = null;
        }
        public Node(List<Node> children)
        {
            foreach (Node child in children)
            {
                AttachChild(child);
            }

        }
        #endregion

        #region Private Methods
        private void AttachChild(Node node)
        {
            node.parent = this;
            children.Add(node);
        }
        #endregion

        #region Public Methods
        public virtual NodeState Evaluate() => NodeState.FAILURE;

        public void SetData(string key, object value)
        {
            _dataContext[key] = value;
        }

        //Recursively called up tree to get data
        public object GetData(string key)
        {
            object value = null;
            if(_dataContext.TryGetValue(key, out value)) { return value; }

            Node node = parent;
            while(node != null)
            {
                value = node.GetData(key);
                if(value != null) { return value; }
                node = node.parent;
            }
            return null;
        }

        public bool ClearData(string key)
        {
            object value = null;
            if (_dataContext.ContainsKey(key)) 
            {
                _dataContext.Remove(key);
                return true; 
            }

            Node node = parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared) { return true; }
                node = node.parent;
            }
            return false;
        }
        #endregion
    }
}
