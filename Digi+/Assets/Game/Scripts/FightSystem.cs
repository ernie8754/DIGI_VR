using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightSystem : MonoBehaviour
{
    public static FightSystem fightSystem;

    public List<Ally> Allies = new List<Ally>();
    public List<Enemy> Enemies = new List<Enemy>();
    public List<Enemy> SummonEneList = new List<Enemy>();
    public SummonSystem summonSystem;
    public List<Transform> sumPlace = new List<Transform>();
    //public bool IsFight = false;
    // Start is called before the first frame update
    void Start()
    {
        fightSystem = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void fightUpadate()
    {
        if (!summonSystem.EneWait)
        {
            StartCoroutine(summonSystem.summonEneObj(SummonEneList[Random.Range(0, SummonEneList.Count)], sumPlace[Random.Range(0, sumPlace.Count)].position));
        }
        summonSystem.summonUpdate();
    }
    public void endFight()
    {
        while (Allies.Count > 0)
        {
            Ally tmp = Allies[0];
            Allies.RemoveAt(0);
            Destroy(tmp.gameObject);
        }
        while (Enemies.Count > 0)
        {
            Enemy tmp = Enemies[0];
            Enemies.RemoveAt(0);
            Destroy(tmp.gameObject);
        }
    }
    #region Find Target
    public Enemy findTarget(Ally Obj)
    {
        float minDis = Mathf.Infinity;
        Enemy nearest = null;
        for(int i = 0; i < Enemies.Count; i++)
        {
            float dis = Vector3.Distance(Obj.transform.position, Enemies[i].transform.position);
            if (dis < minDis)
            {
                minDis = dis;
                nearest = Enemies[i];
            }
        }
        return nearest;
    }
    public Ally findTarget(Enemy Obj)
    {
        float minDis = Mathf.Infinity;
        Ally nearest = null;
        for(int i = 0; i < Allies.Count; i++)
        {
            float dis = Vector3.Distance(Obj.transform.position, Allies[i].transform.position);
            if (dis < minDis)
            {
                minDis = dis;
                nearest = Allies[i];
            }
        }
        return nearest;
    }
    #endregion
    #region Add Obj
    public void AddObj(Ally obj)
    {
        Allies.Add(obj);
    }
    public void AddObj(Enemy obj)
    {
        Enemies.Add(obj);
    }
    #endregion
    #region Delete Obj
    public void deleteObj(Ally Obj)
    {
        Allies.Remove(Obj);
        Destroy(Obj.gameObject);
        //Debug.Log("di");
    }
    public void deleteObj(Enemy Obj)
    {
        Enemies.Remove(Obj);
        Destroy(Obj.gameObject);
        //Debug.Log("di");
    }
    #endregion
    #region AOE
    public void AOE_Attack(Ally Obj, int distance, int ATK)
    {
        for (int i = 0; i < Enemies.Count; i++)
        {
            Enemy enemy = Enemies[i];
            float dis = Vector2.Distance(Obj.transform.position, enemy.transform.position);
            if (dis <= distance)
            {
                enemy.hurt(ATK);
            }
        }
    }
    public void AOE_Attack(Enemy Obj, int distance, int ATK)
    {
        for (int i = 0; i < Allies.Count; i++)
        {
            Ally ally = Allies[i];
            float dis = Vector2.Distance(Obj.transform.position, ally.transform.position);
            if (dis <= distance)
            {
                ally.hurt(ATK);
            }
        }
    }
    #endregion
    [SerializeField]private int BigEneNum;
    public void BigEneDie()
    {
        BigEneNum--;
        if (BigEneNum == 0)
        {
            GameManager.gameManager.PlayerWin();
        }
    }
}
