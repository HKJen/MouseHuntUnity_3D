using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    //private Vector2 stayTimeBorders;
    private float curStayTime;
    private float timer;

    private Transform target;
    private MobStatsSO stats;

    public IdleState(MobStateMachine mobStateMachine, Transform targetTransform, MobStatsSO statsSO)
    {
        stateMachine = mobStateMachine;
        target = targetTransform;
        stats = statsSO;
    }
    
    public override void Enter()
    {
        curStayTime = Random.Range(stats.MaxStayTimeBorders.x, stats.MaxStayTimeBorders.y);
    }

    public override void Exit()
    {
        timer = 0f;
    }

    public override void LogicUpdate()
    {
        if (Vector3.Distance(stateMachine.transform.position, target.position) <= stats.FollowRange)
        {
            stateMachine.ChangeState(stateMachine.Follow);
            return;
        }
        
        if (timer < curStayTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            //patrol logic
            stateMachine.ChangeState(stateMachine.Patrol);
        }
        //throw new System.NotImplementedException();
    }
}
