using System;
using System.Collections;
using System.Collections.Generic;
using AKVA.Assets.Vince.Scripts.Environment;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace AKVA.Assets.Vince.Scripts.AI
{
    public class DropState : AIState
    {
        Transform colliderPos;
        public override void OnCollisionEnter(AIStateManager state, Collider collider)
        {
        }

        public override void OnEnterState(AIStateManager state)
        {
        }
        public override void OnUpdateState(AIStateManager state)
        {
            LookForAPlaceToDrop(state);
        }
        private void LookForAPlaceToDrop(AIStateManager state)
        {
            Collider[] colliders = Physics.OverlapSphere(state.transform.position, state.sphereRadius, state.placesToDrop);

            if (colliders.Length > 0 && state.objOnHand != null)
            {
                Transform colliderPos = colliders[0].GetComponentInChildren<Transform>().transform;

                if(colliders.Length > 1)
                {
                    foreach (Collider collider in colliders)
                    {
                        if (!collider.GetComponent<BatterySocket>().socketIsActive)
                        {
                            colliderPos = collider.transform;
                            break;
                        }
                    }
                }

                state.robotAnim.ChangeAnimState(state.robotAnim.Robot_DropItem);

                if (state.dropItem)
                {
                    state.objOnHand.transform.position = colliderPos.position;
                    state.objOnHand.GetComponent<InteractableBattery>().batteryOnHand = false;
                    state.objOnHand.GetComponent<Collider>().isTrigger = false;
                    state.objOnHand = null;
                    state.moveOnly = true;
                    state.targetIndex++;
                    state.currentTarget = state.pathPoints[state.targetIndex];
                    state.StartCoroutine(SwitchStateDelay(state, 1f));
                }
            }
        }

        IEnumerator SwitchStateDelay(AIStateManager state, float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            state.dropItem = false;
            state.robotAnim.ChangeAnimState(state.robotAnim.Robot_Walk);
            state.SwitchState(state.moveState);
        }
    }
}
