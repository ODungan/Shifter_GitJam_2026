using UnityEngine;

public class AnimationState : BaseState
{
    public AnimationClip toPlay;
    public Animator anim;
    public AnimatorStateInfo stateInfo;
    public override void StateInit(StateMachine machine, BaseState parent = null)
    {
        base.StateInit(machine, parent);
        anim.Play(toPlay.name, -1, 0);
    }
    public override void StateUpdate()
    {
        base.StateUpdate();
        if(isActive == false){return;}
        stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.normalizedTime >= 1.0f && stateInfo.IsName(toPlay.name))
        {
            StateComplete();
        }
    }
}
