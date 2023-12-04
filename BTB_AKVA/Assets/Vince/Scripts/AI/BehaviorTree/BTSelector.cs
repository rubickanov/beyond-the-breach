using AKVA.Assets.Vince.Scripts.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.AI
{
    public class BTSelector : TreeNode
    {
        public BTSelector() : base(){ }
        public BTSelector(List<TreeNode> children) : base(children) { }
        public override NodeState Execute()
        {
            foreach (TreeNode node in children)
            {
                switch (node.Execute())
                {
                    case NodeState.FAILURE:
                        continue;
                    case NodeState.SUCCESS:
                        state = NodeState.SUCCESS;
                        return state;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return state;
                    default:
                        continue;
                }
            }
            state = NodeState.FAILURE;
            return state;
        }
    }
}
