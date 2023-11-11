using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.AI
{
    public class DeathState : RobotState
    {
        public override void OnEnterState(RobotStateManager state)
        {
            state.robotAnim.ChangeAnimState(state.robotAnim.Robot_Death);
        }

        public override void OnUpdateState(RobotStateManager state)
        {
        }
        public override void OnCollisionEnter(RobotStateManager state, Collider collider)
        {
        }
    }
}

