using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
public class BaseCharacter : MonoBehaviour, iDamageable
{
    #region Variables
    public float health;
    public int penance;
    public float speed;
    public float dash;
    [Tooltip("Physics Threshold")]
    public float threshold;
    public int team; // first implementation of team system
    public List<string> immunities = new List<string>();
    public Rigidbody2D body;
    #endregion
    #region Events
    public event Action OnHealth; // hurt state, health UI
    public event Action OnPenence; 
    public event Action OnDeath; // health < 0
    #endregion
    #region iDamageable, iMoveable

    private BaseDamage damage = new BaseDamage(0,0,Vector2.zero);
    // private BaseDamage myDamage, 
    public void Damage()
    {
        AddImmunity(damage);
        Health(damage.damage);
        Penance(damage.damage);
    }
    public void Health(int dmg)
    {
        if (health - dmg <= 0)
        {
            health = 0;
            Death();
            return;
        }
        else if(dmg <= 0){return;}
        health -= dmg;
        OnHealth?.Invoke();
    }
    public void Penance(int dmg)
    {
        int half = Mathf.FloorToInt(dmg/2);
        penance += half;
    }
    //Only enemeies can be stunned, they take stamina dmg equal to double incoming dmg
    /*public void Stun()
    {
        OnPenence?.Invoke();
    }*/
    public void Death()
    {
        OnDeath?.Invoke();
        AddImmunity(damage);
        ClearImmunity();
    }
    public void AddImmunity(BaseDamage damage) // agnostic enough to be called for block, but also works internally
    {
        if(Immune() == false) // Immune Quesrey is too slow for block logic
        {
            immunities.Add(damage.ID);
        }
    }
    public void AddImmunity(string ID) // Block immunity for immediate damage.ID ignore
    {
        immunities.Add(ID);
    }
    public void ClearImmunity()
    {
        for (int i = immunities.Count - 1; i >= 1 ; i--) //count minus one
        {
            immunities.RemoveAt(i);
        }
    }
    public bool Immune()
    {
        for (int i = 0; i < immunities.Count; i++)
        {
            if(immunities[i] == damage.ID)
            {
                Debug.Log($"{this.name} is immune to {damage.ID}");
                return true;
            }
        }
        return false;
    }

    public void DamageRequest(BaseDamage dmg)
    {
        if(dmg == null){return;}
        damage = dmg;
        if(damage.origin == team || Immune() == true)
        {
            Debug.Log($"{dmg} is not valid damage for {name}");
            return;
        }
        else
        {
            Damage();
        }
    }

    #endregion
}