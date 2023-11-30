using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.SceneManager
{
    public class Level1State : LevelState
    {
        public override void OnEnterState(LevelManager state)
        {
        }

        public override void OnUpdateState(LevelManager state)
        {
            ProceedToNextLevel(state);
        }

        void ProceedToNextLevel(LevelManager state)
        {
            if (Vector3.Distance(state.player.transform.position, state.checkPoints[0].position) < 1)
            {
                state.SwitchState(state.level2);
            }
        }
    }
}
