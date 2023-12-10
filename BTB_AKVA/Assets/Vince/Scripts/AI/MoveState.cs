using log4net.Util;
using PlasticPipe.PlasticProtocol.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.AI
{
    public class MoveState : AIState
    {
        public override void OnEnterState(AIStateManager state)
        {
            //state.pathFind.FindPath(state.currentTarget);
            WalkAnimation(state);
        }

        public override void OnUpdateState(AIStateManager state)
        {
            CheckTargetDistance(state);
        }

        private void CheckTargetDistance(AIStateManager state)
        {
            if (Vector3.Distance(state.gameObject.transform.localPosition, state.currentTarget.localPosition) > .1f)
            {
                state.transform.localPosition = Vector3.MoveTowards(state.transform.localPosition, state.currentTarget.localPosition, 3 * Time.deltaTime);

                Quaternion targetRotation = Quaternion.LookRotation(state.currentTarget.localPosition - state.transform.localPosition);
                targetRotation.eulerAngles = new Vector3(0f, targetRotation.eulerAngles.y, 0f);
                state.transform.localRotation = targetRotation;
            }
            else
            {
                //state.StartCoroutine(PlaceAIToPosition(state, state.currentTarget.localPosition));
                state.transform.rotation = Quaternion.identity;
                state.robotAnim.ChangeAnimState(state.robotAnim.Robot_Idle);
            }

            if (Vector3.Distance(state.gameObject.transform.localPosition, state.currentTarget.localPosition) <= .1f)
            {
                if (!state.moveOnly)
                {
                    if (state.objOnHand == null)
                    {
                        state.StartCoroutine(SwitchStateDelay(state, state.pickUpState, 1f));
                    }
                    else
                    {
                        state.SwitchState(state.dropState);
                    }
                }
                
            }
        }


        void WalkAnimation(AIStateManager state)
        {
            if (state.objOnHand != null)
            {
                state.robotAnim.ChangeAnimState(state.robotAnim.Robot_CarryWalk);
            }
            else
            {
                state.robotAnim.ChangeAnimState(state.robotAnim.Robot_Walk);
            }
        }

        public override void OnCollisionEnter(AIStateManager state, Collider collider)
        {
        }

        IEnumerator PlaceAIToPosition(AIStateManager state, Vector3 targetPos, float duration = .1f)
        {
            yield return new WaitForSeconds(0.5f);

            float elapsedTime = 0f;
            Vector3 startingPos = state.transform.localPosition;
            if (Vector3.Distance(state.transform.localPosition, targetPos) > 2)
            {
                state.robotAnim.ChangeAnimState(state.robotAnim.Robot_Walk);
            }
            while (elapsedTime < duration)
            {
                yield return null;

                elapsedTime += Time.deltaTime;

                float t = Mathf.Clamp01(elapsedTime / duration);

                state.transform.localPosition = Vector3.Lerp(startingPos, targetPos, t);
            }
            state.transform.localPosition = targetPos;
        }

        IEnumerator SwitchStateDelay(AIStateManager state, AIState aiState, float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            state.SwitchState(aiState);
        }
    }
}
