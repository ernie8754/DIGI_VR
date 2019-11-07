using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightSystem : MonoBehaviour
{
    public static FightSystem fightSystem;

    public List<Ally> Allies = new List<Ally>();
    public List<Enemy> Enemies = new List<Enemy>();
    public SummonSystem summonSystem;
    // Start is called before the first frame update
    void Start()
    {
        fightSystem = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #region Find Target
    public Enemy findTarget(Ally Obj)
    {
        float minDis = Mathf.Infinity;
        Enemy nearest = null;
        foreach(var enemy in Enemies)
        {
            float dis=Vector2.Distance(Obj.transform.position, enemy.transform.position);
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
            float dis = Vector2.Distance(Obj.transform.position, ally.transform.position);
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
}
