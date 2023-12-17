using System.Collections;
using UnityEngine;
using AKVA.Player;
using AKVA.Assets.Vince.Scripts.AI;
using AKVA.Assets.Vince.Scripts.Environment;
using TMPro;

namespace AKVA.Assets.Vince.Scripts.SceneManager
{
    public class Room1State : SceneState
    {
        TextMeshProUGUI systemTxt;
        TextMeshProUGUI movementUI;
        Color hudColor;
        bool interactionAnim;
        bool interaction = true;
        int dotNum1;
        int dotNum2;
        bool numberSystem = true;
        bool numberTxtAnimating;
        SceneStateManager state;

        bool[] successSfxPlayed;

        bool playerInPosition;
        bool aiActive; //Initiates the AI task
        bool enableAI; //Initiate Each AI to be activated
        bool[] taskDone;
        public override void OnEnterState(SceneStateManager state)
        {
            this.state = state;
            movementUI = state.initializeTxt;
            systemTxt = state.movementTestTxt;
            hudColor = state.hudColor;
            successSfxPlayed = new bool[3];
            taskDone = new bool[6];
            systemTxt.color = Color.green;

            if (!numberTxtAnimating)
            {
                state.StartCoroutine(NumberAnimTxt(state));
                numberTxtAnimating = true;
            }
        }

        public override void OnUpdateState(SceneStateManager state)
        {
            CheckIfPlayerIsInThePlaceHolder(state);
            ActivateAI(state);
        }

        public override void OnExitState(SceneStateManager state)
        {
        }

        private void CheckIfPlayerIsInThePlaceHolder(SceneStateManager state)
        {
            if (Vector3.Distance(state.playerTransform.position, state.room1PlayerPos.position) < 1.5f && !playerInPosition) // if player has positioned to its place holder
            {
                state.room1TutorialMonitor.turnOnTV = true;
                PlayerInput.Instance.DisablePlayerMovement();
                state.playerTransform.GetComponent<Rigidbody>().velocity = Vector3.zero;
                state.playerTransform.position =
                    Vector3.Lerp(state.playerTransform.position, state.room1PlayerPos.position, 1);
                SetMovementUI(false);
                state.StartCoroutine(StartAITask(state, 0));
                numberSystem = false;
                if (!interactionAnim)
                {
                    state.OnSuccess.Invoke();
                    systemTxt.color = Color.green;
                    state.StartCoroutine(InteractionAnimTxt(state));
                    interactionAnim = true;
                }
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
                else if (GetNumberOfActiveSockets(state) == 3 && !taskDone[2] && !enableAI)
                {
                    PlayerInput.Instance.EnablePlayerInput();
                    state.OnMovementEnabled.Invoke();
                    state.playerPicking.enabled = true;
                    taskDone[2] = true;
                    SetMovementUI(true);
                }
                else if (GetNumberOfActiveSockets(state) == 4 && Vector3.Distance(state.playerTransform.position, state.room1PlayerPos.position) < 1.5f && !taskDone[4])
                {
                    SetMovementUI(false);
                    PlayerInput.Instance.DisablePlayerMovement();
                    state.OnSuccess.Invoke();
                    interaction = false;
                    state.StartCoroutine(LineUP(state));
                    taskDone[4] = true;
                }
                else if (taskDone[4] && !taskDone[5] && Vector3.Distance(state.playerTransform.position, state.room1PlayerPos2.position) < 1f)
                {
                    state.room1TutorialMonitor.ProceedToNextRoomText();
                    state.room1Door.EnableDoor = true;
                    for (int i = 0; i < state.listOfAI.Length; i++)
                    {
                        AIStateManager ai = state.listOfAI[i].GetComponent<AIStateManager>();
                        ai.targetIndex++;
                        ai.moveOnly = true;
                        ai.currentTarget = ai.pathPoints[ai.targetIndex];
                        state.StartCoroutine(ProceedToNextRoom(state, ai));
                    }
                    taskDone[5] = true;
                }
            }
        }

        IEnumerator LineUP(SceneStateManager state, float lineUpDelay = 3f)
        {
            state.room1TutorialMonitor.LineUPTxt();
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
            state.listOfAI[aiIndex].GetComponent<AIStateManager>().activateAI = true;
            aiActive = true;
            enableAI = false;
        }

        public int GetNumberOfActiveSockets(SceneStateManager state)
        {
            int activeSockets = 0;

            foreach (var socket in state.room1Buttons.Items)
            {
                if (socket.GetComponent<BatterySocket>().socketIsActive)
                {
                    activeSockets++;
                }
            }
            return activeSockets;
        }

        IEnumerator InteractionAnimTxt(SceneStateManager state)
        {
            yield return new WaitForSeconds(3);
            systemTxt.color = hudColor;

            while (interaction)
            {
                yield return new WaitForSeconds(.5f);

                if (dotNum2 < 4)
                {
                    state.OnLoad.Invoke();
                    systemTxt.SetText("CHECKING INTERACTION SYSTEM" + new string('.', dotNum2));
                }
                else
                {
                    systemTxt.SetText("CHECKING INTERACTION SYSTEM");
                    dotNum2 = 0;
                }
                dotNum2++;
            }
            systemTxt.color = Color.green;
            systemTxt.SetText("INTERACTION SYSTEM SUCCESS");
        }

        IEnumerator NumberAnimTxt(SceneStateManager state)
        {
            yield return new WaitForSeconds(2f);
            systemTxt.color = hudColor;
            while (numberSystem)
            {
                yield return new WaitForSeconds(.5f);

                if (dotNum1 < 4)
                {
                    state.OnLoad.Invoke();
                    systemTxt.SetText("NUMBER RECOGNITION SYSTEM" + new string('.', dotNum1));
                }
                else
                {
                    systemTxt.SetText("NUMBER RECOGNITION SYSTEM");
                    dotNum1 = 0;
                }
                dotNum1++;
            }
            systemTxt.SetText("NUMBER RECOGNITION SUCCESS");
        }

        public void SetMovementUI(bool value)
        {
            if (value)
            {
                state.OnMovementEnabled.Invoke();
                movementUI.color = Color.green;
                movementUI.SetText("MOVEMENT: ENABLED");
            }
            else
            {
                movementUI.color = Color.red;
                movementUI.SetText("MOVEMENT: DISABLED");
            }
        }

        IEnumerator ProceedToNextRoom(SceneStateManager state, AIStateManager ai)
        {
            yield return new WaitForSeconds(0f);
            ai.SwitchState(ai.moveState);
            state.SwitchState(state.room2State);
        }

        void SetSuccessSFX(int successIndex)
        {
            if (!successSfxPlayed[successIndex])
            {
                successSfxPlayed[successIndex] = true;
                state.OnSuccess.Invoke();
            }
        }
    }
}