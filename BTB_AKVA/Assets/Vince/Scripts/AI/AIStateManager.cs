using AKVA.Assets.Vince.Scripts.Astar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.AI
{
    public class AIStateManager : MonoBehaviour
    {
        public bool activateAI;

        [Header("PickUp")]
        public GameObject objOnHand;
        public Transform itemPlaceHolder;
        public LayerMask objectsToPick;
        public float sphereRadius = 1f;

        [Header("PickUp")]
        public LayerMask placesToDrop;

        [Header("Movement")]
        [HideInInspector] public MoveAI pathFind;
        [HideInInspector] public Transform currentTarget;
        public int targetIndex;
        public Transform[] targets;
        public bool moveOnly;
       

        //states
        public AIState currentState;
        public MoveState moveState = new MoveState();
        public PickUpState pickUpState = new PickUpState();
        public DropState dropState = new DropState();
        void Start()
        {
            pathFind = GetComponent<MoveAI>();
        }

        void Update()
        {
            ActivateAI();
            if (currentState != null)
            {
                currentState.OnUpdateState(this);
                transform.rotation = Quaternion.identity;
            }
            HoldObject();
        }

        private void HoldObject()
        {
            if (objOnHand != null)
            {
                objOnHand.transform.position = itemPlaceHolder.position;
                objOnHand.GetComponent<Collider>().isTrigger = true;
            }
        }

        public void ActivateAI()
        {
            if (activateAI)
            {
                if(targetIndex >  0)
                {
                    targetIndex++;
                }
                currentTarget = targets[targetIndex];
                SwitchState(moveState);
                activateAI = false;
            }
        }

        public void SwitchState(AIState state)
        {
            currentState = state;
            state.OnEnterState(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            //currentState.OnCollisionEnter(this,other);
        }

        private void OnDrawGizmos()
        {
            if (itemPlaceHolder != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(itemPlaceHolder.position, sphereRadius);
            }
        }
    }
}