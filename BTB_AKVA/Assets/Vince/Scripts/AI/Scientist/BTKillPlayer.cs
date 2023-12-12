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
        Transform player;
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

            Vector3 directionToTarget = (sciBT.player.position - transform.position).normalized;

            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            targetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);

            transform.rotation = targetRotation;

            sciBT.playerDied.value = true;
            state = NodeState.SUCCESS;
            return state;
        }
    }
}
