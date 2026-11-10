using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
public class BaseCharacter : MonoBehaviour, iDamageable, iGrabable, iMoveable
{
    #region Variables
    public CharacterObject character; // default character, new ones can be passed in
    public float health;
    public float resolve;
    public float speed;
    public float dash;
    [Tooltip("Physics Threshold")]
    public float threshold;
    public int team; // first implementation of team system
    public float regen;
    public float regenDelay;
    public float hitTimer;
    public List<string> immunities = new List<string>();
    public Rigidbody2D body;
    public Vector2 pushed;
    public GameObject follow;
    #endregion
    #region Events
    public event Action OnHealth; // hurt state, health UI
    public event Action OnResolve; // stun state, resolve UI
    //public event Action OnGrabbed; worry about ts later
    public event Action OnPush; // velocity > 0 state
    public event Action OnImpact; // velocity = 0, health dmg is taken
    public event Action OnGrab; // match location to collider / enter grab anim
    public event Action OnDeath; // health < 0
    #endregion
    #region iDamageable, iMoveable

    void Start()
    {
        CharacterStats(character);
    }
    void Update()
    {
        hitTimer += Time.deltaTime;
        if(hitTimer > regenDelay && resolve < character.MaxResolve)
        {
            resolve += Time.deltaTime * regen;
        }
    }
    public void CharacterStats(CharacterObject characterObj) //Can be changed at anytime from new character, might include HSM too
    {
        character = characterObj;
        team = character.Team;
        health = character.MaxHealth;
        resolve = character.MaxResolve;
        regen = character.RecoverySpeed;
        speed = character.MovementSpeed;
        dash = character.DashDistance;
        threshold = character.PhysicsThreshold;
    }
    private BaseDamage damage = new BaseDamage(0,0,Vector2.zero);
    // private BaseDamage myDamage, 
    public void MovementRequest(Vector2 direction)
    {
        pushed = direction;
        OnPush?.Invoke(); // add vector2 data
    }
    public void Impact(BaseDamage dmg)
    {
        OnImpact?.Invoke();
        Health(damage.damage);
        Resolve(damage.resolve);
    }
    public void Damage()
    {
        AddImmunity(damage);
        hitTimer = 0;
        Health(damage.damage);
        Resolve(damage.resolve);
        if (damage.impact.magnitude > threshold)
        {
            MovementRequest(damage.impact);
        }
        else
        {
            body.AddForce(damage.impact, ForceMode2D.Impulse);
        }
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
    public void Resolve(int dmg)
    {
        if(resolve - dmg <= 0)
        {
            Stun();
            resolve = 0;
            return;    
        }
        resolve -= dmg;
    }
    public void Resolve(int dmg, bool cooldown)
    {
        if(cooldown == true)
        {
            hitTimer = 0;
        }
        if(resolve - dmg <= 0)
        {
            Stun();
            resolve = 0;
            return;    
        }
        resolve -= dmg;
        Debug.Log($"Resolve: {resolve}, Damage: {dmg}");
    }
    public void Stun()
    {
        OnResolve?.Invoke();
    }
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
    public void AddImmunity(string ID) // Block immunity for immediate damage.ID ignore, querey above takes too long
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

    public void Grabbed(GameObject Follow)
    {
        follow = Follow;
        OnGrab?.Invoke();
    }
    #endregion
    public CharacterContext Context() // worth doing?
    {
        CharacterContext context = new CharacterContext(this);
        return context;
    }
}
public class CharacterContext // even necessary? Create in statemachine to seperate character from HSM
{
    public int Team;
    public float Health;
    public float Resolve;
    public Vector2 Facing;
    public Rigidbody2D Body;
    public CharacterContext(BaseCharacter character)
    {
        Team = character.team;
        Health = character.health;
        Resolve = character.health;
        Facing = character.pushed; // change to a facing variable
        Body = character.body;
    }
}