using AKVA.Animations;
using AKVA.Assets.Vince.Scripts.Environment;
using Codice.CM.Common.Tree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
                scientistBT.StartCoroutine(TriggerAnimation(1f));
            }

            if (currentTime < interactionTime)
            {
                currentTime += Time.deltaTime;
            }
            else
            {
                scientistBT.StartCoroutine(ResetTask(3));
                state = NodeState.FAILURE;
                return state;
            }
            state = NodeState.RUNNING;
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
            ClearData("target");
            animated = false;
        }
    }
}
