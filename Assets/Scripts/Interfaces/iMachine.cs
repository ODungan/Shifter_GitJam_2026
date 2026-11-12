using UnityEngine;

public interface iMachine
{
    void DefaultState();
    void SetState(BaseState to, BaseState from);
}
