using AKVA.Animations;
using AKVA.Assets.Vince.Scripts.Astar;
using AKVA.Vince.SO;
using Codice.Client.Common.TreeGrouper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.AI
{
    public class BTPatrol : TreeNode
    {
        Transform currentTransform;
        Transform[] wayPoints;
        MoveAI moveAI;
        int targetIndex = 0;
        bool aiMoved;
        ScientistAIAnim anim;
        BoolReference objToGuard;
        ScientistBT sciBT;
        bool patrol;
        public BTPatrol(Transform currentTransform, Transform[] wayPoints, BoolReference objToGuard, bool patrol)
        {
            anim = currentTransform.GetComponent<ScientistAIAnim>();
            moveAI = currentTransform.GetComponent<MoveAI>();
            this.currentTransform = currentTransform;
            this.wayPoints = wayPoints;
            this.objToGuard = objToGuard;
            this.patrol = patrol;
            sciBT = currentTransform.GetComponent<ScientistBT>();
        }
        public override NodeState Execute()
        {
            if (objToGuard != null)
            {
                state = NodeState.FAILURE;
                return state;
            }

            if (wayPoints.Length > 0)
            {
                if (patrol && Vector3.Distance(currentTransform.position, wayPoints[targetIndex].position) <= 3f)
                {
                    anim.ChangeAnimState(anim.Robot_Idle);
                }

                //Looking at the vending machine
                if(patrol && Vector3.Distance(currentTransform.position, wayPoints[0].position) <= 2.5f){
                    currentTransform.rotation = Quaternion.Euler(0f,90f,0f);
                }else if(patrol && Vector3.Distance(currentTransform.position, wayPoints[1].position) <= 2.5f)
                {
                    currentTransform.rotation = Quaternion.Euler(0f, 90f, 0f);
                    currentTransform.GetComponent<ScientistBT>().StartCoroutine(RotateDelay(180f));
                }

                if (patrol && !aiMoved)
                {
                    sciBT.angleToDetect = 40f;
                    currentTransform.GetComponent<ScientistBT>().StartCoroutine(WalkDelay(10));
                    aiMoved = true;
                }

                else if (!aiMoved)
                {
                    anim.ChangeAnimState(anim.Robot_Walk);
                    moveAI.FindPath(wayPoints[targetIndex]);
                    aiMoved = true;
                }
                if (targetIndex == wayPoints.Length - 1)
                {
                    currentTransform.position = wayPoints[targetIndex].position;
                    targetIndex = 0;
                    aiMoved = false;
                }
                else if (Vector3.Distance(currentTransform.position, wayPoints[targetIndex].position) <= 2.5f)
                {
                    targetIndex++;
                    aiMoved = false;
                }
                state = NodeState.RUNNING;
                return state;
            }
            state = NodeState.FAILURE;
            return state;
        }

        IEnumerator WalkDelay(float timeDelay)
        {
            yield return new WaitForSeconds(timeDelay);
            sciBT.angleToDetect = sciBT.initAngleToDetect;
            anim.ChangeAnimState(anim.Robot_Walk);
            moveAI.FindPath(wayPoints[targetIndex]);
        }

        IEnumerator RotateDelay(float yRotation)
        {
            yield return new WaitForSeconds(7);
            currentTransform.rotation = Quaternion.Euler(0f, yRotation, 0f);
        }
    }
}

