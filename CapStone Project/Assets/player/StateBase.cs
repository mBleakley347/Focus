using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class StateBase<T> : MonoBehaviour
{
    protected T owner;
    protected StateMachine<T> ownerMachine;
    public virtual void Enter(T Caller,StateMachine<T> CallerMachine)
    {
        owner = Caller;
        ownerMachine = CallerMachine;
    }

    public virtual void Execute()
    {
        
    }

    public virtual void Exit()
    {
        
    }
}