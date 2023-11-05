using AKVA.Assets.Vince.Scripts.AI;
using AKVA.Assets.Vince.Scripts.SceneManager;
using AKVA.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;


namespace AKVA.Assets.Vince.Scripts.SceneManager
{
    public class Room3State : SceneState
    {
        bool playerInPosition;
        bool aiActive; //Initiates the AI task
        bool enableAI; //Initiate Each AI to be activated

        public override void OnEnterState(SceneStateManager state)
        {
            Debug.Log("Room State 3!");
            state.playerPicking.enabled = false;
        }

        public override void OnExitState(SceneStateManager state)
        {
        }

        public override void OnUpdateState(SceneStateManager state)
        {
            CheckIfPlayerIsInThePlaceHolder(state);
            AITask(state);
        }

        private void CheckIfPlayerIsInThePlaceHolder(SceneStateManager state)
        {
            if (Vector3.Distance(state.playerTransform.position, state.room3PlayerPos.position) < 1.5f && !playerInPosition) // if player has positioned to its place holder
            {
                Debug.Log("Player is In position");
                PlayerInput.Instance.DisablePlayerMovement(true);
                state.tvTurnedOn.value = true;
                state.StartCoroutine(StartAITask(state, 0, 4.5f));
                playerInPosition = true;
            }
        }

        void AITask(SceneStateManager state)
        {
            if (aiActive && !enableAI)
            {
                Debug.Log(Vector3.Distance(state.listOfAI.items[0].transform.position, state.aiDestination[0].position));
                if (Vector3.Distance(state.listOfAI.items[0].transform.position, state.aiDestination[0].position) < 2.5f)
                {
                    state.imagesAppeared[1].value = true;
                    state.StartCoroutine(StartAITask(state, 0, 4.5f));
                    enableAI = true;
                }
            }
        }

        IEnumerator StartAITask(SceneStateManager state, int aiIndex, float delayTime) //Starting AI to do its task
        {
            yield return new WaitForSeconds(delayTime);
            state.listOfAI.items[aiIndex].GetComponent<AIStateManager>().activateAI = true;
            aiActive = true;
        }
    }
}
