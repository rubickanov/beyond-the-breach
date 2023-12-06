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
        bool patrol;
        public BTPatrol(Transform currentTransform, Transform[] wayPoints, BoolReference objToGuard, bool patrol)
        {
            anim = currentTransform.GetComponent<ScientistAIAnim>();
            moveAI = currentTransform.GetComponent<MoveAI>();
            this.currentTransform = currentTransform;
            this.wayPoints = wayPoints;
            this.objToGuard = objToGuard;
            this.patrol = patrol;
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

                if (patrol && Vector3.Distance(currentTransform.position, wayPoints[targetIndex].position) <= 2.5f)
                {
                    anim.ChangeAnimState(anim.Robot_Idle);
                }

                if (patrol && !aiMoved)
                {
                    currentTransform.GetComponent<ScientistBT>().StartCoroutine(WalkDelay(3));
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
            anim.ChangeAnimState(anim.Robot_Walk);
            moveAI.FindPath(wayPoints[targetIndex]);
        }
    }
}

