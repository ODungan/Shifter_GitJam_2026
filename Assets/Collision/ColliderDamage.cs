using UnityEngine;

public class ColliderDamage : MonoBehaviour
{
    public int damage;
    public int resolve;
    public int team;
    public bool active = false;
    public Vector2 force = Vector2.zero;
    protected BaseDamage dmgObject;

    void OnEnable()
    {
        dmgObject = new BaseDamage(damage, resolve, team, force);
    }
    public virtual void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        Debug.Log($"Collision: {collision}");
        iDamageable target = collision.gameObject.GetComponent<iDamageable>();
        if(target != null)
        {
            target.DamageRequest(dmgObject);
            gameObject.SetActive(active);
        }
        Debug.Log("NOT DAMAGEABLE");
    }
}
