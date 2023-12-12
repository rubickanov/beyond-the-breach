using AKVA.Animations;
using AKVA.Assets.Vince.Scripts.Astar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace AKVA.Assets.Vince.Scripts.AI
{
    public class BTGoToPower : TreeNode
    {
        Transform transform;
        Transform[] wayPoints;
        bool aiMoved;
        ScientistAIAnim anim;
        int targetIndex;
        bool idle = true;
        ScientistBT sciBT;
        public BTGoToPower(Transform transform, Transform[] waypoints)
        {
            this.transform = transform;
            this.wayPoints = waypoints;
            anim = transform.GetComponent<ScientistAIAnim>();
            sciBT = transform.GetComponent<ScientistBT>();
        }
        public override NodeState Execute()
        {
            if (wayPoints.Length > 0)
            {
                transform.GetComponent<ScientistBT>().StartCoroutine(MoveAI());
                return state;
            }
            state = NodeState.FAILURE;
            return state;
        }

        IEnumerator MoveAI()
        {
            yield return new WaitForSeconds(4);
            if (!aiMoved)
            {
                anim.ChangeAnimState(anim.Robot_Walk);
                idle = false;
                aiMoved = true;
            }

            if (!sciBT.playerDied.value)
            {
                float yPos = transform.position.y;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(wayPoints[targetIndex].position.x, yPos, wayPoints[targetIndex].position.z), 3 * Time.deltaTime);

            }
            if (Vector3.Distance(transform.position, wayPoints[targetIndex].position) > .3f)
            {
                Vector3 directionToTarget = (wayPoints[targetIndex].position - transform.position).normalized;

                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

                targetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
                transform.rotation = targetRotation;
            }

            else if (targetIndex >= wayPoints.Length - 1 && Vector3.Distance(transform.position, wayPoints[targetIndex].position) <= .3f)
            {
                anim.ChangeAnimState("LookingAround");
                state = NodeState.SUCCESS;
            }
            else if (Vector3.Distance(transform.position, wayPoints[targetIndex].position) <= .3f)
            {
                targetIndex++;
                aiMoved = false;
                anim.ChangeAnimState(anim.Robot_Idle);
            }
        }
    }
}
