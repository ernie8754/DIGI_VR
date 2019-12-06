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
    public bool IsFight = false;
    // Start is called before the first frame update
    void Start()
    {
        fightSystem = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsFight)
        {
            return;
        }
        if (!summonSystem.EneWait)
        {
            StartCoroutine(summonSystem.summonEneObj(SummonEneList[Random.Range(0, SummonEneList.Count)], sumPlace[Random.Range(0, sumPlace.Count)].position));
        }
    }
    #region Find Target
    public Enemy findTarget(Ally Obj)
    {
        float minDis = Mathf.Infinity;
        Enemy nearest = null;
        foreach (var enemy in Enemies)
        {
            float dis = Vector3.Distance(Obj.transform.position, enemy.transform.position);
            if (dis < minDis)
            {
                minDis = dis;
                nearest = enemy;
            }
        }
        return nearest;
    }
    public Ally findTarget(Enemy Obj)
    {
        float minDis = Mathf.Infinity;
        Ally nearest = null;
        foreach (var ally in Allies)
        {
            float dis = Vector3.Distance(Obj.transform.position, ally.transform.position);
            if (dis < minDis)
            {
                minDis = dis;
                nearest = ally;
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
