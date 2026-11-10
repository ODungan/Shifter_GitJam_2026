using UnityEngine;
public class StateMachine : MonoBehaviour, iMachine
{
    public BaseState root;
    protected Transition transition;

    public void Start()
    {
        transition = new Transition(this);
        DefaultState();
    }
    public void DefaultState()
    {
        root.StateInit(this);
    }
    public void Update()
    {
        if(root.isActive == true){root.StateUpdate();}
    }
    public void FixedUpdate()
    {
        if(root.isActive == true){root.StateFixedUpdate();}
    }
    //Recursion was what was needed from git-amend and the text book, now I can reimplement old features
    public void SetState(BaseState to, BaseState from)
    {
        if(from == to || from == null || to == null){return;}
        Debug.Log($"SetState -> to {to}state, {to.gameObject} from {from}state, {from.gameObject}");
        BaseState commonState = transition.CommonState(to, from);
        commonState.StateChild(to);
    }

}
