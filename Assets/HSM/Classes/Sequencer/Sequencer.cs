using UnityEngine;

public class Sequencer : MonoBehaviour
{
    private void Awake() 
    {
        gameObject.GetComponentInChildren<BaseState>();   
    }
}
