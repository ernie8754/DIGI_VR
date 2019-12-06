using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FVObj : Enemy      //流感病毒
{
    [SerializeField] private ParticleSystem particle;
    void Start()
    {
        HP = MaxHp;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = this.moveSpeed;
    }

    // Update is called once per frame
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
            ani.Play("walk");
            state = State.MOVE;
        }
    }
    protected override void MoveBehavior()
    {
        base.MoveBehavior();
        ani.Play("walk");
    }
    protected override void AttackBehavior()
    {
        if (!Target || Vector3.Distance(this.transform.position, Target.transform.position) > AttackDistance)
        {
            state = State.FIND;
            return;
        }
        if (Target && cooldown <= 0 )
        {
            FightSystem.fightSystem.AOE_Attack(this, 5, ATK);
            cooldown = 0.5f;
        }
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        //base.AttackBehavior();
    }
}
