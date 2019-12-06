using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergySystem : MonoBehaviour
{
    public enum state
    {
        WAIT,
        FIGHT,
        END
    }
    public state State;
    private int _playerEnergy;
    private int _enemyEnergy;
    [SerializeField] private Image EnergyBar;

    public int playerEnergy { get { return _playerEnergy; } }
    public int enemyEnergy { get { return _enemyEnergy; } }
    public int SecPerEnergy;
    public int maxEnergy;
    //public Text EnergyText;
    // Start is called before the first frame update
    void Start()
    {
        _playerEnergy = 0;
        _enemyEnergy = 0;
    }
    private float timeCounter = 0;
    // Update is called once per frame
    void Update()
    {
        EnergyBar.fillAmount = playerEnergy * 1.0f / maxEnergy;
        timeCounter += Time.deltaTime;
        switch (State)
        {
            case state.FIGHT:
                if (timeCounter > SecPerEnergy)
                {
                    playerEnergyAdd();
                    EnemyEnergyAdd();
                    timeCounter -= SecPerEnergy;
                }
                //EnergyText.text = _playerEnergy.ToString();
                break;
            case state.END:
                break;
            case state.WAIT:
                break;
            default:
                break;
        }
    }
    private void playerEnergyAdd()
    {
        if (_playerEnergy < maxEnergy)
        {
            _playerEnergy += 1;
        }
    }
    private void EnemyEnergyAdd()
    {
        if (enemyEnergy < maxEnergy)
            _enemyEnergy += 1;
    }
    public void consume(int num)
    {
        _playerEnergy -= num;
    }
    public void EneConsume(int num)
    {
        _enemyEnergy -= num;
    }
}
