using UnityEngine;

public class Context : MonoBehaviour
{
    public BaseCharacter character;

    void Awake()
    {
        //subscribe to character events, send self to statemachine on a tick (Update) 
    }
}
