using AKVA.Assets.Vince.Scripts.Environment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.AI
{
    public class SciMoveState : ScientistState
    {
        public override void OnEnterState(ScientistStateManager state)
        {
            state.pathFind.FindPath(state.currentTarget);
            state.scientistAnim.ChangeAnimState(state.scientistAnim.Walk);
        }

        public override void OnUpdateState(ScientistStateManager state)
        {
            CheckTargetDistance(state);
        }

        private void CheckTargetDistance(ScientistStateManager state)
        {
            if (Vector3.Distance(state.gameObject.transform.position, state.currentTarget.position) <= 1.5f)
            {
                if (!state.moveOnly)
                {
                    state.SwitchState(state.interactState);
                }
                else
                {
                    state.scientistAnim.ChangeAnimState(state.scientistAnim.Idle);
                }
            }
        }

        public override void OnCollisionEnter(ScientistStateManager state, Collider collider)
        {
            if (collider.name == "DoubleDoor")
            {
                DoubleDoor doubleDoor = collider.GetComponent<DoubleDoor>();
                state.StartCoroutine(DoorEnabled(doubleDoor, true, 3f));
                //doubleDoor.ai = true;
                // doubleDoor.OpenDoor();
            }
        }

        public override void OnCollisionExit(ScientistStateManager state, Collider collider)
        {
            if (collider.name == "DoubleDoor")
            {
                DoubleDoor doubleDoor = collider.GetComponent<DoubleDoor>();
                state.StartCoroutine(DoorEnabled(doubleDoor, false, 0.1f));
            }
        }

        IEnumerator DoorEnabled(DoubleDoor door, bool enable,float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            door.ai = enable;
            door.EnableDoor = enable;
        }
    }
}