using AKVA.Animations;
using AKVA.Assets.Vince.Scripts.Astar;
using AKVA.Assets.Vince.Scripts.Environment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.AI
{
    public class BTInteractingObj : TreeNode
    {
        Transform transform;
        private float currentTime;
        private float interactionTime = 5.0f;

        ScientistAIAnim anim;
        ScientistBT scientistBT;
        bool animated;
        public BTInteractingObj(Transform transform)
        {
            anim = transform.GetComponent<ScientistAIAnim>();
            this.transform = transform;
            scientistBT = transform.GetComponent<ScientistBT>();
        }

        public override NodeState Execute()
        {
            if (!animated)
            {
                animated = true;
                scientistBT.interacting = true;
                scientistBT.StartCoroutine(TriggerAnimation(1f));
            }

            if (currentTime < interactionTime)
            {
                scientistBT.transform.rotation = Quaternion.LookRotation(Vector3.forward);
                currentTime += Time.deltaTime;
                state = NodeState.RUNNING;
            }
            else
            {
                scientistBT.StartCoroutine(ResetTask(3));
                scientistBT.interacting = false;
                state = NodeState.FAILURE;
                return state;
            }
            return state;
        }

        IEnumerator TriggerAnimation(float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            anim.ChangeAnimState(anim.Robot_Interaction);
        }

        IEnumerator ResetTask(float time)
        {
            yield return new WaitForSeconds(time);
            currentTime = 0;
            animated = false;
        }
    }
}
