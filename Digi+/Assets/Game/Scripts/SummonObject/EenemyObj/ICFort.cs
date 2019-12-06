using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICFort : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        HP = MaxHp;
        state = State.FIND;
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
    protected override void FindBehavior()
    {
        Target = FightSystem.fightSystem.findTarget(this);
        if (Vector3.Distance(this.transform.position, Target.transform.position) <= AttackDistance)
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
        if (Target && cooldown <= 0)
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
        FightSystem.fightSystem.BigEneDie();
        Destroy(this.gameObject);
    }
}
