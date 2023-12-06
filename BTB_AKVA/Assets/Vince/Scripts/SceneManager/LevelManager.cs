using System;
using AKVA.Animations;
using AKVA.Assets.Vince.Scripts.AI;
using AKVA.Assets.Vince.Scripts.Environment;
using AKVA.Vince.SO;
using Assets.Vince.Scripts.SceneManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.SceneManager
{
    public class LevelManager : MonoBehaviour
    {
        public GameObject player;
        public Transform[] checkPoints;

        [Header("Level 2 Handler")]
        public GameObject scientistLevel2;

        [Header("Level 5 handler")]
        public GameObject scientistLevel5;
        [Header("Level 5 Extension Level")]
        public BoolReference switchStatus;
        public RobotAIAnim robotAnim;
        bool x;

        public static LevelManager Instance;
        
        LevelState currentLevel;
        public Level1State level1 = new Level1State();
        public Level2State level2 = new Level2State();
        public Level5State level5 = new Level5State();
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
            
            //player = GameObject.FindGameObjectWithTag("Player");
            DisableAllMovingAI();
        }
        void Start()
        {
            currentLevel = level1;
            currentLevel.OnEnterState(this);
        }

        void Update()
        {
            currentLevel.OnUpdateState(this);
            Debug.Log(currentLevel);
        }

        public void SwitchState(LevelState state)
        {
            currentLevel = state;
            state.OnEnterState(this);
        }

        void DisableAllMovingAI()
        {
            scientistLevel2.SetActive(false);
            scientistLevel5.SetActive(false);
        }

        public void SetCurrentLevel(LevelStatesEnum newStateEnum)
        {
            switch (newStateEnum)
            {
                case LevelStatesEnum.Level1:
                    SwitchState(level1);
                    break;
                case LevelStatesEnum.Level2:
                    SwitchState(level2);
                    break;
                case LevelStatesEnum.Level3:
                    //SwitchState(level3);
                    break;
                case LevelStatesEnum.Level4:
                    //SwitchState(level4);
                    break;
                case LevelStatesEnum.Level5:
                    SwitchState(level5);
                    break;
                case LevelStatesEnum.Level6:
                    //SwitchState(level6);
                    break;
                case LevelStatesEnum.Level7:
                    //SwitchState(level7);
                    break;
                default:
                    break;
            }
        }

        private void OnDrawGizmos()
        {
            
            Gizmos.color = Color.blue;
            if (checkPoints.Length <= 0) return;
            foreach(Transform points in checkPoints)
            {
                Gizmos.DrawWireSphere(points.position, 0.5f);
            }
        }

        public enum LevelStatesEnum
        {
            Level1,
            Level2,
            Level3,
            Level4,
            Level5,
            Level6,
            Level7
        }
    }
}
