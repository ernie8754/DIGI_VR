using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : Ally
{
    // Start is called before the first frame update
    void Start()
    {
        HP = MaxHp;
        state = State.FIND;
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
    protected override void FindBehavior()
    {
        Target = FightSystem.fightSystem.findTarget(this);
        if(Target && Vector3.Distance(this.transform.position, Target.transform.position) <= AttackDistance)
        {
            state = State.ATTACK;
        }
    }
    protected override void AttackBehavior()
    {
        if (!Target || Vector3.Distance(this.transform.position, Target.transform.position) > AttackDistance)
        {
            state = State.FIND;
        }
        if (Target && cooldown <= 0 )
        {
            Target.hurt(ATK);
            cooldown = 1.5f;
        }
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
    }
    protected override void die()
    {
        GameManager.gameManager.PlayerLose();
    }
}
