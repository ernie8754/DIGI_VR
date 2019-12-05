using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SPObj : Enemy      //肺炎鏈球菌
{
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = this.moveSpeed;
    }

    // Update is called once per frame
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
            case State.DIE:
                die();
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
            ani.Play("walk1");
            state = State.MOVE;
        }
    }
    protected override void MoveBehavior()
    {
        base.MoveBehavior();
        if (ani.IsPlaying("walk1") && ani["walk1"].normalizedTime >= 1.0f)
        {
            ani.Play("walk2");
            agent.isStopped = true;
        }
        else if (ani.IsPlaying("walk2") && ani["walk2"].normalizedTime >= 1.0f)
        {
            ani.Play("walk1");
            agent.isStopped = false;
        }
    }
}
