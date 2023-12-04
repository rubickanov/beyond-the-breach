using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.AI
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }
    public class TreeNode
    {
        protected NodeState state;
        public TreeNode parent;
        protected List<TreeNode> children = new List<TreeNode>();
        private Dictionary<string, object> dataContext = new Dictionary<string, object>();
        public TreeNode()
        {
            parent = null;
        }

        public TreeNode(List<TreeNode> children)
        {
            foreach (TreeNode child in children)
            {
                Attach(child);
            }
        }

        public void Attach(TreeNode node)
        {
            node.parent = this;
            children.Add(node);
        }

        public virtual NodeState Execute() => NodeState.FAILURE;

        public void SetData(string key, object value)
        {
            dataContext[key] = value;
        }

        public object GetData(string key)
        {
            object value = null;
            if (dataContext.TryGetValue(key, out value))
            {
                return value;
            }
            TreeNode node = parent;
            while (node != null)
            {
                value = node.GetData(key);
                if (value != null)
                {
                    return value;
                }
                node = node.parent;
            }
            return null;
        }
        public bool ClearData(string key)
        {
            if (dataContext.ContainsKey(key))
            {
                dataContext.Remove(key);
                return true;
            }
            TreeNode node = parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                {
                    return true;
                }
                node = node.parent;
            }
            return false;
        }
    }
}

