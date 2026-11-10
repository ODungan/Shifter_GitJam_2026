using UnityEngine;

public class PropTest : MonoBehaviour, iDamageable
{
    public SpriteRenderer sprite;
    public void DamageRequest(BaseDamage dmg)
    {
        sprite.color = Color.red;
    }
}
