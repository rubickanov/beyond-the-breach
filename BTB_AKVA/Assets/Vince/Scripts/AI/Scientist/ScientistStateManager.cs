using AKVA.Animations;
using AKVA.Assets.Vince.Scripts.Astar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.AI
{
    public class ScientistStateManager : MonoBehaviour
    {
        [HideInInspector] public ScientistAnim scientistAnim;

        [Header("Movement")]
        public Transform[] targets;
        public int targetIndex;
        public Transform currentTarget;
        [HideInInspector] public MoveAI pathFind;
        public bool activateAI;
        public bool moveOnly;

        public ScientistState currentState;
        public SciMoveState moveState = new SciMoveState();
        public InteractState interactState = new InteractState();
        public WorkState workState = new WorkState();

        private void Awake()
        {
            pathFind = GetComponent<MoveAI>();
            scientistAnim = GetComponent<ScientistAnim>();
        }

        // Update is called once per frame
        void Update()
        {
            ActivateAI();
            if (currentState != null)
            {
                currentState.OnUpdateState(this);
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                activateAI = true;
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

        public void SwitchState(ScientistState state)
        {
            currentState = state;
            state.OnEnterState(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            currentState?.OnCollisionEnter(this, other);
        }

        private void OnTriggerExit(Collider other)
        {
            currentState?.OnCollisionExit(this, other);
        }
    }
}