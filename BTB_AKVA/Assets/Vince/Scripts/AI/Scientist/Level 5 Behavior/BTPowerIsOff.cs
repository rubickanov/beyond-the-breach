using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AKVA.Assets.Vince.Scripts.Environment;
using AKVA.Vince.SO;
using AKVA.Animations;
namespace AKVA.Assets.Vince.Scripts.AI
{
    public class BTPowerIsOff : TreeNode
    {
        Transform transform;
        BoolReference objStatus;
        ScientistAIAnim anim;
        public BTPowerIsOff(Transform transform, BoolReference objStatus)
        {
            this.transform = transform;
            this.objStatus = objStatus;
            anim = transform.GetComponent<ScientistAIAnim>();
        }

        public override NodeState Execute()
        {
            if(objStatus == null)
            {
                state = NodeState.FAILURE;
                return state;
            }
            if (!objStatus.value)
            {
                state = NodeState.SUCCESS;
                return state;
            }
          
            return NodeState.FAILURE;
        }
    }
}
