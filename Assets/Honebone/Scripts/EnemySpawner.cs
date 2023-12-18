using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    Transform baseTF;

    List<Enemy> spawnedEnemy;
    List<Transform> enemiesTF;
    // Start is called before the first frame update
    void Start()
    {
        spawnedEnemy = new List<Enemy>(GetComponentsInChildren<Enemy>());//test
        enemiesTF = new List<Transform>();
        foreach(Enemy enemy in spawnedEnemy)
        {
            enemiesTF.Add(enemy.transform);
            enemy.SetBaseTF(baseTF);
        }
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
