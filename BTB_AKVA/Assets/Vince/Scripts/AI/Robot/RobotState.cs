using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.AI
{
    public abstract class RobotState
    {
        public abstract void OnEnterState(RobotStateManager state);
        public abstract void OnUpdateState(RobotStateManager state);
        public abstract void OnCollisionEnter(RobotStateManager state, Collider collider);
    }
}