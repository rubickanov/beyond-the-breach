using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.AI
{
    public class PickUpState : RobotState
    {
        public override void OnCollisionEnter(RobotStateManager state, Collider collider)
        {
        }

        public override void OnEnterState(RobotStateManager state)
        {
            state.robotAnim.ChangeAnimState(state.robotAnim.Robot_PickItem);
        }

        public override void OnUpdateState(RobotStateManager state)
        {
            LookForObjectsToPick(state);
        }

        private void LookForObjectsToPick(RobotStateManager state)
        {
            Collider[] colliders = Physics.OverlapSphere(state.transform.position, state.sphereRadius, state.objectsToPick);

            if (colliders.Length > 0)
            { 
                if (state.pickUp)
                {
                    state.objOnHand = colliders[0].gameObject;
                    state.targetIndex++;
                    state.currentTarget = state.targets[state.targetIndex];
                    state.StartCoroutine(SwitchDelay(state, 2f));
                    state.pickUp = false;
                }

                if (state.objOnHand != null) { return; }
                state.transform.LookAt(colliders[0].transform);
            }
        }


        IEnumerator SwitchDelay(RobotStateManager state, float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            state.SwitchState(state.moveState);
        }
    }
}
