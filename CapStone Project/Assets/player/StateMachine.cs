using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// dummy derived class to set the default Generic Type value
/// </summary>
public class StateMachine : StateMachine<StateMachine>{}

//todo figure out if this really needs a type input
public class StateMachine<T>
{
    //public Dictionary<string, bool> Conditions;
    public virtual StateBase<T> currentstate
    {
        get { return _CurrentState; }
        set { _CurrentState = value; }

    }
    private StateBase<T> _CurrentState = null;
    // Start is called before the first frame update

    public void RunState()
    {
        if(currentstate) currentstate.Execute();
    }
        
    public void ChangeState(StateBase<T> newstate, T Caller)
    {
        //check newstate is not current state
        if (newstate != currentstate||(currentstate==null&&newstate!=null))
        {
            if(currentstate) currentstate.Exit();
            if(newstate) newstate.Enter(Caller,this);
            currentstate = newstate;
        }

        //return newstate != currentstate;
    }


    public virtual void EndState(T Caller)
    {
        ChangeState(null,Caller);
    }
}

/// <summary>
/// dummy derived class to set the default Generic Type value
/// </summary>
public class PushDownStateMachine:PushDownStateMachine<PushDownStateMachine>{}
public class PushDownStateMachine<T> : StateMachine<T>
{
    public override StateBase<T> currentstate
    {
        get
        {
            if(_CurrentState.Count!=0)
                return _CurrentState[_CurrentState.Count-1];
            return null;
        }
        set
        {
            if (value != null)
            {
                if(_CurrentState.Count!=0)
                    _CurrentState[_CurrentState.Count-1] = value;
                _CurrentState.Add(value);
            }
            else
            {
                _CurrentState.Remove(currentstate);
            }
        }

    }
    private List<StateBase<T>> _CurrentState = new List<StateBase<T>>();
    public void PushState(StateBase<T> newstate, T Caller)
    {
        //check newstate is not current state
        if (newstate != currentstate)
        {
            //todo figure out if a pushdown specific exit is needed, like a pause, might not be needed due to pausing by not running
            //if(currentstate[currentstate.Count]) currentstate[currentstate.Count].Exit();
            if(newstate) newstate.Enter(Caller,this);
            _CurrentState.Add(newstate);
        }
    }
    
    public override void EndState(T Caller)
    {
        if (currentstate != null)
        {
            currentstate.Exit();
            currentstate = null;
            if (currentstate)
            {
                //todo figure out if a pushdown specific enter is needed, like a resume, might not be needed due to already having been setup
            }
        }
        //ChangeState(null,Caller);
    }
}