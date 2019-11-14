using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Macrophage : Ally
{
    public Animation ani;
    public override void BirthBehavior()
    {
        if (!ani.IsPlaying("birth"))
        {
            state = State.FIND;
        }
    }
    public override void move()
    {
        base.move();
        ani.Play("go ahead");
    }
    public override void AttackBehavior()
    {
        ani.Play("attach");
    }
}
