using System.Collections;
using UnityEngine;

public class BaseState : MonoBehaviour, iState
{
    [Tooltip("Inspector Assigned")]
    public StateMachine Machine;
    [Tooltip("Inspector Assigned")]
    public BaseState Default;
    [Tooltip("Inspector Assigned")]
    public BaseState Parent;
    public BaseState ActiveChild;
    public bool isRoot = false;
    public bool isActive = false;
    protected Transition Transition;
    protected float startTime;
    public float timeActive => Time.time - startTime;
    #region State Methods
    /// <summary>
    /// State initializer, called through root State => Machine, or through parent State
    /// </summary>
    /// <param name="machine">State Machine from root, or parent</param>
    /// <param name="parent">Determines if this State is active child, default null => Root State</param>
    public virtual void StateInit(StateMachine machine, BaseState parent = null)
    {
        Machine = machine;
        Parent = parent;
        Debug.Log($"Init for {this}, parent is {Parent}");
        startTime = Time.time;
        StateEnter();
    }
    public virtual void StateEnter()
    {
        Transition = new Transition(Machine);
        if(Parent != null)
        {
            Parent.ActiveChild = this;
            Debug.Log($"Entered {gameObject} H_State, {Parent} Child is {Parent.ActiveChild}");
        }
        else
        {
            isRoot = true;
            Debug.Log($"Entered {gameObject} state");
        }
        BaseState child = Default;
        if (child != null){StateChild(child);}
        Enter();
    }
    public virtual void StateComplete()
    {
        Parent?.ChildStateComplete();
    }
    public virtual void StateExit()
    {
        if(ActiveChild != null){ActiveChild.StateExit();}
        ActiveChild = null;
        isActive = false;
        Debug.Log($"Was in {this} state");
        Exit();
    }
    public virtual void StateUpdate()
    {
        if(isActive == false){return;}
        ActiveChild?.StateUpdate();
    }
    public virtual void StateFixedUpdate()
    {
        if(isActive == false){return;}
        ActiveChild?.StateFixedUpdate();
    }
    public virtual void StateDefault()
    {
        StateChild(Default);
    }
    public virtual BaseState StateTransition()
    {
        //serve up which state to transfer to, update will request a transition
        //transition is based on context, which can be events or hard data
        //state machine will carry out recursive exit and entrence of returned state
        throw new System.NotImplementedException();
    }
    public virtual BaseState StateLeaf()
    {
        BaseState state = this;
        while(state.ActiveChild != null){state = state.ActiveChild;}
        return state;
    }
    #endregion
    protected virtual void Enter()
    {
        isActive = true;
    }
    protected virtual void Exit()
    {
        isActive = false;
    }

    public virtual void ChildStateComplete()
    {
        if(ActiveChild != null){ActiveChild.StateExit();}
        ActiveChild = null;
        BaseState child = Default;
        if (child != null)
        {
            StateChild(child);
        }
    }

    public void StateChild(BaseState state)
    {
        if(state == null){return;}
        if(ActiveChild != null)
        {ActiveChild.StateExit();}
        ActiveChild = null;
        state.StateInit(Machine, this);
    }
}
