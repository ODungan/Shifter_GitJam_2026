using UnityEngine;

public class ComboState : InputState
{
    public override void West()
    {
        if(ActiveChild != null)
        {
            InputState input = ActiveChild as InputState;
            if(input != null)
            {
                input.West();
                return;
            }
        }
        if(WestState == null){return;}
        StateChild(WestState);
    }
    public override void North()
    {
        if(ActiveChild != null)
        {
            InputState input = ActiveChild as InputState;
            if(input != null)
            {
                input.North();
                return;
            }
        }
        if(NorthState == null){return;}
        StateChild(NorthState);
    }
    public override void East()
    {
        if(ActiveChild != null)
        {
            InputState input = ActiveChild as InputState;
            if(input != null)
            {
                input.East();
                return;
            }
        }
        if(EastState == null){return;}
        StateChild(EastState);
    }
    public override void South()
    {
        if(ActiveChild != null)
        {
            InputState input = ActiveChild as InputState;
            if(input != null)
            {
                input.South();
                return;
            }
        }
        if(SouthState == null){return;}
        StateChild(SouthState);
    }
}
