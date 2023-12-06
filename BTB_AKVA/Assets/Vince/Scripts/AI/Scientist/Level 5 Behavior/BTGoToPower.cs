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
        public BTGoToPower(Transform transform, Transform[] waypoints)
        {
            this.transform = transform;
            this.wayPoints = waypoints;
            anim = transform.GetComponent<ScientistAIAnim>();
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
            transform.position = Vector3.MoveTowards(transform.position, wayPoints[targetIndex].position, 3 * Time.deltaTime);

            Vector3 directionToTarget = (new Vector3(wayPoints[targetIndex].position.x, wayPoints[targetIndex].position.y, wayPoints[targetIndex].position.z) - transform.position).normalized;
            if (directionToTarget != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(directionToTarget);
            }

            if (targetIndex >= wayPoints.Length - 1 && Vector3.Distance(transform.position, wayPoints[targetIndex].position) <= .1f)
            {
                anim.ChangeAnimState(anim.Robot_Interaction);
                state = NodeState.SUCCESS;
            }
            else if (Vector3.Distance(transform.position, wayPoints[targetIndex].position) <= .1f)
            {
                targetIndex++;
                aiMoved = false;
                anim.ChangeAnimState(anim.Robot_Idle);
            }
        }
    }
}
