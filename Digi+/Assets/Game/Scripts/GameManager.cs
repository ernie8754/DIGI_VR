using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform testCube;
    [SerializeField] private ParabolaSys para;
    public static GameManager gameManager;
    [SerializeField] private Canvas OpenCan;
    [SerializeField] private Canvas ButtonCan;
    [SerializeField] private Canvas WinCan;
    [SerializeField] private Canvas FailCan;
    public enum state
    {
        WAIT,
        OPENING,
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
    private float endWaitTime = 0;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.LoadScene(0);
        }
        switch (State)
        {
            case state.OPENING:
                break;
            case state.WAIT:
                break;
            case state.FIGHT:
                FightSystem.fightSystem.fightUpadate();
                break;
            case state.END:
                endWaitTime -= Time.deltaTime;
                if (endWaitTime <= 0)
                {
                    //State = state.OPENING;
                    //FailCan.gameObject.SetActive(false);
                    //WinCan.gameObject.SetActive(false);
                    //OpenCan.gameObject.SetActive(true);
                    SceneManager.LoadScene(0);
                }
                break;
            default:
                break;
        }
    }
    public void GameStart()
    {
        OpenCan.gameObject.SetActive(false);
        ButtonCan.gameObject.SetActive(true);
        State = state.FIGHT;
    }
    public void PlayerLose()
    {
        FailCan.gameObject.SetActive(true);
        ButtonCan.gameObject.SetActive(false);
        FightSystem.fightSystem.endFight();
        endWaitTime = 5;
        State = state.END;
    }
    public void PlayerWin()
    {
        WinCan.gameObject.SetActive(true);
        ButtonCan.gameObject.SetActive(false);
        FightSystem.fightSystem.endFight();
        endWaitTime = 5;
        State = state.END;
    }
}
