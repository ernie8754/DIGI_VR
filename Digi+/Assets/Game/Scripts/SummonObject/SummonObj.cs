using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class SummonObj : MonoBehaviour
{
    public int ATK;
    public int MaxHp;
    public int moveSpeed;
    public int consumeEnergy;
    public int AttackDistance;
    public Animation ani;

    public int HP;
    protected SummonObj Target=null;
    protected NavMeshAgent agent;
    protected bool IsDead=false;

    [SerializeField] private Image HpBar;

    public enum State
    {
        BIRTH,
        FIND,
        MOVE,
        ATTACK,
        DIE,
        IDLE
    }
    public State state;
    protected virtual void attack(SummonObj obj)
    {
        if (obj && obj.HP>0)
        {
            obj.hurt(ATK);
        }
    }
    protected abstract void die();
    public virtual void hurt(int damage)
    {
        HP -= damage;
        HpBar.fillAmount = HP * 1.0f / MaxHp;
        if (HP <= 0)
        {
            die();
        }
    }
    protected virtual void move()
    {
        if (Target)
        {
            agent.SetDestination(Target.transform.position);
            ani.Play("walk");
            state = State.MOVE;
        }
    }
    protected virtual void BirthBehavior() 
    {
        if (ani["birth"].normalizedTime >= 0.9f)
        {
            state = State.FIND;
        }
    }
    protected virtual void FindBehavior() { }
    protected virtual void MoveBehavior()
    {
        if (Target && Vector3.Distance(this.transform.position, Target.transform.position) <= AttackDistance)
        {
            state = State.ATTACK;
            //ani.Play("attack");
            agent.isStopped = true;
        }
        else
        {
            state = State.FIND;
        }
    }
    protected float cooldown = 0;
    protected virtual void AttackBehavior() 
    {
        if (!Target || Vector3.Distance(this.transform.position, Target.transform.position) > AttackDistance)
        {
            state = State.FIND;
        }
        if (Target && cooldown <= 0 && ani["attack"].normalizedTime >= 0.5f)
        {
            attack(Target);
            cooldown = 1.5f;
        }
        if (cooldown > 0)
        {
            cooldown-=Time.deltaTime;
        }
        else
        {
            ani.Play("attack");
        }
    }
    protected virtual void IdleBehavior() { }
}
