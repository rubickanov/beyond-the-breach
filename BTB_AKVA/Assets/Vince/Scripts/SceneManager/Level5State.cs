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
        bool[] moveAI;

        public override void OnEnterState(LevelManager state)
        {
            MoveAIToScanner(state.scientist);
            moveAI = new bool[5];
        }

        public override void OnUpdateState(LevelManager state)
        {
            MoveAIToPathPoints(state);
        }

        private void MoveAIToPathPoints(LevelManager state)
        {
            if (Vector3.Distance(state.scientist.transform.position, state.scientist.targets[0].position) <= 1 && !moveAI[0])
            {
                moveAI[0] = true;
                moveAI[1] = false;
                state.StartCoroutine(MoveToAIToNextLocation(state));
            }
            else if (!moveAI[1] && moveAI[0] && Vector3.Distance(state.scientist.transform.position, state.scientist.targets[1].position) < 2)
            {
                state.scientist.targetIndex = 0;
                state.StartCoroutine(ResetAIPath(state));
                //state.scientist.SwitchState(state.scientist.moveState);
                moveAI[1] = true;
            }
        }

        IEnumerator ResetAIPath(LevelManager state)
        {
            yield return new WaitForSeconds(2);
            state.scientist.transform.position = state.scientist.targets[state.scientist.targets.Length - 1].position;
            state.scientist.targetIndex = 0;
            state.scientist.SwitchState(state.scientist.moveState);
            moveAI[0] = false;
        }

        IEnumerator MoveToAIToNextLocation(LevelManager state)
        {
            yield return new WaitForSeconds(20);
            state.scientist.targetIndex++;
            state.scientist.SwitchState(state.scientist.moveState);
        }

        void MoveAIToScanner(ScientistStateManager scientist)
        {
            scientist.moveOnly = true;
            scientist.activate = true;
        }

    }
}