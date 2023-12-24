using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "T_", menuName = "ScriptableObjects/TurretData")]
public class TurretData : ScriptableObject
{
    public GameObject obj;
    public string turretName;
    public Sprite turretImage;

    public int maxHP;
    public int DMG;
    public ItemData ammoData;
    public int maxAmmo;
    public int maxBattery;
    public float range;
    [Header("毎秒何回攻撃するか")] public float attackSpeed;

    public GameObject projectile;

    public AudioClip SE_GengeratePjtor;
    public AudioClip SE_Fire;

    [Header("ターゲットを追尾するか/方向転換速度")] public float followTargetSpeed;
    [Header("現在のプレイヤーの位置を追尾するか falseなら発射時の場所へ")] public bool followCurrentTarget;

    [Header("一回の発射で射出する弾数")] public int pellets = 1;
    [Header("ランダムな方向に発射するか")] public bool fireRandomly;
    [Header("+-(spread/2)°のブレが生じる")] public float spread;
    [Header("spread上に等間隔に発射するか")] public bool equidistant;

    //[Header("発射回数")] public float fireRounds = 1;
    //[Header("発射回数が2以上の時に参照 1発発射するごとのインターバル[s] 0なら同時発射")] public float fireRate;

    [Header("min～maxの間でランダムに決まる")]
    public float projectileSpeed_min = 100f;
    public float projectileSpeed_max = 100f;

    public bool infinitePenetration;
    public int penetration;
    public float projectileDuration = 1f;
}
