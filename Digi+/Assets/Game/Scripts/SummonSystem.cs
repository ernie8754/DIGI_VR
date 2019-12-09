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
        energySystem.EnergyStart();
    }

    // Update is called once per frame
    void Update()
    {
        //print(Input.GetAxis("Oculus_CrossPlatform_PrimaryHandTrigger"));
        //print(Input.GetButton("Oculus_CrossPlatform_PrimaryIndexTrigger"));
        //print(OVRInput.Get(OVRInput.Button.One, controller));
        
    }
    public void summonUpdate()
    {
        switch (state)
        {
            case State.IDLE:
                if (IsRightGridPress)
                {
                    IsRightGridPress = false;
                    // Debug.Log(IsRightGridPress);
                }
                //toAimming(summmm);
                break;
            case State.AIMMING:
                if (IsRightGridPress && SummonObj)
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
            default:
                break;
        }
        energySystem.energyUpdate();
    }
    public void toAimming(Ally obj, Transform ConTrans)
    {
        if (obj.consumeEnergy <= energySystem.playerEnergy)
        {
            paraSys.paraStart(ConTrans);
            SummonObj = obj;
            state = State.AIMMING;
        }
    }
    public void rightGridPress()
    {
        //print("d");
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
            paraSys.paraShut();
            state = State.IDLE;
        }
    }
    public bool EneWait=false;
    public IEnumerator summonEneObj(Enemy obj, Vector3 pos)
    {
        EneWait = true;
        yield return new WaitUntil(() => obj.consumeEnergy <= energySystem.enemyEnergy);
        var newObj = Instantiate(obj, pos, Quaternion.Euler(0, 180, 0));
        FightSystem.fightSystem.AddObj(newObj);
        energySystem.EneConsume(obj.consumeEnergy);
        EneWait = false;
        //state = State.IDLE;
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
