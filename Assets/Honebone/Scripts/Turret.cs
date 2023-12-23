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
        public Turret turret;

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
            ammo = maxAmmo;
            battery = maxBattery;
        }
        public string GetInfo()
        {
            string s = "";
            s += string.Format("[{0}]\n\n", turretData.turretName);
            s += string.Format("バッテリー：{0}/{1}\n", battery,maxBattery);
            s += string.Format("弾薬：{0}/{1}\n", ammo,maxAmmo);
            s += string.Format("HP：{0}/{1}\n\n", HP,maxHP);
            s += string.Format("DMG：{0}\n", DMG);
            s += string.Format("攻撃速度：毎秒{0}回\n", attackSpeed);

            return s;
        }
    }
    [SerializeField]
    TurretData test;
    TurretStatus status;
    Enemy target;
    [SerializeField]
    TurretStatusUI statusUI;

    [SerializeField]
    GameObject damageText;
    EnemySpawner enemySpawner;
    SoundManager soundManager;

    float timer_attack;
    bool readyFire;
    float timer_battery;
    public void Init(TurretData data)
    {
        status = new TurretStatus();
        status.Init(data);
        status.turret = this;
        statusUI.Init(this);
    }
    private void Start()
    {
        Init(test);
        enemySpawner = FindObjectOfType<EnemySpawner>();
        soundManager = FindObjectOfType<SoundManager>();
    }
    void Update()
    {
        if (target == null || !target.CheckAlive()) { SetTarget(); }//ターゲットがいないか、今のターゲットが死んでいるとき、新たにターゲットを設定
        Vector2 origin = transform.position;
        Vector2 direction = new Vector2(1, 0);
        Debug.DrawRay(origin, direction * status.range, Color.red);

        timer_attack += Time.deltaTime;
        if (timer_attack >= (1f / status.attackSpeed)) { readyFire = true; }
        if (target != null && readyFire && CheckFunctional())
        {
            readyFire = false;
            timer_attack = 0;
            Attack();
        }

        timer_battery += Time.deltaTime;
        if (timer_battery >= 1f)//バッテリーの消費
        {
            timer_battery -= 1f;
            ConsumeBattery(1);
        }

    }
    public void SetTarget()
    {
        target = enemySpawner.GetNearestEnemy(transform.position, status.range);
    }
    public virtual void Attack()
    {
        if (target != null && target.CheckAlive())
        {
            StartCoroutine(Fire());
        }
    }
    public void Damage(int DMG)
    {
        status.HP -= DMG;
        var t = Instantiate(damageText, transform.position, Quaternion.identity);
        statusUI.SetSliderValue();
        t.GetComponent<DamageText>().Init(DMG);
        if (status.HP <= 0)
        {
            status.dead = true;

            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    IEnumerator Fire()
    {
        var wait = new WaitForSeconds(status.turretData.fireRate);
        if (status.turretData.SE_GengeratePjtor != null) { soundManager.PlaySE(transform.position, status.turretData.SE_GengeratePjtor); }

        for (int i = 0; i < status.turretData.fireRounds; i++)
        {
            if (CheckFunctional())
            {
                FireProjectile();
                ConsumeAmmo();
                if (status.turretData.fireRate > 0) { yield return wait; }
            }
            else { break; }
        }
    }
    public void FireProjectile()
    {
        if (status.turretData.SE_Fire != null) { soundManager.PlaySE(transform.position, status.turretData.SE_Fire); }

        Vector3 dir = new Vector3();
        dir = (target.transform.position - transform.position).normalized;
        Quaternion quaternion = Quaternion.FromToRotation(Vector3.up, dir);
        float delta = status.turretData.spread / -2f; ;
        for (int i = 0; i < status.turretData.pellets; i++)
        {
            float spread = 0f;
            if (status.turretData.spread > 0 && !status.turretData.equidistant) { spread = Random.Range(status.turretData.spread / -2f, status.turretData.spread / 2f); }//拡散の決定
            if (status.turretData.equidistant)//等間隔に発射するなら
            {
                spread = delta;
                delta += status.turretData.spread / (status.turretData.pellets - 1);
            }
            if (status.turretData.fireRandomly) { spread = Random.Range(-180f, 180f); }//ランダムに飛ばすなら

            var pjtl = Instantiate(status.turretData.projectile, transform.position, quaternion);//pjtlの生成
            pjtl.GetComponent<TurretProjectile>().Init(this, target);
            pjtl.transform.Rotate(new Vector3(0, 0, 1), spread);//拡散分回転させる
        }

    }
    public void SupplyAmmo(int quantity)
    {
        int delta = status.maxAmmo - status.ammo;
        if (delta >= quantity)
        {
            status.ammo += quantity;
        }
        else
        {
            status.ammo=status.maxAmmo;
        }
        statusUI.SetSliderValue();
    }
    public void SupplyBattery(int quantity)
    {
        int delta = status.maxBattery - status.battery;
        if (delta >= quantity)
        {
            status.battery += quantity;
        }
        else
        {
            status.battery = status.maxBattery;
        }
        statusUI.SetSliderValue();
    }
    public void Repair(int quantity)
    {
        int delta = status.maxHP - status.HP;
        if (delta >= quantity)
        {
            status.HP += quantity;
        }
        else
        {
            status.HP = status.maxHP;
        }
        statusUI.SetSliderValue();
    }
    void ConsumeAmmo()
    {
        status.ammo -= 1;
        if (status.ammo < 0) { status.ammo = 0; }
        statusUI.SetSliderValue();
    }
    void ConsumeBattery(int value)
    {
        status.battery -= value; 
        if (status.battery < 0) { status.battery = 0; }
        statusUI.SetSliderValue();
    }

    public Turret.TurretStatus GetTurretStatus() { return status; }
    public bool CheckAlive() { return !status.dead; }
    public bool CheckFunctional()
    {
        return !status.dead && status.ammo > 0 && status.battery > 0;
    }
}
