using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LeuObj : Ally
{
    void Start()
    {
        HP = MaxHp;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = this.moveSpeed;
        //agent.Warp(new Vector3(-0.84f, 10,0));
    }
    void Update()
    {
        if (IsDead)
        {
            die();
            return;
        }
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
            case State.DIE:
                //die();
                break;
            default:
                break;
        }
    }
    protected override void move()
    {
        if (Target)
        {
            agent.SetDestination(Target.transform.position);
            agent.isStopped = false;
            ani.Play("walk");
            state = State.MOVE;
        }
    }
    protected override void MoveBehavior()
    {
        ani.Play("walk");
        base.MoveBehavior();
    }
}
