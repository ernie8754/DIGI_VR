using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ally : SummonObj
{
    private NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = this.moveSpeed;
        //agent.Warp(new Vector3(-0.84f, 10,0));
    }
    public enum State
    {
        BIRTH,
        FIND,
        MOVE,
        ATTACK,
        IDLE
    }
    public State state;
    void Update()
    {
        switch (state)
        {
            case State.BIRTH:
                BirthBehavior();
                break;
            case State.FIND:
                FindBehavior();
                break;
            case State.MOVE:
                MoveBehavior();
                break;
            case State.ATTACK:
                AttackBehavior();
                break;
            case State.IDLE:
                IdleBehavior();
                break;
            default:
                break;
        }
    }
    protected override void die()
    {
        throw new System.NotImplementedException();
    }
    protected override void move()
    {
        agent.SetDestination(Target.transform.position);
        state = State.MOVE;
    }

    public override SummonObj summon()
    {
        throw new System.NotImplementedException();
    }
    protected virtual void BirthBehavior() { }
    protected virtual void FindBehavior() {
        if (Target == null)
        {
            Target = FightSystem.fightSystem.findTarget(this);
        }
        else
        {
            move();
        }
    }
    protected virtual void MoveBehavior() {
        if (Vector3.Distance(this.transform.position, Target.transform.position) <= AttackDistance)
        {
            state = State.ATTACK;
            agent.isStopped = true;
        }
    }
    protected virtual void AttackBehavior() { }
    protected virtual void IdleBehavior() { }
}
