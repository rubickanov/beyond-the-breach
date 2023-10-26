using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : AIState
{
    public override void OnEnterState(AIStateManager state)
    {
        state.pathFind.FindPath(state.currentTarget);
    }

    public override void OnUpdateState(AIStateManager state)
    {
        CheckTargetDistance(state);
    }

    private void CheckTargetDistance(AIStateManager state)
    {
        if(Vector3.Distance(state.gameObject.transform.position, state.currentTarget.position) <= 2.5f)
        {
            Debug.Log("Reached Destination");
            if(state.objOnHand == null)
            {
                state.SwitchState(state.pickUpState);
            }
            else
            {
                state.objOnHand.GetComponent<Collider>().isTrigger = false;
                state.currentState = null;
                state.objOnHand = null;
            }
        }
    }

    public override void OnCollisionEnter(AIStateManager state, Collider collider)
    {
    }
}
