using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    EnemyData test;
    [SerializeField]
    Transform baseTF;
    [SerializeField]
    InfoUI infoUI;
    [SerializeField]
    Text remainingText;
    GameManager gameManager;
    ScoreManager scoreManager;
    SoundManager soundManager;

    List<Enemy> spawnedEnemy;
    List<Transform> enemiesTF;

    float timer;

    bool waving;
    WaveData wave;
    List<GameManager.EnemySet> enemies;
    List<int> remaining;
    // Start is called before the first frame update
    void Start()
    {
        gameManager=FindObjectOfType<GameManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
        soundManager = FindObjectOfType<SoundManager>();
        spawnedEnemy = new List<Enemy>(GetComponentsInChildren<Enemy>());//test
        enemiesTF = new List<Transform>();
        //foreach(Enemy enemy in spawnedEnemy)//test
        //{
        //    enemiesTF.Add(enemy.transform);
        //    enemy.Init(baseTF,infoUI);
        //}
    }

    int index;
    private void Update()
    {
        if (waving)
        {
            timer += Time.deltaTime;
            if (timer >= 1f / wave.spawnSpeed)
            {
                timer -= 1f / wave.spawnSpeed;

                for (int i = 0; i < wave.spawnAmount; i++)
                {
                    index = remaining.ChoiceWithWeight();
                    SpawnEnemy(enemies[index].enemy);
                    if (remaining[index] > 0) { remaining[index]--; }

                    bool f = true;
                    foreach (int j in remaining)
                    {
                        if (j > 0) { f = false; }
                    }
                    if (f)
                    {
                        waving = false;
                        break;
                    }
                }
            }
        }
    }
    public void StartWave(WaveData w)
    {
        wave = w;
        enemies = new List<GameManager.EnemySet>(wave.enemySets);
        remaining = new List<int>();
        foreach (GameManager.EnemySet enemy in enemies) { remaining.Add(enemy.amount); }
        waving = true;
        SetRemainingText();
    }

    public void SpawnEnemy(EnemyData data)
    {
        float radius = wave.radius;
        if (!wave.solidRadius) { radius *= Random.Range(0.8f, 1.2f); }
        float angle = Random.Range(-wave.spread / 2f, wave.spread / 2f);
        Vector2 spawnPos = angle.UnitCircle() * radius;
        var e = Instantiate(data.obj, spawnPos, Quaternion.identity, transform);
        e.GetComponent<Enemy>().Init(baseTF,infoUI, data,scoreManager,soundManager);
        enemiesTF.Add(e.GetComponent<Transform>());
    }
    public void RemoveEnemyTF(Transform tf)
    {
        enemiesTF.Remove(tf);
        SetRemainingText();
        if (enemiesTF.Count == 0&&!waving)
        {
            gameManager.EndWave();
        }
    }

    public Enemy GetNearestEnemy(Vector2 pos,float range)
    {
        if (enemiesTF.Count == 0)
        {
            return null;
        }
        Transform nearestEnemy = null;

        float nearestDist = range;
        foreach (Transform t in enemiesTF)
        {
            float dist = Vector2.Distance(pos, (Vector2)t.position);
            if (dist <= nearestDist)
            {
                nearestEnemy = t;
                nearestDist = dist;
            }
        }

        if(nearestEnemy != null) { return nearestEnemy.GetComponent<Enemy>(); }
        else { return null; }
    }
    public void SetRemainingText()
    {
        int remain = enemiesTF.Count;
        foreach(int i in remaining) { remain += i; }
        remainingText.text = string.Format("Žc‚è{0}‘Ì", remain);
    }
}
