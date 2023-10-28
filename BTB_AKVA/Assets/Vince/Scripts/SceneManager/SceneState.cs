using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneState
{
    public abstract void OnEnterState(SceneStateManager state);
    public abstract void OnUpdateState(SceneStateManager state);
    public abstract void OnExitState(SceneStateManager state);

}


