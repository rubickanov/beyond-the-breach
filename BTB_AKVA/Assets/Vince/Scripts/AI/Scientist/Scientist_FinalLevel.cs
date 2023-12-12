using AKVA.Animations;
using AKVA.Assets.Vince.Scripts.AI;
using AKVA.Assets.Vince.Scripts.Astar;
using AKVA.Vince.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scientist_FinalLevel : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float stoppingDistance = 3f;
    [SerializeField] bool activate;
    [SerializeField] float moveSpeed = 4f;
    [SerializeField] float chaseDelay = 3f;
    [SerializeField] Transform[] waypoints;
    [SerializeField] BoolReference playerDead;
    int targetIndex;
    MoveAI moveAI;
    ScientistAIAnim anim;
    Vector3 currentTarget;
    void Awake()
    {
        anim = GetComponent<ScientistAIAnim>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        moveAI = GetComponent<MoveAI>();
        currentTarget = waypoints[targetIndex].position;
    }

    void Update()
    {
        if (activate)
        {
            StartCoroutine(ChaseDelay());
            KillPlayerInRange();
        }
    }

    IEnumerator ChaseDelay()
    {
        yield return new WaitForSeconds(chaseDelay);
        ChaseTarget();
    }

    private void KillPlayerInRange()
    {
        if (Vector3.Distance(transform.position, target.position) <= stoppingDistance)
        {
            playerDead.value = true;
        }
    }

    void MoveAI()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, moveSpeed * Time.deltaTime);
    }

    private void ChaseTarget()
    {
        if (activate && !playerDead.value)
        {
            transform.LookAt(currentTarget);
            if (Vector3.Distance(transform.position, currentTarget) < stoppingDistance)
            {
                if (targetIndex < waypoints.Length - 1)
                {
                    targetIndex++;
                    currentTarget = waypoints[targetIndex].position;
                }
                else
                {
                    currentTarget = target.position;
                    stoppingDistance = 5f;

                }

            }
            anim.ChangeAnimState("Run");
            MoveAI();
        }
        else
        {
            anim.ChangeAnimState(anim.Robot_Idle);
        }
    }
}
