using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.SceneManager
{
    public abstract class LevelState
    {
        public abstract void OnEnterState(LevelManager state);
        public abstract void OnUpdateState(LevelManager state);
    }
}
