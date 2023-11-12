using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.AI
{
    public abstract class AIState
    {
        public abstract void OnEnterState(AIStateManager state);
        public abstract void OnUpdateState(AIStateManager state);
        public abstract void OnCollisionEnter(AIStateManager state, Collider collider);
    }
}