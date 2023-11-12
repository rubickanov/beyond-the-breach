using log4net.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.AI
{
    public class MoveState : AIState
    {
        bool pickUpState;
        public override void OnEnterState(AIStateManager state)
        {
            state.pathFind.FindPath(state.currentTarget);
            WalkAnimation(state);
        }

        public override void OnUpdateState(AIStateManager state)
        {
            CheckTargetDistance(state);
        }

        private void CheckTargetDistance(AIStateManager state)
        {
            if (Vector3.Distance(state.gameObject.transform.position, state.currentTarget.position) <= 2f)
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
                else
                {
                    state.transform.rotation = Quaternion.identity;
                }
                //if (!state.rb.IsSleeping())
                //{
                //    if (state.objOnHand != null)
                //    {
                //        state.robotAnim.ChangeAnimState(state.robotAnim.Robot_CarryWalk);
                //    }
                //    else
                //    {
                //        state.robotAnim.ChangeAnimState(state.robotAnim.Robot_Walk);
                //    }
                //}
                //else
                //{
                state.robotAnim.ChangeAnimState(state.robotAnim.Robot_Idle);
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

        IEnumerator SwitchStateDelay(AIStateManager state, AIState aiState, float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            state.SwitchState(aiState);
        }
    }
}
