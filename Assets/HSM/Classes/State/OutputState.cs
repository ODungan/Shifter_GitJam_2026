using UnityEngine;

public class OutputState : BaseState, iOutputable
{
    public BaseState DeathState;
    public BaseState GrabState;
    public BaseState HurtState;
    public BaseState ImpactState;
    public BaseState PushState;
    public BaseState StunState;
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

    public void Grab()
    {
        if(ActiveChild != null)
        {
            OutputState output = ActiveChild as OutputState;
            if(output != null)
            {
                output.Grab();
                return;
            }   
        }
        if(GrabState == null){return;}
        Transition.RequestStateChange(GrabState, StateLeaf());
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

    public void Impact()
    {
        if(ActiveChild != null)
        {
            OutputState output = ActiveChild as OutputState;
            if(output != null)
            {
                output.Impact();
                return;
            }
        }
        if(ImpactState == null){return;}
        Transition.RequestStateChange(ImpactState, StateLeaf());
    }

    public void Push()
    {
        if(ActiveChild != null)
        {
            OutputState output = ActiveChild as OutputState;
            if(output != null)
            {
                output.Push();
                return;
            }
        }
        if(PushState == null){return;}
        Transition.RequestStateChange(PushState, StateLeaf());
    }

    public void Stun()
    {
        if(ActiveChild != null)
        {
            OutputState output = ActiveChild as OutputState;
            if(output != null)
            {
                output.Stun();
                return;
            }
        }
        if(StunState == null){return;}
        Transition.RequestStateChange(StunState, StateLeaf());
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
