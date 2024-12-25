using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using unityroom.Api;

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
    WaveData endless;
    [SerializeField]
    ItemData[] upgradeDataBase;

    [SerializeField]
    EnemySpawner enemySpawner;
    [SerializeField]
    WaveText waveText;
    [SerializeField]
    WaveClearText clearText;
    [SerializeField]
    Image blackout;

    [SerializeField]
    GameObject gameoverUI;
    [SerializeField]
    Text resultText;
    LogUI logUI;
    Base @base;
    ScoreManager scoreManager;
    PauseUI pauseUI;
    SceneLoadManager sceneLoadManager;
    TutorialUI tutorialUI;
    SoundManager soundManager;

    int killCount;
    int suppliedCount;

    int waveCount;
    WaveData curretWave;
    float mul = 0;

    void Start()
    {
        @base = FindObjectOfType<Base>();
        logUI = FindObjectOfType<LogUI>();
        scoreManager = FindObjectOfType<ScoreManager>();
        pauseUI = FindObjectOfType<PauseUI>();
        sceneLoadManager= FindObjectOfType<SceneLoadManager>();
        tutorialUI= FindObjectOfType<TutorialUI>();
        soundManager= FindObjectOfType<SoundManager>();

        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        var wait = new WaitForSeconds(0.05f);
        Color c = Color.black;
        blackout.color = c;
        for (int i = 0; i < 10; i++)
        {
            yield return wait;
            c.a -= 0.1f;
            blackout.color = c;
        }
        yield return new WaitForSeconds(1f);
        StartGame();
    }
    public void EndGame()
    {
        StartCoroutine(FadeOut());
    }
    IEnumerator FadeOut()
    {
        var wait = new WaitForSecondsRealtime(0.05f);
        Color c = Color.clear;
        blackout.color = c;
        for (int i = 0; i < 10; i++)
        {
            yield return wait;
            c.a += 0.1f;
            blackout.color = c;
        }
        yield return new WaitForSecondsRealtime(1f);
        sceneLoadManager.EndGame();
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
        logUI.AddLog("�E�F�[�u�N���A!");

        int baseScore = curretWave.GetBaseScore();
        float bonus = 0;
        scoreManager.AddScore(baseScore, "�E�F�[�u�N���A",true);

        if (@base.GetTurretsAmount() > 0)
        {
            bonus = baseScore * 0.05f * @base.GetTurretsAmount();
            scoreManager.AddScore(bonus, string.Format("�^���b�g�����{�[�i�X {0}��", @base.GetTurretsAmount() * 5),false);
        }

        if (!pauseUI.CheckPaused())
        {
            scoreManager.AddScore(baseScore * 0.5f, "�m���X�g�b�v�{�[�i�X 50��", false);
        }

        string r = "";
        foreach (TurretReward reward in curretWave.turretRewards)
        {
            @base.DeployTurret(reward.turret, reward.pos);
            r += string.Format("�^���b�g��ǉ��F{0}\n", reward.turret.turretName);
        }
        foreach (DroneData reward in curretWave.droneRewards)
        {
            @base.AddDrone(reward);
            r += string.Format("�h���[����ǉ��F{0}\n", reward.droneName);
        }
        for (int i = 0; i < curretWave.upgradeRewards; i++)
        {
            ItemData reward = GetRandomUpgrade();
            @base.AddItem(reward);
            r += string.Format("�A�b�v�O���[�h���W���[�������F{0}\n", reward.itemName);
        }
        clearText.Clear(r);
    }
    public void NextWave()
    {
        if (waveCount < 15)//15
        {
            curretWave = waves[waveCount];
        }
        else
        {
            curretWave = endless;
        }
        waveCount++;

        waveText.SetText(waveCount);
        foreach(Turret.TurretStatus turretStatus in @base.GetTurretsStatus()) { turretStatus.ResetCounters(); }
    }
    public void StartWave()
    {
        tutorialUI.DisplayTutorial("GameStart");

        if (waveCount==2)
        {
            tutorialUI.DisplayTutorial("Drone");
        }
        if (waveCount==3)
        {
            tutorialUI.DisplayTutorial("Upgrade");
        }
        pauseUI.RestFrag();
        if (waveCount >= 16) { mul += 0.1f; }//16
        enemySpawner.StartWave(curretWave,mul);
    }
    
    public void GameOver()
    {
        FindObjectOfType<CameraController>().MoveTo(@base.transform.position);
        pauseUI.Gameover();
        soundManager.Gameover();
        string result = string.Format("��{0}�E�F�[�u�܂Ő������т�\n",waveCount);
        result += string.Format("�ŏISCORE�F{0}\n", scoreManager.score);
        result += string.Format("�L�����F{0}\n",killCount);
        result += string.Format("�⋋�����A�C�e�����F{0}\n", suppliedCount);

        UnityroomApiClient.Instance.SendScore(1, scoreManager.score, ScoreboardWriteMode.HighScoreDesc);

        gameoverUI.SetActive(true);
        resultText.text = result;
    }
    public void AddKillCount() { killCount++; }
    public void AddSuppliedCount(int amount) { suppliedCount += amount; }

    public int GetBaseScore() { return curretWave.GetBaseScore(); }
    public ItemData GetRandomUpgrade() { return upgradeDataBase[Random.Range(0, upgradeDataBase.Length)]; } 
}
