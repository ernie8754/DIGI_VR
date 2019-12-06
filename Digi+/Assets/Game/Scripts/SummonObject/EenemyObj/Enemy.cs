using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : SummonObj
{
    protected override void die()
    {
        state = State.DIE;
        IsDead = true;
        if (ani)
        {
            ani.Play("death");
            
            if (ani["death"].normalizedTime >= 0.9f)
            {
                //Debug.Log("di");
                FightSystem.fightSystem.deleteObj(this);
            }
        }
        else
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
