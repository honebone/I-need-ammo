using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public class TurretStatus
    {
        public TurretData turretData;

        public float maxHP_mul;
        public int maxHP;

        public float DMG_mul;
        public int DMG;

        public float maxAmmo_mul;
        public int maxAmmo;

        public float maxBattery_mul;
        public int maxBattery;

        public float range_mul;
        public float range;

        public float attackSpeed_mul;
        public float attackSpeed;

        public bool dead;
        public int HP;
        public int shield;
        public int ammo;
        public int battery;

        public void Init(TurretData data)
        {
            turretData = data;
            maxHP = data.maxHP;
            DMG = data.DMG;
            maxAmmo = data.maxAmmo;
            maxBattery = data.maxBattery;
            range = data.range;
            attackSpeed = data.attackSpeed;

            HP = maxHP;
        }
    }
    [SerializeField]
    TurretData test;
    TurretStatus status;
    Enemy target;

    EnemySpawner enemySpawner;

    Coroutine interval;
    public void Init(TurretData data)
    {
        status = new TurretStatus();
        status.Init(data);
    }
    private void Start()
    {
        Init(test);
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }
    void Update()
    {
        if (target == null || !target.CheckAlive()) { SetTarget(); }
        Vector2 origin = transform.position;//origin�Ɏ��g�̍��W����
        Vector2 direction = new Vector2(1, 0);//direction�Ɏ��g����v���C���[�Ɍ������P�ʃx�N�g������
        Debug.DrawRay(origin, direction * status.range, Color.red);//Ray�Ɠ����n�_�A�����A�����̐Ԃ�����1�t���[���`��
    }
    //void OnDrawGizmosSelected()
    //{
    //    // Display the explosion radius when selected
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, status.range);
    //}
    public void SetTarget()
    {
        target = enemySpawner.GetNearestEnemy(transform.position, status.range);
        if (target != null)
        {
            if (interval != null) { StopCoroutine(interval); }
            interval = StartCoroutine(Interval());
        }
    }

    IEnumerator Interval()
    {
        yield return new WaitForSeconds(1f/ status.attackSpeed);
        if (CheckAlive())
        {
            Attack();
            interval = StartCoroutine(Interval());
        }
    }
    public virtual void Attack()
    {
        if (target != null && target.CheckAlive())
        {
            StartCoroutine(Fire());
        }
    }
    IEnumerator Fire()
    {
        var wait = new WaitForSeconds(status.turretData.fireRate);
        //if (status.SE_GengeratePjtor != null) { soundManager.PlayAudio(status.SE_GengeratePjtor); }
        //if (data.fireRate == 0 && status.SE_Fire != null) { soundManager.PlayAudio(status.SE_Fire); }

        for (int i = 0; i < status.turretData.fireRounds; i++)
        {
            FireProjectile();
            if (status.turretData.fireRate > 0) { yield return wait; }
        }
    }
    public void FireProjectile()
    {
        Vector3 dir = new Vector3();
        dir = (target.transform.position - transform.position).normalized;
        Quaternion quaternion = Quaternion.FromToRotation(Vector3.up, dir);
        float delta = status.turretData.spread / -2f; ;
        for (int i = 0; i < status.turretData.pellets; i++)
        {
            float spread = 0f;
            if (status.turretData.spread > 0 && !status.turretData.equidistant) { spread = Random.Range(status.turretData.spread / -2f, status.turretData.spread / 2f); }//�g�U�̌���
            if (status.turretData.equidistant)//���Ԋu�ɔ��˂���Ȃ�
            {
                spread = delta;
                delta += status.turretData.spread / (status.turretData.pellets - 1);
            }
            if (status.turretData.fireRandomly) { spread = Random.Range(-180f, 180f); }//�����_���ɔ�΂��Ȃ�

            var pjtl = Instantiate(status.turretData.projectile, transform.position, quaternion);//pjtl�̐���
            pjtl.GetComponent<TurretProjectile>().Init(this, target);
            pjtl.transform.Rotate(new Vector3(0, 0, 1), spread);//�g�U����]������
        }

    }

    public Turret.TurretStatus GetTurretStatus() { return status; }
    public bool CheckAlive() { return !status.dead; }
}
