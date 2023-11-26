using AKVA.Assets.Vince.Scripts.AI;
using System;
using System.Collections;
using System.Collections.Generic;
using AKVA.Assets.Vince.Scripts.Environment;
using UnityEngine;


namespace AKVA.Assets.Vince.Scripts.AI
{
    public class SciInteractState : ScientistState
    {
        DoubleDoor door;
        public override void OnCollisionEnter(ScientistStateManager state, Collider collider)
        {
            if(collider.GetComponent<DoubleDoor>()!= null)
            {
                door = collider.GetComponent<DoubleDoor>();
            }
        }

        public override void OnCollisionExit(ScientistStateManager state, Collider collider)
        {
            if (door != null)
            {
                door.EnableDoor = false;
                door = null;
            }
        }

        public override void OnEnterState(ScientistStateManager state)
        {
            state.sciAnim.ChangeAnimState(state.sciAnim.Robot_Interaction);
            state.StartCoroutine(ChangeState(state));
        }

        public override void OnUpdateState(ScientistStateManager state)
        {

        }

        IEnumerator ChangeState(ScientistStateManager state)
        {
            yield return new WaitForSeconds(4f);
            state.moveOnly = true;
            state.targetIndex++;
            door.EnableDoor = true;
            state.SwitchState(state.moveState);
        }
    }
}
