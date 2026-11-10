using UnityEngine;
using System;
public class BaseProp : MonoBehaviour, iDamageable
{
    public float health;
    public event Action OnDamage;
    public event Action OnDestroy;
    public void DamageRequest(BaseDamage dmg)
    {
        if(dmg.damage >= health)
        {
            health = 0;
            Death();
            return;
        }
        else
        {
            health -= dmg.damage;
        }
        OnDamage?.Invoke();
    }
    public void Death()
    {
        OnDestroy?.Invoke();
    }
}
public interface iProp
{
    public void Damage();
    public void Destroy();
}
