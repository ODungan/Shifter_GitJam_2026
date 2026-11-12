using UnityEngine.InputSystem;
public interface iPlayable
{
    public void LeftTrigger(){}
    public void RightTrigger(){}
    public void West(){}
    public void North(){}
    public void East(){}
    public void South(){}
    public void Move(InputAction direction){}
}
