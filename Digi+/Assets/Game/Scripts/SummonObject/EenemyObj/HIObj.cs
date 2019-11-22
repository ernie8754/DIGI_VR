using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HIObj : Enemy      //結核桿菌
{
    // Start is called before the first frame update
    void Start()
    {
        
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
}
