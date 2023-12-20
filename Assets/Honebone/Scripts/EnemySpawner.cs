using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    EnemyData test;
    [SerializeField]
    Transform baseTF;

    List<Enemy> spawnedEnemy;
    List<Transform> enemiesTF;
    [SerializeField]
    float spread;

    float timer;
    // Start is called before the first frame update
    void Start()
    {
        spawnedEnemy = new List<Enemy>(GetComponentsInChildren<Enemy>());//test
        enemiesTF = new List<Transform>();
        foreach(Enemy enemy in spawnedEnemy)//test
        {
            enemiesTF.Add(enemy.transform);
            enemy.SetBaseTF(baseTF);
        }
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.5f)
        {
            timer -= 0.5f;
            SpawnEnemy(test);
        }
    }

    public void SpawnEnemy(EnemyData data)
    {
        float radius = Random.Range(80f, 120f);
        float angle = Random.Range(-spread,spread);
        Vector2 spawnPos = angle.UnitCircle() * radius;
        var e = Instantiate(data.obj, spawnPos, Quaternion.identity, transform);
        e.GetComponent<Enemy>().SetBaseTF(baseTF);
        enemiesTF.Add(e.GetComponent<Transform>());
    }
    public void RemoveEnemyTF(Transform tf) { enemiesTF.Remove(tf); }

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
}
