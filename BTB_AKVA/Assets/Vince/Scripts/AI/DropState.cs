using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.AI
{
    public class DropState : AIState
    {
        public override void OnCollisionEnter(AIStateManager state, Collider collider)
        {
        }

        public override void OnEnterState(AIStateManager state)
        {
            Debug.Log("DropState");
            LookForAPlaceToDrop(state);
        }

        private void LookForAPlaceToDrop(AIStateManager state)
        {
            Collider[] colliders = Physics.OverlapSphere(state.transform.position, state.sphereRadius, state.placesToDrop);
            Debug.Log(colliders.Length);

            if (colliders.Length > 0 && state.objOnHand != null)
            {
                state.objOnHand.transform.position = colliders[0].transform.position;
                state.objOnHand.GetComponent<Collider>().isTrigger = false;
                state.objOnHand = null;
                state.moveOnly = true;
                state.currentTarget = state.thirdTarget;
                state.StartCoroutine(SwitchStateDelay(state, 1f));
            }
        }

        IEnumerator SwitchStateDelay(AIStateManager state, float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            state.SwitchState(state.moveState);
        }

        public override void OnUpdateState(AIStateManager state)
        {

        }
    }
}
