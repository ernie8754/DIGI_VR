using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class SummonObj : MonoBehaviour
{
    public int ATK;
    public int HP;
    public int moveSpeed;
    public int consumeEnergy;
    public int AttackDistance;
    public Animation ani;
    
    protected SummonObj Target=null;
    protected NavMeshAgent agent;

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
        if (obj)
        {
            obj.hurt(ATK);
        }
    }
    protected abstract void die();
    public virtual void hurt(int damage)
    {
        HP -= damage;
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
        if (ani["birth"].normalizedTime >= 1.0f)
        {
            state = State.FIND;
        }
    }
    protected virtual void FindBehavior() { }
    protected virtual void MoveBehavior()
    {
        if (Vector3.Distance(this.transform.position, Target.transform.position) <= AttackDistance)
        {
            state = State.ATTACK;
            agent.isStopped = true;
        }
    }
    protected virtual void AttackBehavior() 
    {
        ani.Play("attack");
        bool HaveAttacked = false;
        if (ani["attack"].normalizedTime >= 0.5f && !HaveAttacked)
        {
            HaveAttacked = true;
            attack(Target);
        }
    }
    protected virtual void IdleBehavior() { }
}
