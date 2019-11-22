using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : SummonObj
{
    protected override void die()
    {
        ani.Play("death");
        if (ani["death"].normalizedTime >= 1.0f)
        {
            FightSystem.fightSystem.deleteObj(this);
        }
    }
    protected override void FindBehavior()
    {
        if (Target == null)
        {
            Target = FightSystem.fightSystem.findTarget(this);
        }
        else
        {
            move();
        }
    }
}
