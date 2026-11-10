using UnityEngine;

public class ColliderGrab : MonoBehaviour
{

    public Rigidbody2D rb;
    public void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        iGrabable target = collision.gameObject.GetComponent<iGrabable>();
        if(target != null)
        { // Need to add a target to follow
            target.Grabbed(gameObject);
        }
    }
}
