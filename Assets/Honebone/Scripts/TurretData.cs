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
    [Header("���b����U�����邩")] public float attackSpeed;

    public GameObject projectile;

    public AudioClip SE_GengeratePjtor;
    public AudioClip SE_Fire;

    [Header("�^�[�Q�b�g��ǔ����邩/�����]�����x")] public float followTargetSpeed;
    [Header("���݂̃v���C���[�̈ʒu��ǔ����邩 false�Ȃ甭�ˎ��̏ꏊ��")] public bool followCurrentTarget;

    [Header("���̔��˂Ŏˏo����e��")] public int pellets = 1;
    [Header("�����_���ȕ����ɔ��˂��邩")] public bool fireRandomly;
    [Header("+-(spread/2)���̃u����������")] public float spread;
    [Header("spread��ɓ��Ԋu�ɔ��˂��邩")] public bool equidistant;

    //[Header("���ˉ�")] public float fireRounds = 1;
    //[Header("���ˉ񐔂�2�ȏ�̎��ɎQ�� 1�����˂��邲�Ƃ̃C���^�[�o��[s] 0�Ȃ瓯������")] public float fireRate;

    [Header("min�`max�̊ԂŃ����_���Ɍ��܂�")]
    public float projectileSpeed_min = 100f;
    public float projectileSpeed_max = 100f;

    public bool infinitePenetration;
    public int penetration;
    public float projectileDuration = 1f;
}
