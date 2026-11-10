using UnityEngine;
using System;
using System.Collections.Generic;
public class Transition
{
    private StateMachine _machine;
    public Transition(StateMachine machine)
    {
        _machine = machine;
    }
    public void RequestStateChange(BaseState to, BaseState from)
    {
        _machine.SetState(to, from);
    }
    public BaseState CommonState(BaseState a, BaseState b)
    {
        HashSet<BaseState> compare = new HashSet<BaseState>();
        for (BaseState state = a; state != null; state = state.Parent){compare.Add(state);}
        for (BaseState state = b; state != null; state = state.Parent)
        {
            if (compare.Contains(state))
            {
                Debug.Log($"Common state between {a} and {b} is {state}");
                return state;
            }
        }
        Debug.Log($"No Common State between {a} and {b}");
        return null;
    }
}
