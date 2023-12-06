using AKVA.Animations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.AI
{
    public class BTGoBackToPos : TreeNode
    {

        Transform transform;
        Transform[] wayPoints;
        bool aiMoved;
        ScientistAIAnim anim;
        int targetIndex = 3;
        public BTGoBackToPos(Transform transform, Transform[] wayPoints)
        {
            this.transform = transform;
            this.wayPoints = wayPoints;
        }

        public override NodeState Execute()
        {

            if (wayPoints.Length > 0)
            {
                Debug.Log("Going back to pos");
                Debug.Log(targetIndex);
                // Move towards the waypoints in reverse order
                transform.position = Vector3.MoveTowards(transform.position, wayPoints[targetIndex].position, 3 * Time.deltaTime);
                transform.LookAt(wayPoints[targetIndex]);

                //// Check if reached the first waypoint
                //if (targetIndex == 0 && Vector3.Distance(transform.position, wayPoints[targetIndex].position) <= 0.1f)
                //{
                //    state = NodeState.FAILURE;  // Assuming reaching the first waypoint indicates failure
                //    return state;
                //}

                //// Check if reached the current waypoint, then move to the previous one
                //if (Vector3.Distance(transform.position, wayPoints[targetIndex].position) <= 0.1f)
                //{
                //    targetIndex--;
                //}

                state = NodeState.RUNNING;
                return state;
            }

            state = NodeState.RUNNING;
            return state;
        }

        IEnumerator MoveAI()
        {
            yield return new WaitForSeconds(3);
            if (!aiMoved)
            {
                anim.ChangeAnimState(anim.Robot_Walk);
                aiMoved = true;
            }
            transform.position = Vector3.MoveTowards(transform.position, wayPoints[targetIndex].position, 3 * Time.deltaTime);
            transform.LookAt(wayPoints[targetIndex]);

            if (Vector3.Distance(transform.position, wayPoints[targetIndex].position) <= .1f)
            {
                if (targetIndex <= wayPoints.Length - 1)
                {
                    anim.ChangeAnimState(anim.Robot_Idle);
                    state = NodeState.SUCCESS;
                }
                if (Vector3.Distance(transform.position, wayPoints[targetIndex].position) <= .1f)
                {
                    targetIndex--;
                    aiMoved = false;
                }
            }
        }
    }
}
