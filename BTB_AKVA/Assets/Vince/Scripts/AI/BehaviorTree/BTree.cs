using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.AI
{
    public abstract class BTree : MonoBehaviour
    {
        private TreeNode root = null;

        private void Awake()
        {
            root = SetupTree();
        }

        private void Update()
        {
            if (root != null)
            {
                root.Execute();
            }
        }

        protected abstract TreeNode SetupTree();
    }
}
