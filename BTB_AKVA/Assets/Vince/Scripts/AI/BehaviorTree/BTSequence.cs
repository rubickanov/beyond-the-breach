using AKVA.Assets.Vince.Scripts.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.AI
{
    public class BTSequence : TreeNode
    {
        public BTSequence() : base() { }
        public BTSequence(List<TreeNode> children) : base(children) { }
        public override NodeState Execute()
        {
            bool childIsRunning = false;

            foreach (TreeNode node in children)
            {
                switch (node.Execute())
                {
                    case NodeState.FAILURE:
                        state = NodeState.FAILURE;
                        return state;
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        childIsRunning = true;
                        continue;
                    default:
                        state = NodeState.SUCCESS;
                        return state;
                }
            }
            state = childIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return state;
        }
    }
}
