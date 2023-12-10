using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.AI
{
    public class DeathState : AIState
    {
        public override void OnEnterState(AIStateManager state)
        {
            state.electricityVfx.SetActive(true);
            state.robotAnim.ChangeAnimState("Elect");
        }

        public override void OnUpdateState(AIStateManager state)
        {
            state.GetComponent<Animator>().applyRootMotion = true;
        }
        public override void OnCollisionEnter(AIStateManager state, Collider collider)
        {
        }
    }
}

