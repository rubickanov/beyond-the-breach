using AKVA.Animations;
using AKVA.Assets.Vince.Scripts.Astar;
using AKVA.Vince.SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.AI
{
    public class BTKillPlayer : TreeNode
    {
        Transform transform;
        ScientistAIAnim anim;
        ScientistBT sciBT;
        public BTKillPlayer(Transform transform, BoolReference playerDead)
        {
            this.transform = transform;
            anim = transform.GetComponent<ScientistAIAnim>();
            sciBT = transform.GetComponent<ScientistBT>();
        }

        public override NodeState Execute()
        {
            anim.ChangeAnimState(anim.Robot_Idle);
            transform.GetComponent<MoveAI>().StopMoving();
            Debug.Log("Player Dead");
            sciBT.playerDied.value = true;
            state = NodeState.SUCCESS;
            return state;
        }
    }
}
