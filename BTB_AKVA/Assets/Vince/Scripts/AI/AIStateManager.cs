using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateManager : MonoBehaviour
{
    [SerializeField] bool activateAI;

    [Header("PickUp")]
    public GameObject objOnHand;
    public Transform itemPlaceHolder;
    public LayerMask objectsToPick;
    public float sphereRadius = 1f;

    [Header("Movement")]
    [HideInInspector] public MoveAI pathFind;
    [HideInInspector] public Transform currentTarget;
    public Transform firstTarget, secondTarget;


    //states
    public AIState currentState;
    public MoveState moveState = new MoveState();
    public PickUpState pickUpState = new PickUpState();
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
        if(activateAI)
        {
            currentTarget = firstTarget;
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
        if(itemPlaceHolder != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(itemPlaceHolder.position, sphereRadius);
        }
    }
}
