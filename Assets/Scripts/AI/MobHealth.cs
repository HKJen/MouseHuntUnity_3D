using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobHealth : MonoBehaviour
{
    private MobStateMachine stateMachine;
    private float curHp;
    
    void Start()
    {
        stateMachine = GetComponent<MobStateMachine>();
        curHp = stateMachine.Stats.MaxHealth;
    }
    
    public void TakeDamage(float damage)
    {
        curHp -= damage;

        if (curHp <= 0)
        {
            stateMachine.ChangeState(stateMachine.Death);
            enabled = false;
        }
    }

    private void OnMouseDown()
    {
        TakeDamage(100);
    }
}
