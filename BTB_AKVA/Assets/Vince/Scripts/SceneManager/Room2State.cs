using AKVA.Assets.Vince.Scripts.AI;
using AKVA.Assets.Vince.Scripts.Environment;
using AKVA.Player;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AKVA.Assets.Vince.Scripts.SceneManager
{
    public class Room2State : SceneState
    {
        TextMeshProUGUI systemTxt;
        TextMeshProUGUI movementUI;
        bool txtAnim = true;
        int dotNum;
        bool txtAnimated;
        SceneStateManager state;

        bool playerInPosition;
        bool aiActive; //Initiates the AI task
        bool enableAI; //Initiate Each AI to be activated
        bool[] taskDone;
        bool rotated;
        bool[] positioned;
        private bool RobotsInPosition = false;

        public override void OnEnterState(SceneStateManager state)
        {
            this.state = state;
            state.playerPicking.enabled = false;
            movementUI = state.initializeTxt;
            systemTxt = state.movementTestTxt;
            taskDone = new bool[6];
            positioned = new bool[4];
            state.aiPos = state.listOfAI[2].GetComponent<AIStateManager>().pathPoints[6].transform;
        }

        public override void OnExitState(SceneStateManager state)
        {

        }

        public override void OnUpdateState(SceneStateManager state)
        {
            SetAIPositions(state);
            CheckIfPlayerIsInThePlaceHolder(state);
            ActivateAI(state);
        }

        private void SetAIPositions(SceneStateManager state)
        {
            if (!RobotsInPosition)
            {
                if (Vector3.Distance(state.listOfAI[2].transform.position, state.listOfAI[0].GetComponent<AIStateManager>().pathPoints[4].position) < 1.5f)
                {
                    for (int i = 0; i < state.listOfAI.Length; i++)
                    {
                        AIStateManager aiManager = state.listOfAI[i].GetComponent<AIStateManager>();
                        aiManager.activateAI = true;
                    }
                    RobotsInPosition = true;
                }

            }
        }

        private void CheckIfPlayerIsInThePlaceHolder(SceneStateManager state)
        {
            if (Vector3.Distance(state.playerTransform.position, state.room2PlayerPos.position) < 1.5f && !playerInPosition && RobotsInPosition)
            {
                if (!rotated)
                {
                    rotated = true;
                    foreach (var ai in state.listOfAI)
                    {
                        ai.transform.rotation = Quaternion.identity;
                    }
                }

                //HUD
                if (!txtAnimated)
                {
                    systemTxt.color = state.hudColor;
                    txtAnimated = true;
                    state.StartCoroutine(AnimTxt());
                }
                SetMovementUI(false);
                PlayerInput.Instance.DisablePlayerMovement();
                state.StartCoroutine(StartAITask(state, 0));
                playerInPosition = true;
            }
        }

        private void ActivateAI(SceneStateManager state)
        {
            if (aiActive)
            {
                if (GetNumberOfActiveSockets(state) == 1 && !taskDone[0] && !enableAI)
                {
                    state.StartCoroutine(StartAITask(state, 1));
                    enableAI = true;
                    taskDone[0] = true;
                }
                else if (GetNumberOfActiveSockets(state) == 2 && !taskDone[1] && !enableAI)
                {
                    state.StartCoroutine(StartAITask(state, 2));
                    enableAI = true;
                    taskDone[1] = true;
                }
                else if (GetNumberOfActiveSockets(state) == 3 && !taskDone[2] && !enableAI && Vector3.Distance(state.listOfAI[2].transform.localPosition, state.aiPos.localPosition) <= 2f)
                {
                    state.playerPicking.enabled = true;
                    SetMovementUI(true);
                    state.OnMovementEnabled.Invoke();
                    PlayerInput.Instance.EnablePlayerInput();
                    taskDone[2] = true;
                }
                else if (GetNumberOfActiveSockets(state) == 4 && !taskDone[3] && Vector3.Distance(state.playerTransform.position, state.room2PlayerPos.position) < 1.5f)
                {
                    txtAnim = false;
                    state.OnSuccess.Invoke();
                    SetMovementUI(false);
                    PlayerInput.Instance.DisablePlayerMovement();
                    state.StartCoroutine(LineUP(state));
                    taskDone[3] = true;
                }
                else if (taskDone[3] && !taskDone[4] && Vector3.Distance(state.playerTransform.position, state.room2PlayerPos2.position) < 1f)
                {
                    taskDone[4] = true;
                    state.room2Door.EnableDoor = true;
                    for (int i = 0; i < state.listOfAI.Length; i++)
                    {
                        AIStateManager ai = state.listOfAI[i].GetComponent<AIStateManager>();
                        ai.targetIndex++;
                        ai.moveOnly = true;
                        ai.currentTarget = ai.pathPoints[ai.targetIndex];
                        state.StartCoroutine(ProceedToNextRoom(state, ai));
                    }
                    SetMovementUI(true);
                    state.OnMovementEnabled.Invoke();
                    PlayerInput.Instance.EnablePlayerInput();
                }

            }
        }

        IEnumerator LineUP(SceneStateManager state, float lineUpDelay = 3f)
        {
            for (int i = 0; i < state.listOfAI.Length; i++)
            {
                AIStateManager ai = state.listOfAI[i].GetComponent<AIStateManager>();
                ai.targetIndex++;
                ai.moveOnly = true;
                ai.currentTarget = ai.pathPoints[ai.targetIndex];
                ai.SwitchState(ai.moveState);
                yield return new WaitForSeconds(lineUpDelay);
            }
            SetMovementUI(true);
            PlayerInput.Instance.EnablePlayerInput();
        }

        IEnumerator StartAITask(SceneStateManager state, int aiIndex) //Starting AI to do its task
        {
            yield return new WaitForSeconds(3f);
            state.listOfAI[aiIndex].GetComponent<AIStateManager>().moveOnly = false;
            state.listOfAI[aiIndex].GetComponent<AIStateManager>().activateAI = true;
            aiActive = true;
            enableAI = false;
        }

        public int GetNumberOfActiveSockets(SceneStateManager state)
        {
            int activeSockets = 0;

            foreach (var btn in state.room2Buttons.Items)
            {
                if (btn.GetComponent<BatterySocket>().socketIsActive)
                {
                    activeSockets++;
                }
            }
            return activeSockets;
        }

        IEnumerator ProceedToNextRoom(SceneStateManager state, AIStateManager ai)
        {
            yield return new WaitForSeconds(0);
            ai.SwitchState(ai.moveState);
            state.SwitchState(state.room3State);
        }
        
        IEnumerator AnimTxt()
        {
            while (txtAnim)
            {
                yield return new WaitForSeconds(.5f);

                if (dotNum < 4)
                {
                    state.OnLoad.Invoke();
                    systemTxt.SetText("COLOR RECOGNITION SYSTEM" + new string('.', dotNum));
                }
                else
                {
                    systemTxt.SetText("COLOR RECOGNITION SYSTEM");
                    dotNum = 0;
                }
                dotNum++;
            }
            systemTxt.color = Color.green;
            systemTxt.SetText("COLOR RECOGNITION SUCCESS");
        }

        public void SetMovementUI(bool value)
        {
            if (value)
            {
                movementUI.color = Color.green;
                movementUI.SetText("MOVEMENT: ENABLED");
            }
            else
            {
                movementUI.color = Color.red;
                movementUI.SetText("MOVEMENT: DISABLED");
            }
        }

        void DisableAIMoveOnly(SceneStateManager state)
        {
            foreach (GameObject ai in state.listOfAI)
            {
                if (ai.GetComponent<AIStateManager>().moveOnly)
                {
                    ai.GetComponent<AIStateManager>().moveOnly = false;
                }
            }
        }
    }
}