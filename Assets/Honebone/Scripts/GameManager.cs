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
    LogUI logUI;
    Base @base;
    ScoreManager scoreManager;
    PauseUI pauseUI;

    int waveCount;
    WaveData curretWave;
    float waveScoref;
    // Start is called before the first frame update
    void Start()
    {
        @base = FindObjectOfType<Base>();
        logUI = FindObjectOfType<LogUI>();
        scoreManager = FindObjectOfType<ScoreManager>();
        pauseUI = FindObjectOfType<PauseUI>();
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
        logUI.AddLog("ウェーブクリア!");

        int baseScore = curretWave.GetBaseScore();
        float bonus = 0;
        scoreManager.AddScore(baseScore, "ウェーブクリア",true);

        if (@base.GetTurretsAmount() > 0)
        {
            bonus = baseScore * 0.05f * @base.GetTurretsAmount();
            scoreManager.AddScore(bonus, string.Format("タレット生存ボーナス {0}％", @base.GetTurretsAmount() * 5),false);
        }

        if (!pauseUI.CheckPaused())
        {
            scoreManager.AddScore(baseScore * 0.5f, "ノンストップボーナス 50％", false);
        }

        foreach (TurretReward reward in curretWave.turretRewards)
        {
            @base.DeployTurret(reward.turret, reward.pos);
        }
        NextWave(waves[waveCount]);
    }
    public void NextWave(WaveData w)
    {
        waveScoref = 0;
        curretWave = w;
        waveCount++;
        waveText.SetText(waveCount);
    }
    public void StartWave()
    {
        pauseUI.RestFrag();
        enemySpawner.StartWave(curretWave);
    }
    public void TogglePause()
    {
        if (Time.timeScale == 1) { Time.timeScale = 0; }
        else { Time.timeScale = 1; }
    }

    public int GetBaseScore() { return curretWave.GetBaseScore(); }
}
