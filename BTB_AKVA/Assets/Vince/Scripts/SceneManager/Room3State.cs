using AKVA.Assets.Vince.Scripts.AI;
using AKVA.Assets.Vince.Scripts.SceneManager;
using AKVA.Player;
using PlasticGui.WorkspaceWindow.BranchExplorer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using System.Threading;
using UnityEngine;


namespace AKVA.Assets.Vince.Scripts.SceneManager
{
    public class Room3State : SceneState
    {
        bool playerInPosition;
        bool aiActive; //Initiates the AI task
        bool[] task; //Initiate Each AI to be activated
        bool aiEnabled;
        int index;


        public override void OnEnterState(SceneStateManager state)
        {
            index = 2;
            state.playerPicking.enabled = false;
            task = new bool[8];
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
                PlayerInput.Instance.DisablePlayerMovement();
                state.tvTurnedOn.value = true;
                state.imagesAppeared[0].value = true;
                state.StartCoroutine(StartAITask(state, 0, 4.5f, false));
                playerInPosition = true;
            }
        }

        void AITask(SceneStateManager state)
        {
            if (aiActive)
            {
                if (Vector3.Distance(state.listOfAI[0].transform.position, state.aiDestination[0].position) < 2f && !task[0] && !aiEnabled)
                {
                    state.imagesAppeared[5].value = true;
                    state.StartCoroutine(showImageDelay(state, 1, 2f)); // show pass key
                    state.StartCoroutine(StartAITask(state, 0, 7f, true)); // go to the position of the  pass key
                    state.StartCoroutine(showImageDelay(state, 5, 10f)); // correct
                    aiEnabled = true;
                }

                else if (task[0] && !task[1])
                {
                    task[1] = true;
                    state.StartCoroutine(StartAITask(state, 0, 10f, true)); // go back to pos
                }
                else if (task[1] && !task[2] && Vector3.Distance(state.listOfAI[0].transform.position, state.listOfAI[0].GetComponent<AIStateManager>().pathPoints[12].position) < 1f)
                {
                    task[2] = true;
                    state.StartCoroutine(showImageDelay(state, 2, 1f)); // show switch
                    state.StartCoroutine(StartAITask(state, 1, 4.5f, true)); // go to the switch pos
                }
                else if (task[2] && !task[3])
                {
                    task[3] = true;
                    state.StartCoroutine(showImageDelay(state, 5, 7f)); // correct
                    state.StartCoroutine(showImageDelay(state, 3, 10f)); // show image of button
                    state.StartCoroutine(StartAITask(state, 1, 14f, true)); // go to button
                }
                else if (task[3] && !task[4] && Vector3.Distance(state.listOfAI[1].transform.position, state.listOfAI[1].GetComponent<AIStateManager>().pathPoints[11].position) < 1f)
                {
                    task[4] = true;
                    state.imagesAppeared[5].value = true; // correct
                    state.StartCoroutine(StartAITask(state, 1, 4f, true)); // go back to pos
                }
                else if (task[4] && !task[5] && Vector3.Distance(state.listOfAI[1].transform.position, state.listOfAI[1].GetComponent<AIStateManager>().pathPoints[12].position) < 1f)
                {
                    task[5] = true;
                    state.StartCoroutine(showImageDelay(state, 1, 1f)); // show image of pass key
                    state.StartCoroutine(StartAITask(state, 2, 5f, true));
                    state.StartCoroutine(showImageDelay(state, 5, 7f)); // correct
                }
                else if (task[5] && !task[6])
                {
                    task[6] = true;
                    state.StartCoroutine(showImageDelay(state, 2, 10f)); // show image of switch
                    state.StartCoroutine(StartAITask(state, 2, 13f, true)); // go to switch pos
                }else if (task[6] && !task[7] && Vector3.Distance(state.listOfAI[2].transform.position, state.listOfAI[2].GetComponent<AIStateManager>().pathPoints[11].position) < 1f)
                {
                    task[7] = true;
                    state.StartCoroutine(Electricute(state, 5));
                }
            }
        }

        IEnumerator showImageDelay(SceneStateManager state,int imgIndex, float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
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
            state.imagesAppeared[4].value = true;
            while (index >= 0/*state.listOfAI.Length*/)
            {
                yield return new WaitForSeconds(delayTime);
                GameObject ai = state.listOfAI[index];
                ai.GetComponent<Animator>().applyRootMotion = true;
                ai.GetComponent<CapsuleCollider>().enabled = false;
                ai.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                ai.GetComponent<AIStateManager>().SwitchState(ai.GetComponent<AIStateManager>().deathState);
                index--;
            }
        }
    }
}
