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
                transform.position = Vector3.MoveTowards(transform.position, target.position, 0.5f * Time.deltaTime);
                Vector3 directionToTarget = (new Vector3(target.position.x, transform.position.y, target.position.z) - transform.position).normalized;

                transform.rotation = Quaternion.LookRotation(directionToTarget);
                state = NodeState.RUNNING;
                return state;
            }
            return state;
        }
    }
}
