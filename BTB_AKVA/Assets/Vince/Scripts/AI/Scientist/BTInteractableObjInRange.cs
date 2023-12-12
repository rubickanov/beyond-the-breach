using AKVA.Assets.Vince.Scripts.Environment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.AI
{
    public class BTInteractableObjInRange : TreeNode
    {
        private Transform currentTransform;
        private Transform visionPos;
        private float visionRadius;
        private LayerMask interactableLayer;
        public BTInteractableObjInRange(Transform currentTransform, Transform visionPos, float visionRadius, LayerMask interactableLayer)
        {
            this.currentTransform = currentTransform;
            this.visionPos = visionPos;
            this.visionRadius = visionRadius;
            this.interactableLayer = interactableLayer;
        }
        public override NodeState Execute()
        {
            //object target = GetData("target");

            Collider[] colliders = Physics.OverlapSphere(visionPos.position, visionRadius, interactableLayer);
            if (colliders.Length > 0)
            {
                Debug.Log("Interactable Detected");
                //parent.parent.SetData("target", colliders[0].transform); //storing data in the root
                state = NodeState.SUCCESS;
                return state;
            }
            state = NodeState.FAILURE;
            return state;
        }
    }
}
