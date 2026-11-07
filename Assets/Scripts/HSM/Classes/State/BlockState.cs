using UnityEngine;

public class BlockState : InputState
{
    public override void South()
    {
        if(ActiveChild!= null)
        {
            InputState input = ActiveChild as InputState;
            if(input != null)
            {
                input.South();
                return;
            }
        }
        if(ActiveChild == Default)
        {
            StateComplete();
            return;
        }
        if(SouthState == null){return;}
        Transition.RequestStateChange(SouthState, StateLeaf());
    }
    public override void StateUpdate()
    {
        base.StateUpdate();
        if(Input._player.Combat.South.IsPressed() == false)
        {
            StateComplete();
        }   
    }
}
    
