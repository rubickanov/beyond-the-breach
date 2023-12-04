using AKVA.Assets.Vince.Scripts.AI;
using AKVA.Assets.Vince.Scripts.SceneManager;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

namespace Assets.Vince.Scripts.SceneManager
{
    public class Level5State : LevelState
    {
        public override void OnEnterState(LevelManager state)
        {
            state.scientistLevel5.SetActive(true);
        }

        public override void OnUpdateState(LevelManager state)
        {
        }

        void ProceedToNextLevel(LevelManager state)
        {
            if (Vector3.Distance(state.player.transform.position, state.checkPoints[3].position) < 1)
            {
                state.SwitchState(state.level5);
            }
        }
    }
}