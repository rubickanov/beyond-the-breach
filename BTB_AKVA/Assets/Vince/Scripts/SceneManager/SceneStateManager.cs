using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStateManager : MonoBehaviour
{
    SceneState currentState;
    public MovementTutorialState movementTutorial = new MovementTutorialState();
    public Room1State room1State = new Room1State();

    [Header("Movement Tutorial")]
    public ControlsSO controlsSO;
    public float timeDelayBeforePlayerMovement;
    public float timeDelayDuringTutorial;
    public DoubleDoor roomDoor;
    [HideInInspector] public PlayerMovement playerMovement;
    //[HideInInspector] public bool[] movementTask;

    [Header("Room 1 Scene")]
    public Transform playerPlaceHolder;
    public AIStateManager[] listOfAI;
    public FloorButton[] listOfButtons; 

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
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
        playerMovement.enabled = false;
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