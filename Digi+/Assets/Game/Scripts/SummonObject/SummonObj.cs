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
    public float walkAniWaitSec;
    public float attackAniWaitSec;
    public SummonObj Target=null;
    
    protected virtual void attack(SummonObj obj)
    {
        obj.hurt(ATK);
    }
    protected abstract void die();
    public virtual void hurt(int damage)
    {
        HP -= damage;
    }
    public abstract SummonObj summon();
    protected abstract void move();
}
