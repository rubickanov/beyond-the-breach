using AKVA.Animations;
using AKVA.Assets.Vince.Scripts.Astar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.AI
{
    public class BTKillPlayer : TreeNode
    {
        Transform transform;
        ScientistAIAnim anim;
        public BTKillPlayer(Transform transform)
        {
            this.transform = transform;
            anim = transform.GetComponent<ScientistAIAnim>();
        }

        public override NodeState Execute()
        {
            anim.ChangeAnimState(anim.Robot_Idle);
            transform.GetComponent<MoveAI>().StopMoving();
            Debug.Log("Player Dead");
            state = NodeState.RUNNING;
            return state;
        }
    }
}
