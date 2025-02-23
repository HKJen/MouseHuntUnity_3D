using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private float attackAnimTimerBefore = 0.52f;
    private float attackAnimTimerFull = 1.133f;
    private bool attackedPlayer;
    private float timer;

    private Transform target;
    private Animator animator;
    private MobStatsSO stats;
    private PlayerStats playerStats;

    public AttackState(MobStateMachine states, Transform targetTransform, MobStatsSO statsSO)
    {
        stateMachine = states;
        animator = stateMachine.GetComponent<Animator>();
        target = targetTransform;
        stats = statsSO;
        playerStats = target.GetComponent<PlayerStats>();
    }

    public override void Enter()
    {
        animator.SetBool("Attack", true);
    }

    public override void Exit()
    {
        animator.SetBool("Attack", false);
        timer = 0f;
        attackedPlayer = false;
    }

    public override void LogicUpdate()
    {
        if (Vector3.Distance(stateMachine.transform.position, target.position) > stats.AttackRange)
        {
            stateMachine.ChangeState(stateMachine.Follow);
            return;
        }

        timer += Time.deltaTime;

        if (!attackedPlayer)
        {
            if (timer >= attackAnimTimerBefore)
            {
                attackedPlayer = true;
                //deal damage to player
                playerStats.TakeDamage(stats.Power);
            }
        }

        else
        {
            if (timer >= attackAnimTimerFull)
            {
                attackedPlayer = false;
                timer = 0f;
            }
        }
    }
}
