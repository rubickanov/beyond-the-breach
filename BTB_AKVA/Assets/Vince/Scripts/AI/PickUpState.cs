using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.AI
{
    public class PickUpState : AIState
    {
        public override void OnCollisionEnter(AIStateManager state, Collider collider)
        {
        }

        public override void OnEnterState(AIStateManager state)
        {
            state.robotAnim.ChangeAnimState(state.robotAnim.Robot_PickItem);
        }

        public override void OnUpdateState(AIStateManager state)
        {
            LookForObjectsToPick(state);
        }

        private void LookForObjectsToPick(AIStateManager state)
        {
            Collider[] colliders = Physics.OverlapSphere(state.transform.position, state.sphereRadius, state.objectsToPick);

            if (colliders.Length > 0)
            { 
                if (state.pickUp)
                {
                    state.objOnHand = colliders[0].gameObject;
                    state.targetIndex++;
                    state.currentTarget = state.pathPoints[state.targetIndex];
                    state.StartCoroutine(SwitchDelay(state, 2f));
                    state.pickUp = false;
                }

                if (state.objOnHand != null) { return; }
                state.transform.LookAt(colliders[0].transform);
            }
        }


        IEnumerator SwitchDelay(AIStateManager state, float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            state.SwitchState(state.moveState);
        }
    }
}
