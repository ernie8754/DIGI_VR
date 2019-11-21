using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonSystem : MonoBehaviour
{
    public EnergySystem energySystem;
    public enum State
    {
        IDLE,
        AIMMING,
        ENEMY_AIMMING,
        SUMMON
    }
    public State state;
    public Camera cam;
    public Ally summmm;
    public ParabolaSys paraSys;

    private Vector3 mouseTarget;
    private Ally SummonObj;
    private Enemy EneObj;
    private bool IsRightGridPress = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //print(Input.GetAxis("Oculus_CrossPlatform_PrimaryHandTrigger"));
        //print(Input.GetButton("Oculus_CrossPlatform_PrimaryIndexTrigger"));
        //print(OVRInput.Get(OVRInput.Button.One, controller));
        switch (state)
        {
            case State.IDLE:
                toAimming(summmm);
                break;
            case State.AIMMING:
                if (IsRightGridPress)
                {
                    IsRightGridPress = false;
                    //print("do");
                    if (MousePosition())
                    {
                        state = State.SUMMON;
                        summon(SummonObj, mouseTarget);
                    }
                    else
                    {
                        state = State.IDLE;
                    }
                }
                break;
            case State.SUMMON:
                break;
            case State.ENEMY_AIMMING:
                if (Input.GetMouseButtonDown(0))
                {
                    if (MousePosition())
                    {
                        state = State.SUMMON;
                        summon(EneObj, mouseTarget);
                    }
                    else
                    {
                        state = State.IDLE;
                    }
                }
                break;
            default:
                break;
        }
    }
    public void toAimming(Ally obj)
    {
        SummonObj = obj;
        state = State.AIMMING;
    }
    public void toAimming(Enemy obj)
    {
        EneObj = obj;
        state = State.ENEMY_AIMMING;
    }
    public void rightGridPress()
    {
        
        IsRightGridPress = true;
    }
    #region Summon
    public void summon(Ally obj, Vector3 pos)
    {
        if (obj.consumeEnergy <= energySystem.playerEnergy)
        {
            var newObj = Instantiate(obj, new Vector3(pos.x, 5, pos.z), Quaternion.Euler(0,0,0));
            FightSystem.fightSystem.AddObj(newObj);
            energySystem.consume(obj.consumeEnergy);
            state = State.IDLE;
        }
    }
    public void summon(Enemy obj, Vector3 pos)
    {
        if (obj.consumeEnergy <= energySystem.playerEnergy)
        {
            var newObj = Instantiate(obj, pos, Quaternion.Euler(0, 0, 0));
            FightSystem.fightSystem.AddObj(newObj);
            state = State.IDLE;
        }
    }
    #endregion
    #region mouse position
    public bool MousePosition()
    {
        mouseTarget = paraSys.hitPointCompute();
        return true;
    }
    #endregion
}
