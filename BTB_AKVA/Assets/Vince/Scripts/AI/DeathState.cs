using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.AI
{
    public class DeathState : AIState
    {
        public override void OnEnterState(AIStateManager state)
        {
            state.robotAnim.ChangeAnimState(state.robotAnim.Robot_Death);
        }

        public override void OnUpdateState(AIStateManager state)
        {
        }
        public override void OnCollisionEnter(AIStateManager state, Collider collider)
        {
        }
    }
}

