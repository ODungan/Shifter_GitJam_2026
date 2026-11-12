using UnityEngine;

public interface iState
{
    void StateInit(StateMachine machine, BaseState parent);
    void StateEnter();
    void StateUpdate();
    void StateFixedUpdate();
    void StateComplete();
    void StateChild(BaseState state);
    void ChildStateComplete();
    void StateExit();
    void StateDefault();
    BaseState StateTransition();
    BaseState StateLeaf();
}
