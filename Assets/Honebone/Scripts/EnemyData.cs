using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "E_", menuName = "ScriptableObjects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public GameObject obj;

    [TextArea(3, 5)]
    public string enemyName;
    public int maxHP;
    public int DMG;
    public float speed;
    public float range;
    public float attackSpeed;
    [Space(25)]
    public bool charger;
    public bool explode;
    [Space(25)]
    [Header("弾を飛ばして遠距離攻撃をするか")] public bool rangedAttack;

    public GameObject projectile;

    public AudioClip SE_Fire;

    public float projectileSpeed = 50f;
    public float projectileDuration = 3f;
}