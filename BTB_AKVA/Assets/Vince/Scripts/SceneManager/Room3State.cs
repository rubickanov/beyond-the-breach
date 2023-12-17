using AKVA.Assets.Vince.Scripts.AI;
using AKVA.Assets.Vince.Scripts.Environment;
using AKVA.Player;
using EZCameraShake;
using PlasticGui.WorkspaceWindow.BranchExplorer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using System.Threading;
using TMPro;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace AKVA.Assets.Vince.Scripts.SceneManager
{
    public class Room3State : SceneState
    {
        Material roomMat;
        Texture2D redTex;

        bool robotsInPosition;
        bool playerInPosition;
        bool aiActive; //Initiates the AI task
        bool[] task; //Initiate Each AI to be activated
        bool aiEnabled;
        int index;

        TextMeshProUGUI systemTxt;
        TextMeshProUGUI movementUI;
        bool txtAnim = true;
        int dotNum;
        bool txtAnimating;
        bool errorAnimated;
        bool errorAnimating = true;

        GameObject electricVFX;
        bool enableCameraShake;

        SceneStateManager state;
        public override void OnEnterState(SceneStateManager state)
        {
            this.state = state;

            roomMat = state.room3Renderer.material;
            redTex = state.redTexture;

            systemTxt = state.movementTestTxt;
            movementUI = state.initializeTxt;
            index = 2;
            state.playerPicking.enabled = false;
            task = new bool[8];
            electricVFX = state.electricVFX;
            state.tvTurnedOn.value = true;
        }

        public override void OnExitState(SceneStateManager state)
        {
        }

        public override void OnUpdateState(SceneStateManager state)
        {
            SetAIPositions(state);
            CheckIfPlayerIsInThePlaceHolder(state);
            AITask(state);
            CameraShake(state);
        }
        private void SetAIPositions(SceneStateManager state)
        {
            if (!robotsInPosition)
            {
                if (Vector3.Distance(state.listOfAI[2].transform.position, state.listOfAI[0].GetComponent<AIStateManager>().pathPoints[10].position) <= 2f)
                {
                    for (int i = 0; i < state.listOfAI.Length; i++)
                    {
                        AIStateManager aiManager = state.listOfAI[i].GetComponent<AIStateManager>();
                        aiManager.activateAI = true;
                    }
                    robotsInPosition = true;
                }
            }
        }

        private void CheckIfPlayerIsInThePlaceHolder(SceneStateManager state)
        {
            if (Vector3.Distance(state.playerTransform.position, state.room3PlayerPos.position) < 1.5f && !playerInPosition && robotsInPosition) // if player has positioned to its place holder
            {
                for (int i = 0; i < state.room3TutorialMonitor.Length; i++)
                {
                    TutorialMonitor monitor = state.room3TutorialMonitor[i];
                    monitor.turnOnTV = true;
                    monitor.SetKeyLettersAndInstruction("[E]", "TO INTERACT", 50);
                }
                SetMovementUI(false);
                PlayerInput.Instance.DisablePlayerMovement();

                SetScreenImages(state.neuroLabSprite, false);
                SetScreenImages(state.interactableSprites, true);

                state.imagesAppeared[0].value = true;
                state.StartCoroutine(StartAITask(state, 0, 4.5f, false));

                if (!txtAnimating)
                {
                    systemTxt.color = state.hudColor;
                    state.StartCoroutine(AnimTxt(state));
                    txtAnimating = true;
                }

                playerInPosition = true;
            }
        }

        void AITask(SceneStateManager state)
        {
            if (aiActive)
            {
                if (Vector3.Distance(state.listOfAI[0].transform.position, state.listOfAI[0].GetComponent<AIStateManager>().pathPoints[12].position) < 2f && !task[0] && !aiEnabled)
                {
                    state.imagesAppeared[5].value = true;

                    state.StartCoroutine(showImageDelay(state, 1, 2f, "[LEFT-CLICK]", "TO INTERACT PASSWORD BUTTONS", 36)); // show pass key
                    state.StartCoroutine(StartAITask(state, 0, 7f, true)); // go to the position of the  pass key
                    state.StartCoroutine(showImageDelay(state, 5, 10f)); // correct
                    aiEnabled = true;
                }

                else if (task[0] && !task[1])
                {
                    task[1] = true;
                    state.StartCoroutine(StartAITask(state, 0, 10f, true)); // go back to pos
                }
                else if (task[1] && !task[2] && Vector3.Distance(state.listOfAI[0].transform.position, state.listOfAI[0].GetComponent<AIStateManager>().pathPoints[14].position) < 1f)
                {
                    task[2] = true;
                    state.StartCoroutine(showImageDelay(state, 2, 1f, "[E]", "TO INTERACT", 50)); // show switch
                    state.StartCoroutine(StartAITask(state, 1, 4.5f, true)); // go to the switch pos
                }
                else if (task[2] && !task[3])
                {
                    task[3] = true;
                    state.StartCoroutine(showImageDelay(state, 5, 7f)); // correct
                    state.StartCoroutine(showImageDelay(state, 3, 10f)); // show image of button
                    state.StartCoroutine(StartAITask(state, 1, 14f, true)); // go to button
                }
                else if (task[3] && !task[4] && Vector3.Distance(state.listOfAI[1].transform.position, state.listOfAI[1].GetComponent<AIStateManager>().pathPoints[13].position) < 1f)
                {
                    task[4] = true;
                    state.OnScreenCorrect.Invoke();
                    state.imagesAppeared[5].value = true; // correct
                    state.StartCoroutine(StartAITask(state, 1, 4f, true)); // go back to pos
                }
                else if (task[4] && !task[5] && Vector3.Distance(state.listOfAI[1].transform.position, state.listOfAI[1].GetComponent<AIStateManager>().pathPoints[14].position) < 1f)
                {
                    task[5] = true;
                    state.StartCoroutine(showImageDelay(state, 1, 1f, "[LEFT-CLICK]", "TO INTERACT PASSWORD BUTTONS", 36)); // show image of pass key
                    state.StartCoroutine(StartAITask(state, 2, 5f, true));
                    state.StartCoroutine(showImageDelay(state, 5, 7f)); // correct
                }
                else if (task[5] && !task[6])
                {
                    task[6] = true;
                    state.StartCoroutine(showImageDelay(state, 2, 10f, "[E]", "TO INTERACT", 50)); // show image of switch
                    state.StartCoroutine(StartAITask(state, 2, 13f, true)); // go to switch pos
                }
                else if (task[6] && !task[7] && Vector3.Distance(state.listOfAI[2].transform.position, state.listOfAI[2].GetComponent<AIStateManager>().pathPoints[13].position) < 1f)
                {
                    task[7] = true;
                    state.StartCoroutine(Electricute(state, 5));
                }
            }
        }
        IEnumerator showImageDelay(SceneStateManager state, int imgIndex, float delayTime)
        {
            yield return new WaitForSeconds(delayTime);

            if (imgIndex == 5)
            {
                state.OnScreenCorrect.Invoke();
            }

            state.imagesAppeared[imgIndex].value = true;
        }

        IEnumerator showImageDelay(SceneStateManager state, int imgIndex, float delayTime, string interactKey, string instruction, float txtSize)
        {
            yield return new WaitForSeconds(delayTime);
            for (int i = 0; i < state.room3TutorialMonitor.Length; i++)
            {
                TutorialMonitor monitor = state.room3TutorialMonitor[i];
                monitor.SetKeyLettersAndInstruction(interactKey, instruction, txtSize);
            }

            state.imagesAppeared[imgIndex].value = true;
        }

        IEnumerator StartAITask(SceneStateManager state, int aiIndex, float delayTime, bool enable) //Starting AI to do its task
        {
            yield return new WaitForSeconds(delayTime);
            state.listOfAI[aiIndex].GetComponent<AIStateManager>().activateAI = true;
            aiActive = true;

            if (enable)// to enable image after showing the pass key
            {
                task[0] = true;
            }
        }

        IEnumerator Electricute(SceneStateManager state, float delayTime)
        {
            yield return new WaitForSeconds(1f);
            state.OnRobotError.Invoke();
            state.imagesAppeared[4].value = true;
            SetRoomToRed(state);
            state.AlarmSound.Invoke();

            yield return new WaitForSeconds(3f);
            SubtitleManager.Instance.PlayPublicAnnoucememnt("Instructor:", "Anomaly detected in NeuroSystem. Error correction initiated. Commencing system diagnostics and force shutdown for resolution.", 10f);
            state.ForceShutdownAudio.Invoke();

            yield return new WaitForSeconds(5f);

            while (index >= 0/*state.listOfAI.Length*/)
            {
                yield return new WaitForSeconds(delayTime);
                GameObject ai = state.listOfAI[index];
                ai.GetComponent<CapsuleCollider>().enabled = false;
                ai.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                ai.GetComponent<AIStateManager>().SwitchState(ai.GetComponent<AIStateManager>().deathState);
                index--;
            }
            txtAnim = false;
        }
        IEnumerator AnimTxt(SceneStateManager state)
        {
            while (txtAnim)
            {
                yield return new WaitForSeconds(.5f);

                if (dotNum < 4)
                {
                    state.OnLoad.Invoke();
                    systemTxt.SetText("IMAGE RECOGNITION SYSTEM" + new string('.', dotNum));
                }
                else
                {
                    systemTxt.SetText("IMAGE RECOGNITION SYSTEM");
                    dotNum = 0;
                }
                dotNum++;
            }
            systemTxt.color = Color.red;
            systemTxt.SetText("ERROR: FORCE SHUTDOWN INITIATED");
            if (!errorAnimated)
            {
                state.cameraShaker.enabled = true;
                enableCameraShake = true;
                electricVFX.SetActive(true);
                movementUI.gameObject.SetActive(false);

                state.StartCoroutine(LoadToDifferentScene(state));
                state.StartCoroutine(AnimateErrorTxt());
                errorAnimated = true;
            }
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

        IEnumerator AnimateErrorTxt()
        {
            while (errorAnimating)
            {
                yield return new WaitForSeconds(.5f);
                systemTxt.gameObject.SetActive(false);
                yield return new WaitForSeconds(.5f);
                state.HUDError.Invoke();
                systemTxt.gameObject.SetActive(true);
            }
        }

        IEnumerator LoadToDifferentScene(SceneStateManager state)
        {
            yield return new WaitForSeconds(6);
            state.PlayerHUDWithoutAnim.SetActive(false);
            state.blackBG.color = new Color(state.blackBG.color.r, state.blackBG.color.g, state.blackBG.color.b, 1);

            yield return new WaitForSeconds(5);
            errorAnimating = false;
            state.movementTestTxt.gameObject.SetActive(false);
            systemTxt.gameObject.SetActive(false);
            movementUI.gameObject.SetActive(false);
            state.sfx.Stop();
            state.bgMusic.Stop();
            state.alarmSound.Stop();
            yield return new WaitForSeconds(state.preMainSceneDelay);
            state.SwitchState(state.preMainSceneState);
        }

        void SetRoomToRed(SceneStateManager state)
        {
            roomMat.color = Color.red;
            roomMat.SetTexture("_EmissionMap", redTex);
            roomMat.SetColor("_EmissionColor", Color.red * 10);

            foreach (Light lights in state.realTimeLights)
            {
                lights.color = Color.red;
                lights.intensity = 10f;
            }
        }

        void CameraShake(SceneStateManager state)
        {
            if (enableCameraShake)
            {
                CameraShaker.Instance.ShakeOnce(state.magnitude, state.roughness, state.fadeInTime, state.fadeOutTime);
            }
        }

        void SetScreenImages(GameObject [] imgs, bool value)
        {
            foreach(GameObject img in imgs)
            {
                img.SetActive(value);
            }
        }


        IEnumerator MoveDown(CapsuleCollider collider)
        {
            yield return new WaitForSeconds(3f);
            while (collider.height > 0.1f)
            {
                yield return null;
                collider.height -= 0.005f * Time.deltaTime;
            }
        }
    }
}
