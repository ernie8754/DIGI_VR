using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public Transform testCube;
    public ParabolaSys para;
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
        para.paraStart(testCube);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
