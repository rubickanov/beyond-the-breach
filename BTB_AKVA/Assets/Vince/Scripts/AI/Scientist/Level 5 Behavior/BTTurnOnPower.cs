using AKVA.Vince.SO;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;


namespace AKVA.Assets.Vince.Scripts.AI
{
    public class BTTurnOnPower : TreeNode
    {
        Transform transform;
        BoolReference objSwitch;
        float timeToTurnOn = 3;
        NodeState currentState;
        bool success;

        float maxTime = 6f;
        float currentTime;

        public BTTurnOnPower(Transform transform, BoolReference objSwitch)
        {
            //this.objSwitch = objSwitch;
            this.transform = transform;
            this.objSwitch = transform.GetComponent<ScientistBT>().objStatus;
        }

        public override NodeState Execute()
        {
           // transform.GetComponent<ScientistBT>().StartCoroutine(TurnOnSwitch());
            //transform.GetComponent<ScientistBT>().objStatus.value = true;

            //Debug.Log(objSwitch.value);
            //if (transform.GetComponent<ScientistBT>().objStatus.value)
            //{
            //    Debug.Log("Power ON");
            //    currentState = NodeState.SUCCESS;
            //}
            //else
            //{
            //    currentState = NodeState.FAILURE;
            //}
            //return currentState;

            if(currentTime < maxTime)
            {
                currentTime += Time.deltaTime;
            }
            else
            {
                transform.GetComponent<ScientistBT>().objStatus.value = true;
                Debug.Log(transform.GetComponent<ScientistBT>().objStatus.value);
                Debug.Log("Power ON");
                state = NodeState.SUCCESS;
                return state;
            }
            state = NodeState.FAILURE;
            return state;
        }
    }
}
