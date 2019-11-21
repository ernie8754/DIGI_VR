using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Macrophage : Ally
{
    public Animation ani;
    private NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = this.moveSpeed;
        //agent.Warp(new Vector3(-0.84f, 10,0));
    }
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
    protected override void BirthBehavior()
    {
         state = State.FIND;
    }
    protected override void move()
    {
        base.move();
        ani.Play("walk");
    }
    protected override void AttackBehavior()
    {
        ani.Play("attack");
        Target.hurt(ATK);
    }
}
