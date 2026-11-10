using UnityEngine;
using System;
public class BaseDamage
{
    public int damage; // consider a property {get; private set;}
    public int resolve;
    public Vector2 impact;
    public string ID;
    public int origin;

    public BaseDamage(int dmg, int team, Vector2 force)
    {
        damage = dmg;
        resolve = 0;
        impact = force;
        origin = team;
        ID = DamageID();
    }
    public BaseDamage(int dmg, int res, int team, Vector2 force)
    {
        damage = dmg;
        resolve = res;
        impact = force;
        origin = team;
        ID = DamageID();
    }
    public string DamageID()
    {
        Guid guid = Guid.NewGuid();
        string id = guid.ToString();
        return id;
    }
}
