using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Entity : MonoBehaviour
{
    public int Health = 100; //Network Variable
    public string Name = "Entity"; //Network Variable
    public int KillXP = 100; //Server-only
    public int XP; //Network Variable

    public Action<Entity> OnDie;

    public void GiveXP(int xp)
    {
        XP += xp;
    }

    public void Start()
    {
        var names = new string[] { "John", "Buddy", "Lowi", "Argessive Cube", "Sanchos", "Mika", "Mike", "Carlos" };
        Name = names[Random.Range(0, names.Length)];
        TakeDamage(100, this);
    }

    public void TakeDamage(int amount, Entity aggressor) //Server-only
    {
        Health = Math.Max(Health - amount, 0);
        if (Health == 0)
        {
            Die(aggressor);
        }
    }
    
    public void Die(Entity killer) //Server-only
    {
        OnDie?.Invoke(this);
        if (killer != null)
        {
            killer.GiveXP(KillXP);
            Debug.Log($"{Name} was killed by {killer.Name}."); //Client code
        }
        else
        {
            Debug.Log($"{Name} died."); //Client code
        }
    }
}
