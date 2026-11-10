using UnityEngine;

public class ColliderPull : MonoBehaviour
{
    public int damage;
    public int resolve;
    public float force;
    public int team;
    public GameObject target;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        iDamageable hit = collision.gameObject.GetComponent<iDamageable>();
        if(hit != null)
        {
            Vector2 direction = target.transform.position - collision.gameObject.transform.position;
            BaseDamage dmg = new BaseDamage(damage, resolve, team, direction * force);
            hit.DamageRequest(dmg);
        }
    }
}
