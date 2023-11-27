using AKVA.Assets.Vince.Scripts.AI;
using AKVA.Assets.Vince.Scripts.SceneManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Vince.Scripts.SceneManager
{
    public class Level5State : LevelState
    {
        public override void OnEnterState(LevelManager state)
        {
            foreach(ScientistStateManager scientist in state.scientists)
            {
                scientist.activate = true;
                scientist.moveOnly = true;
                state.StartCoroutine(MoveAI(scientist));
            }
        }

        public override void OnUpdateState(LevelManager state)
        {
        }

        IEnumerator MoveAI(ScientistStateManager scientist)
        {
            while (true)
            {
                yield return new WaitForSeconds(10f);

                if (scientist.targetIndex >= scientist.targets.Length-1)
                {
                    scientist.targetIndex = 2;

                }
                else
                {
                    scientist.targetIndex++;
                }

                if (Vector3.Distance(scientist.transform.position, scientist.targets[scientist.targets.Length - 1].position) > 2)
                {
                    scientist.activate = true;
                }
            }
        }
    }
}