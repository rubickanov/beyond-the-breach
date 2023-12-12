using AKVA.Animations;
using AKVA.Assets.Vince.Scripts.Astar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.AI
{
    public class MoveTowardsInteractableObj : TreeNode
    {
        private Transform transform;
        ScientistAIAnim anim;
        bool interacting;
        float interactionTime = 5f;
        float currentTime;
        public MoveTowardsInteractableObj(Transform transform)
        {
            this.transform = transform;
            anim = transform.GetComponent<ScientistAIAnim>();
        }
        public override NodeState Execute()
        {
            Transform target = (Transform)GetData("target");
            if(target == null)
            {
                state = NodeState.FAILURE;
                 return state;
            }
            if (Vector3.Distance(transform.position, target.position) > 2f)
            {
                Debug.Log("Dstiance interact : " + Vector3.Distance(transform.position, target.position));
                transform.position = Vector3.MoveTowards(transform.position, target.position, 0.5f * Time.deltaTime);

                Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
                Vector3 directionToTarget = (targetPosition - transform.position).normalized;

                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

                targetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);

                transform.rotation = targetRotation;

                state = NodeState.RUNNING;
                return state;
            }
            return state;
        }
    }
}
