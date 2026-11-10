using UnityEngine;

public class ColorState : BaseState
{
    public Color color;
    public SpriteRenderer sprite;

    public override void StateEnter()
    {
        base.StateEnter();
        sprite.color = color;
    }
}
