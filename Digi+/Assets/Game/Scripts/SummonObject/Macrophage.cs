using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Macrophage : Ally
{
    public Animator ani;
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
                if (Target == null)
                {
                    Target = FightSystem.fightSystem.findTarget(this);
                }
                else
                {
                    move();
                }
                break;
            case State.MOVE:
                if (Vector3.Distance(this.transform.position, Target.transform.position) <= AttackDistance)
                {
                    state = State.ATTACK;
                    agent.isStopped = true;
                }
                break;
            case State.ATTACK:
                AttackBehavior();
                break;
            case State.IDLE:
                break;
            default:
                break;
        }
    }
    public override void BirthBehavior()
    {
         state = State.FIND;
    }
    public override void move()
    {
        agent.SetDestination(Target.transform.position);
        state = State.MOVE;
        ani.Play("walk");
    }
    public override void AttackBehavior()
    {
        ani.Play("attach");
    }
}
