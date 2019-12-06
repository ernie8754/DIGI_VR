using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ally : SummonObj
{
    protected override void die()
    {
        state = State.DIE;
        IsDead = true;
        ani.Play("death");
        if (ani["death"].normalizedTime >= 0.9f)
        {
            FightSystem.fightSystem.deleteObj(this);
        }
    }
    protected override void FindBehavior()
    {
        Target = FightSystem.fightSystem.findTarget(this);
        if(Target)
        {
            move();
        }
    }
}
