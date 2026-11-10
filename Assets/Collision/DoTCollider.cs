using UnityEngine;

public class DoTCollider : MonoBehaviour
{
    public int team;
    public int dmg;
    public BaseDamage dmgObject;
    public SpriteRenderer render;
    public float interval;
    private float timer;
    public void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        iDamageable target = collision.gameObject.GetComponent<iDamageable>();
        if(target != null)
        {
            target.DamageRequest(dmgObject);
        }
        Debug.Log("NOT DAMAGEABLE");
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        iDamageable target = collision.gameObject.GetComponent<iDamageable>();
        if(target != null)
        {
            target.DamageRequest(dmgObject);
        }
    }
    private void Update() 
    {
        timer += Time.deltaTime;
        if (timer >= interval) 
        {
            dmgObject = new BaseDamage(dmg, team, Vector2.zero); // new ID every 2 seconds
            render.color = Random.ColorHSV();
            timer = 0f;
        }
    }
}
