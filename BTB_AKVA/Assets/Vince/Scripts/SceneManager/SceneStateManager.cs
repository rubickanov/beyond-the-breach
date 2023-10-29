using AKVA;
using AKVA.Controls;
using AKVA.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStateManager : MonoBehaviour
{
    SceneState currentState;
    public MovementTutorialState movementTutorial = new MovementTutorialState();
    public Room1State room1State = new Room1State();
    public Room2State room2State = new Room2State();

    [HideInInspector] public Transform playerTransform;

    [Header("Movement Tutorial")]
    public float timeDelayBeforePlayerMovement;
    public float timeDelayDuringTutorial;
    public DoubleDoor roomDoor;
    

    [Header("Room 1 Scene")]
    public Transform playerPlaceHolder;
    public AIStateManager[] listOfAI;
    public FloorButton[] listOfButtons; 

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        MovementTutorial();
    }

    private void Update()
    {
        if(currentState != null)
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
        PlayerInput.Instance.DisablePlayerMovement(false);
        StartCoroutine(StartMovementTutorial());
    }

    IEnumerator StartMovementTutorial()
    {
        yield return new WaitForSeconds(timeDelayBeforePlayerMovement);
        Debug.Log("Movement Tutorial Started");
        currentState = movementTutorial;
        currentState.OnEnterState(this);
    }
}