using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class EnemySet
    {
        public EnemyData enemy;
        public int amount;
    }
    [System.Serializable]
    public class TurretReward
    {
        public TurretData turret;
        public Vector2 pos;
    }

    [SerializeField]
    WaveData[] waves;

    [SerializeField]
    EnemySpawner enemySpawner;
    [SerializeField]
    WaveText waveText;
    [SerializeField]
    Transform tuuretsP;
    [SerializeField]
    LogUI logUI;
    Base @base;

    int waveCount;
    WaveData curretWave;
    // Start is called before the first frame update
    void Start()
    {
        @base = FindObjectOfType<Base>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        NextWave(waves[0]);
    }
    public void EndWave()
    {
        foreach (TurretReward reward in curretWave.turretRewards)
        {
            @base.DeployTurret(reward.turret, reward.pos);
        }
        NextWave(waves[waveCount]);
    }
    public void NextWave(WaveData w)
    {
        curretWave = w;
        waveCount++;
        waveText.SetText(waveCount);
    }
    public void StartWave()
    {
        enemySpawner.StartWave(curretWave);
    }
    public void TogglePause()
    {
        if (Time.timeScale == 1) { Time.timeScale = 0; }
        else { Time.timeScale = 1; }
    }
}
