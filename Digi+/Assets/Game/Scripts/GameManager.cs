using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public Transform testCube;
    public ParabolaSys para;
    public static GameManager gameManager;
    public enum state
    {
        WAIT,
        INITIAL,
        FIGHT,
        END
    }
    public state State;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayerLose()
    {

    }
    public void PlayerWin()
    {

    }
}
