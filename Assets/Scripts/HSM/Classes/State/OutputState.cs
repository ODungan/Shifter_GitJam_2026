using UnityEngine;

public class OutputState : BaseState, iOutputable
{
    public BaseState DeathState;
    public BaseState HurtState;
    public void Die()
    {
        if(ActiveChild != null)
        {
            OutputState output = ActiveChild as OutputState;
            if(output != null)
            {
                output.Die();
                return;
            }
        }
        if(DeathState == null){return;}
        Transition.RequestStateChange(DeathState, StateLeaf());
    }

    public void Hurt()
    {
        if(ActiveChild != null)
        {
            OutputState output = ActiveChild as OutputState;
            if(output != null)
            {
                output.Hurt();
                return;
            }
        }
        if(HurtState == null){return;}
        Transition.RequestStateChange(HurtState, StateLeaf());
    }


    public void Stun()
    {
        Debug.Log("Not stunable >:)");
    }
    
    public override void ChildStateComplete()
    {
        if(ActiveChild != null){ActiveChild.StateExit();}
        if(ActiveChild == Default)
        {
            StateComplete();
            return;
        }
        ActiveChild = null;
        StateDefault();
    }

}
