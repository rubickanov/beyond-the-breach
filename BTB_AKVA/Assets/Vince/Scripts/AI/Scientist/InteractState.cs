using AKVA.Assets.Vince.Scripts.Environment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.AI
{
    public class InteractState : ScientistState
    {
        public override void OnEnterState(ScientistStateManager state)
        {
            state.scientistAnim.ChangeAnimState(state.scientistAnim.MachineInteraction);
            if(state.targets.Length > 1)
            {
                state.moveOnly = true;
                state.StartCoroutine(StartWalking(state));
            }
        }

        public override void OnUpdateState(ScientistStateManager state)
        {
           
        }

        IEnumerator StartWalking(ScientistStateManager state)
        {
            yield return new WaitForSeconds(3);
            state.targetIndex++;
            state.currentTarget = state.targets[state.targetIndex];
            state.SwitchState(state.moveState);
        }

        public override void OnCollisionEnter(ScientistStateManager state, Collider collider)
        {
        }

        public override void OnCollisionExit(ScientistStateManager state, Collider collider)
        {
        }

        IEnumerator DoorEnabled(DoubleDoor door, bool enable, float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            door.EnableDoor = enable;
        }
    }
}
