using UnityEngine;
using System;
using System.Collections.Generic;
public class PropState : BaseState, iProp
{
    public BaseProp prop;
    public BaseState damageState;
    public BaseState destroyState;
    public int currentState = 0;
    public override void StateInit(StateMachine machine, BaseState parent = null)
    {
        base.StateInit(machine, parent);
        Subscribe();
    }
    public override void StateExit()
    {
        base.StateExit();
        Unsubscribe();
    }
    void OnDisable()
    {
        Unsubscribe();
    }
    public void Damage()
    {
        if(ActiveChild != null)
        {
            iProp child = ActiveChild as iProp;
            if(child != null)
            {
                child.Damage();
                return;
            }
        }
        if(damageState == null){return;}
        Transition.RequestStateChange(damageState, StateLeaf());
    }
    public void Destroy()
    {
        if(ActiveChild != null)
        {
            iProp child = ActiveChild as iProp;
            if(child != null)
            {
                child.Destroy();
                return;
            }
        }
        if(destroyState == null){return;}
        Transition.RequestStateChange(destroyState, StateLeaf());
    }
    public void Subscribe()
    {
        prop.OnDamage += Damage;
        prop.OnDestroy += Destroy;
    }
    public void Unsubscribe()
    {
        prop.OnDamage -= Damage;
        prop.OnDestroy -= Destroy;
    }
}
