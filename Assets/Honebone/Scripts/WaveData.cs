using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "W_", menuName = "ScriptableObjects/WaveData")]
public class WaveData : ScriptableObject
{
    public float spread;
    public float radius;
    public int spawnAmount = 1;
    public float spawnSpeed = 1;
    public List<GameManager.EnemySet> enemySets;
    public List<GameManager.TurretReward> turretRewards;
}
