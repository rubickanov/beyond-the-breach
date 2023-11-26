using AKVA.Assets.Vince.Scripts.AI;
using AKVA.Assets.Vince.Scripts.Environment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.SceneManager
{
    public class LevelManager : MonoBehaviour
    {
        [HideInInspector] public GameObject player;
        public Transform[] checkPoints;

        [Header("Level 2 Handler")]
        public GameObject level2AI;
        public Transform aIStartPos;
        public Transform aIEndPos;

        [Header("Level 5 handler")]
        public ScientistStateManager scientist;

        LevelState currentLevel;
        public Level1State level1 = new Level1State();
        public Level2State level2 = new Level2State();
        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        void Start()
        {
            currentLevel = level1;
            currentLevel.OnEnterState(this);
        }

        void Update()
        {
            currentLevel.OnUpdateState(this);
        }

        public void SwitchState(LevelState state)
        {
            currentLevel = state;
            state.OnEnterState(this);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            if(checkPoints.Length <= 0 ) { return; }
            foreach(Transform points in checkPoints)
            {
                Gizmos.DrawWireSphere(points.position, 0.5f);
            }
        }
    }
}
