using UnityEngine;

public class CharacterDamage : MonoBehaviour
{
    public BaseCharacter owner;
    public BaseDamage dmgObject;
    public int damage;
    public int team;

    public void OnEnable() 
    {
        dmgObject = new BaseDamage(damage, team, Vector2.zero);
        owner.AddImmunity(dmgObject);    
    }
    public void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        iDamageable target = collision.gameObject.GetComponent<iDamageable>();
        if(target != null)
        {
            target.DamageRequest(dmgObject);
        }
        Debug.Log("NOT DAMAGEABLE");
    }
}
