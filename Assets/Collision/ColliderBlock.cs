using UnityEngine;

public class ColliderBlock : MonoBehaviour, iDamageable
{
    public BaseCharacter character;
    public void DamageRequest(BaseDamage dmg)
    {
        if(dmg == null){return;}
        Debug.Log("Block Event Fired");
        character.AddImmunity(dmg.ID);
    }
}
