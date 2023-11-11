using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.AI
{
    public abstract class ScientistState
    {
        public abstract void OnEnterState(ScientistStateManager state);
        public abstract void OnUpdateState(ScientistStateManager state);
        public abstract void OnCollisionEnter(ScientistStateManager state, Collider collider);
        public abstract void OnCollisionExit(ScientistStateManager state, Collider collider);
    }
}
