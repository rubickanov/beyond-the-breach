using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.AI
{
    public class SciMoveState : ScientistState
    {
        public override void OnCollisionEnter(ScientistStateManager state, Collider collider)
        {

        }

        public override void OnCollisionExit(ScientistStateManager state, Collider collider)
        {
        }

        public override void OnEnterState(ScientistStateManager state)
        {
            state.sciAnim.ChangeAnimState(state.sciAnim.Robot_Walk);
            state.moveAi.FindPath(state.targets[state.targetIndex]);
        }

        public override void OnUpdateState(ScientistStateManager state)
        {
            if(Vector3.Distance(state.transform.position, state.targets[state.targetIndex].position) < 2)
            {
                if (!state.moveOnly)
                {
                    state.SwitchState(state.interactState);
                }
                else
                {
                    state.sciAnim.ChangeAnimState(state.sciAnim.Robot_Idle);
                }
            }
        }
    }
}
