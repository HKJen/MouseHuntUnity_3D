using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : BaseState
{
    private NavMeshAgent agent;
    private Vector3 defPos;

    private Vector3 targetPos;

    private Animator animator;

    private Transform target;
    private MobStatsSO stats;

    public PatrolState(MobStateMachine states, Transform targetTransform, MobStatsSO statsSO)
    {
        stateMachine = states;
        agent = stateMachine.GetComponent<NavMeshAgent>();
        animator = stateMachine.GetComponent<Animator>();
        defPos = stateMachine.transform.position;
        target = targetTransform;
        stats = statsSO;

    }
    public override void Enter()
    {
        //throw new System.NotImplementedException();
        animator.SetBool("Walk", true);
        agent.isStopped = false;
        GeneratePatrolPoint();
    }

    private void GeneratePatrolPoint()
    {
        targetPos = defPos + new Vector3(Random.Range(-10,10), 0, Random.Range(-10,10));
        agent.SetDestination(targetPos);
    }

    public override void Exit()
    {
        animator.SetBool("Walk", false);
        agent.isStopped = true;
    }

    public override void LogicUpdate()
    {
        if (Vector3.Distance(stateMachine.transform.position, target.position) <= stats.FollowRange)
        {
            stateMachine.ChangeState(stateMachine.Follow);
            return;
        }
        
        if (Vector3.Distance(stateMachine.transform.position, targetPos) < 2)
        {
            stateMachine.ChangeState(stateMachine.Idle);
        }
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
