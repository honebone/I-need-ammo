using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "E_", menuName = "ScriptableObjects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public int maxHP;
    public int DMG;
    public float speed;
    public float range;
    public float fireRate;
}