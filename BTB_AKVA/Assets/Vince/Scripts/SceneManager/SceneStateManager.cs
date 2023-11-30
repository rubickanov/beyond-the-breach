using AKVA.Assets.Vince.Scripts.Environment;
using AKVA.Assets.Vince.Scripts.AI;
using AKVA.Controls;
using AKVA.Player;
using AKVA.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AKVA.Assets.Vince.SO;
using AKVA.Vince.SO;

namespace AKVA.Assets.Vince.Scripts.SceneManager
{
    public class SceneStateManager : MonoBehaviour
    {
        SceneState currentState;
        public MovementTutorialState movementTutorial = new MovementTutorialState();
        public Room1State room1State = new Room1State();
        public Room2State room2State = new Room2State();
        public Room3State room3State = new Room3State();

        public Transform playerTransform;
        [HideInInspector] public Picking playerPicking;
        public RuntimeList listOfAI;


        [Header("Movement Tutorial")]
        public float timeDelayBeforePlayerMovement;
        public float timeDelayDuringTutorial;
        public DoubleDoor roomDoor;
        [HideInInspector] public TutorialScreen tutorialScreen;

        [Header("Room 1 Scene")]
        public DoubleDoor room1Door;
        public Transform room1PlayerPos;
        public RuntimeList room1Buttons;
        public RuntimeList room1Boxes;
        public RuntimeList room1PositionPoints;

        [Header("Room 2 Scene")]
        public Transform room2PlayerPos;
        public RuntimeList room2Buttons;
        public DoubleDoor room2Door;
        [HideInInspector] public Transform aiPos;

        [Header("Room 3 Scene")]
        public Transform room3PlayerPos;
        public BoolReference[] imagesAppeared;
        public BoolReference tvTurnedOn;
        public Transform[] aiDestination;

        private void Awake()
        {
           //playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            playerPicking = playerTransform.GetComponent<Picking>();
            tutorialScreen = FindObjectOfType<TutorialScreen>();
            playerPicking.enabled = false;
            PlayerInput.Instance.DisablePlayerMovement();
            MovementTutorial();
        }

        private void Update()
        {
            if (currentState != null)
            {
                currentState.OnUpdateState(this);
            }
        }

        public void SwitchState(SceneState state)
        {
            currentState = state;
            state.OnEnterState(this);
        }

        private void MovementTutorial()
        {
            PlayerInput.Instance.EnablePlayerMovement();
            StartCoroutine(StartMovementTutorial());
        }

        IEnumerator StartMovementTutorial()
        {
            yield return new WaitForSeconds(timeDelayBeforePlayerMovement);
            currentState = movementTutorial;
            currentState.OnEnterState(this);
        }
    }
}