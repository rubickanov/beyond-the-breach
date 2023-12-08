using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.SceneManager
{
    public class Level6State : LevelState
    {
        public override void OnEnterState(LevelManager state)
        {
            foreach (GameObject scientist in state.scientistsLevel6)
            {
                scientist.SetActive(true);
            }
        }

        public override void OnUpdateState(LevelManager state)
        {
        }
    }
}
