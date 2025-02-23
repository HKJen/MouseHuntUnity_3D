using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState 
{
    protected MobStateMachine stateMachine;
    public abstract void Enter();
    public abstract void LogicUpdate();
    public abstract void Exit();
}
