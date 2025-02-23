using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowState : BaseState
{
    private Transform target;
    private NavMeshAgent agent;
    private Animator animator;
    private MobStatsSO stats;

    public FollowState(MobStateMachine states, Transform targetTransform, MobStatsSO statsSO)
    {
        stateMachine = states;
        agent = stateMachine.GetComponent<NavMeshAgent>();
        animator = stateMachine.GetComponent<Animator>();
        target = targetTransform;
        stats = statsSO;
    }
    public override void Enter()
    {
        animator.SetBool("Walk", true);
        agent.isStopped = false;
    }

    public override void Exit()
    {
        animator.SetBool("Walk", false);
        agent.isStopped = true;
    }

    public override void LogicUpdate()
    {
        agent.SetDestination(target.position);

        if(Vector3.Distance(stateMachine.transform.position, target.transform.position) > stats.FollowRange)
        {
            stateMachine.ChangeState(stateMachine.Idle);
        }
        else if(Vector3.Distance(stateMachine.transform.position, target.transform.position) <= stats.AttackRange)
        {
            stateMachine.ChangeState(stateMachine.Attack);
        }
    }

}
