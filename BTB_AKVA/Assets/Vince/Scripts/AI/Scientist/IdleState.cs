using AKVA.Assets.Vince.Scripts.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Vince.Scripts.AI
{
    public class IdleState : ScientistState
    {
        public override void OnCollisionEnter(ScientistStateManager state, Collider collider)
        {
        }

        public override void OnCollisionExit(ScientistStateManager state, Collider collider)
        {
        }

        public override void OnEnterState(ScientistStateManager state)
        {
            state.sciAnim.ChangeAnimState(state.sciAnim.Robot_Idle);
        }

        public override void OnUpdateState(ScientistStateManager state)
        {
        }
    }
}
