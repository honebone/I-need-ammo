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
    ItemData[] upgradeDataBase;

    [SerializeField]
    EnemySpawner enemySpawner;
    [SerializeField]
    WaveText waveText;
    [SerializeField]
    WaveClearText clearText;
    LogUI logUI;
    Base @base;
    ScoreManager scoreManager;
    PauseUI pauseUI;

    int waveCount;
    WaveData curretWave;
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
        NextWave();
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

        string r = "";
        foreach (TurretReward reward in curretWave.turretRewards)
        {
            @base.DeployTurret(reward.turret, reward.pos);
            r += string.Format("タレットを追加：{0}\n", reward.turret.turretName);
        }
        foreach (DroneData reward in curretWave.droneRewards)
        {
            @base.AddDrone(reward);
            r += string.Format("ドローンを追加：{0}\n", reward.droneName);
        }
        for (int i = 0; i < curretWave.upgradeRewards; i++)
        {
            ItemData reward = GetRandomUpgrade();
            @base.AddItem(reward);
            r += string.Format("アップグレードモジュールを入手：{0}\n", reward.itemName);
        }
        clearText.Clear(r);
    }
    public void NextWave()
    {
        curretWave = waves[waveCount];
        waveCount++;
        waveText.SetText(waveCount);
        foreach(Turret.TurretStatus turretStatus in @base.GetTurretsStatus()) { turretStatus.ResetCounters(); }
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
    public ItemData GetRandomUpgrade() { return upgradeDataBase[Random.Range(0, upgradeDataBase.Length)]; } 
}
