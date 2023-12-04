using AKVA.Animations;
using AKVA.Assets.Vince.Scripts.Astar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.AI
{
    public class BTMindControlled : TreeNode
    {
        Transform transform;
        ScientistBT scientistBT;
        bool mindControlled;
        ScientistAIAnim anim;
        public BTMindControlled(Transform transform, bool mindControlled)
        {
            this.transform = transform;
            this.mindControlled = mindControlled;
            anim = transform.GetComponent<ScientistAIAnim>();
            scientistBT = transform.GetComponent<ScientistBT>();
        }

        public override NodeState Execute()
        {
            if(scientistBT.isMindControlled)
            {
                scientistBT.electricityVfx.SetActive(true);
                transform.GetComponent<MoveAI>().StopMoving();
                anim.ChangeAnimState(anim.Robot_Idle);
                state = NodeState.SUCCESS;
                return state;
            }
            else
            {
                state = NodeState.FAILURE;
                return state;
            }
        }
    }
}
