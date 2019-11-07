using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SummonObj : MonoBehaviour
{
    public int ATK;
    public int HP;
    public int moveSpeed;
    public int consumeEnergy;
    public int AttackDistance;
    public SummonObj Target=null;
    
    public virtual void attack(SummonObj obj)
    {
        obj.hurt(ATK);
    }
    public abstract void die();
    public virtual void hurt(int damage)
    {
        HP -= damage;
    }
    public abstract SummonObj summon();
    public abstract void move();
}
