using AKVA.Assets.Vince.Scripts.Astar;
using System.Collections;
using System.Collections.Generic;
using AKVA.Animations;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.AI
{
    public class AIStateManager : MonoBehaviour
    {
        public bool activateAI;
        [HideInInspector] public RobotAIAnim robotAnim;
        [HideInInspector] public Rigidbody rb;

        [Header("PickUp")]
        public GameObject objOnHand;
        public Transform itemPlaceHolder;
        public LayerMask objectsToPick;
        public float sphereRadius = 1f;

        [Header("PickUp")]
        public LayerMask placesToDrop;
        [HideInInspector] public bool pickUp;

        [Header("Movement")]
        [HideInInspector] public MoveAI pathFind;
        [HideInInspector] public bool dropItem;
        [HideInInspector] public Transform currentTarget;
        public int targetIndex;
        public Transform[] targets;
        public bool moveOnly;
        public bool x;



        //states
        public AIState currentState;
        public MoveState moveState = new MoveState();
        public PickUpState pickUpState = new PickUpState();
        public DropState dropState = new DropState();
        public DeathState deathState = new DeathState();
        void Start()
        {
            pathFind = GetComponent<MoveAI>();
            robotAnim = GetComponent<RobotAIAnim>();
            rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            ActivateAI();
            if (currentState != null)
            {
                currentState.OnUpdateState(this);
            }
            HoldObject();

            if (x)
            {
                x = true;
                SwitchState(deathState);
            }
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
                if (targetIndex > 0)
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

        #region AnimProperties
        public void EnablePickUp()
        {
            pickUp = true;
        }

        public void EnableDrop()
        {
            dropItem = true;
        }
        #endregion
    }
}