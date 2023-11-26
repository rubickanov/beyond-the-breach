using AKVA.Animations;
using AKVA.Assets.Vince.Scripts.AI;
using AKVA.Assets.Vince.Scripts.Astar;
using AKVA.Assets.Vince.Scripts.Environment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AKVA.Assets.Vince.Scripts.AI
{
    public class ScientistStateManager : MonoBehaviour
    {
        [Header("Movement")]
        public Transform[] targets;
        [HideInInspector] public MoveAI moveAi;
        public int targetIndex;
        public bool moveOnly;
        [HideInInspector] public ScientistAIAnim sciAnim;

        public bool activate;
        ScientistState currentState;
        public SciMoveState moveState = new SciMoveState();
        public SciInteractState interactState = new SciInteractState();
        void Start()
        {
            sciAnim = GetComponent<ScientistAIAnim>();
            moveAi = GetComponent<MoveAI>();
        }

        // Update is called once per frame
        void Update()
        {
            if (activate)
            {
                activate = false;
                currentState = moveState;
                currentState?.OnEnterState(this);
            }
            currentState?.OnUpdateState(this);
        }

        public void SwitchState(ScientistState state)
        {
            currentState = state;
            state.OnEnterState(this);
        }

        private void OnTriggerStay(Collider other)
        {
            if (currentState == interactState)
            {
                currentState?.OnCollisionEnter(this, other);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            DisableDoubleDoor(other);
        }

        private static void DisableDoubleDoor(Collider other)
        {
            if (other.GetComponent<DoubleDoor>() != null)
            {
                other.GetComponent<DoubleDoor>().EnableDoor = false;
            }
        }

        public void MindControl()
        {
            sciAnim.ChangeAnimState(sciAnim.Robot_Idle);
            if(currentState == moveState || currentState == interactState)
            {
                moveAi.FindPath(transform);
                moveAi.enabled = false;
                this.enabled = false;
            }
        }
    }
}
