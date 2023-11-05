using AKVA.Assets.Vince.Scripts.AI;
using AKVA.Assets.Vince.Scripts.Environment;
using AKVA.Player;
using System.Collections;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.SceneManager
{
    public class Room2State : SceneState
    {
        bool playerInPosition;
        bool aiActive; //Initiates the AI task
        bool enableAI; //Initiate Each AI to be activated
        bool[] taskDone;

        public override void OnEnterState(SceneStateManager state)
        {
            Debug.Log("Room 2 State!");
            state.playerPicking.enabled = false;
            taskDone = new bool[5];
            DisableAIMoveOnly(state);
        }

        public override void OnExitState(SceneStateManager state)
        {

        }

        public override void OnUpdateState(SceneStateManager state)
        {
            CheckIfPlayerIsInThePlaceHolder(state);
            ActivateAI(state);
        }

        private void CheckIfPlayerIsInThePlaceHolder(SceneStateManager state)
        {
            if (Vector3.Distance(state.playerTransform.position, state.room2PlayerPos.position) < 1.5f && !playerInPosition) // if player has positioned to its place holder
            {
                Debug.Log("Player is In position");
                PlayerInput.Instance.DisablePlayerMovement(true);
                state.StartCoroutine(StartAITask(state, 0));
                playerInPosition = true;
            }
        }

        private void ActivateAI(SceneStateManager state)
        {
            if (aiActive)
            {
                if (GetNumberOfActiveButtons(state) == 1 && !taskDone[0] && !enableAI)
                {
                    state.StartCoroutine(StartAITask(state, 1));
                    enableAI = true;
                    taskDone[0] = true;
                }
                else if (GetNumberOfActiveButtons(state) == 2 && !taskDone[1] && !enableAI)
                {
                    state.playerPicking.enabled = true;
                    PlayerInput.Instance.DisablePlayerMovement(false);
                    taskDone[1] = true;
                }
                else if (GetNumberOfActiveButtons(state) == 3 && !taskDone[2] && !enableAI)
                {
                    state.StartCoroutine(StartAITask(state, 2));
                    enableAI = true;
                    taskDone[2] = true;
                }
                //if (GetNumberOfActiveButtons(state) == 4 && Vector3.Distance(state.playerTransform.position, state.room2PlayerPos.position) < 1.5f && !taskDone[4])
                //{
                //    state.room1Door.EnableDoor = true;
                //    for (int i = 0; i < state.listOfAI.Count; i++)
                //    {
                //        AIStateManager ai = state.listOfAI.items[i].GetComponent<AIStateManager>();
                //        ai.moveOnly = true;
                //        ai.currentTarget = ai.targets[3];
                //        state.StartCoroutine(ProceedToNextRoom(state, ai));
                //    }
                //    taskDone[4] = true;
                //}
            }
        }

        IEnumerator StartAITask(SceneStateManager state, int aiIndex) //Starting AI to do its task
        {
            yield return new WaitForSeconds(3f);
            state.listOfAI.items[aiIndex].GetComponent<AIStateManager>().activateAI = true;
            aiActive = true;
            enableAI = false;
        }

        public int GetNumberOfActiveButtons(SceneStateManager state)
        {
            int activeBtns = 0;

            foreach (var btn in state.room2Buttons.Items)
            {
                if (btn.GetComponent<BatterySocket>().socketIsActive)
                {
                    activeBtns++;
                }
            }
            return activeBtns;
        }

        IEnumerator ProceedToNextRoom(SceneStateManager state, AIStateManager ai)
        {
            yield return new WaitForSeconds(2f);
            ai.SwitchState(ai.moveState);
            state.SwitchState(state.room2State);
        }

        void DisableAIMoveOnly(SceneStateManager state)
        {
            foreach(GameObject ai in state.listOfAI.Items)
            {
                if (ai.GetComponent<AIStateManager>().moveOnly)
                {
                    ai.GetComponent<AIStateManager>().moveOnly = false;
                }
            }
        }
    }
}