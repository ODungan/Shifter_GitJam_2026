using UnityEngine;
public class ColliderImpact : MonoBehaviour
{
    public int damage;
    public int resolve;
    public int team;
    public Vector2 impact = Vector2.zero;
    protected BaseDamage dmgObject;

    void OnEnable()
    {
        dmgObject = new BaseDamage(damage, resolve, team, Vector2.zero);
    }
    
    public virtual void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        iMoveable target = collision.gameObject.GetComponent<iMoveable>();
        if(target != null)
        {
            Debug.Log($"Impact target: {target}");
            if(collision.relativeVelocity.magnitude > impact.magnitude)
            {
                target.Impact(dmgObject);
            }
        }
        Debug.Log("NOT DAMAGEABLE");
    }
}
