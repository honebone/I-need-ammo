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

        public int killCount_total;
        public int dealtDMG_total;
        public int killCount_wave;
        public int dealtDMG_wave;

        public float doubletap;
        public int generator;
        public int executer;

        public List<UpgradeModule> upgrades;
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
            upgrades = new List<UpgradeModule>();
        }
        public void LevelUpUpgrade(ItemData upgrade)
        {
            foreach (UpgradeModule upgradeModule in upgrades)
            {
                if (upgradeModule.GetItemData() == upgrade)
                {
                    upgradeModule.LevelUp();
                    return;
                }
            }
        }
        public int GetUpgradeLevel(ItemData upgrade)
        {
            foreach(UpgradeModule upgradeModule in upgrades)
            {
                if (upgradeModule.GetItemData() == upgrade) { return upgradeModule.GetLevel(); }
            }
            return 0;
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
            s += string.Format("与ダメージ：{0}(累計{1})\n",dealtDMG_wave, dealtDMG_total);
            s += string.Format("キル数：{0}(累計{1})\n",killCount_wave, killCount_total);
            foreach (UpgradeModule upgradeModule in upgrades)
            {
                s += "\n" + upgradeModule.GetInfo() + "\n";
            }

            return s;
        }
        public void KillConfirmed()
        {
            killCount_total++;
            killCount_wave++;
        }
        public void AddDMGDealt(int DMG)
        {
            dealtDMG_total += DMG;
            dealtDMG_wave += DMG;
        }
        public void ResetCounters()
        {
            killCount_wave = 0;
            dealtDMG_wave = 0;
        }
    }
    [SerializeField]
    TurretData test;
    TurretStatus status;
    Enemy target;
    [SerializeField]
    TurretStatusUI statusUI;
    [SerializeField]
    Transform upgradeP;

    [SerializeField]
    GameObject damageText;
    EnemySpawner enemySpawner;
    SoundManager soundManager;
    LogUI logUI;
    GameManager gameManager;

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
        gameManager = FindObjectOfType<GameManager>();
        logUI = FindObjectOfType<LogUI>();
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

            FindObjectOfType<Base>().RemoveTurret(this);
            logUI.AddLog(string.Format("{0}が破壊!", status.turretData.turretName));

            statusUI.gameObject.SetActive(false);
            Destroy(gameObject,10f);
        }
    }
    IEnumerator Fire()
    {
        if (status.turretData.SE_GengeratePjtor != null) { soundManager.PlaySE(transform.position, status.turretData.SE_GengeratePjtor); }

        if (CheckFunctional())
        {
            FireProjectile();
            ConsumeAmmo();
        }
        if (status.doubletap.Probability())
        {
            yield return new WaitForSeconds(0.1f);
            if (CheckFunctional()&&target != null || target.CheckAlive())
            {
                FireProjectile();
                var t = Instantiate(damageText, transform.position, Quaternion.identity);
                t.GetComponent<DamageText>().Init_Message("ダブルタップ!", Color.yellow);
            }
        }
    }
    public void FireProjectile()
    {
        if (status.turretData.SE_Fire != null) { soundManager.PlaySE(transform.position, status.turretData.SE_Fire); }

        Vector3 dir = new Vector3();
        dir = (target.transform.position - transform.position).normalized;
        Quaternion quaternion = Quaternion.FromToRotation(Vector3.up, dir);
        float delta = status.turretData.spread / -2f; 
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
            pjtl.GetComponent<TurretProjectile>().Init(this, target,gameManager);
            pjtl.transform.Rotate(new Vector3(0, 0, 1), spread);//拡散分回転させる
        }

    }
    public bool SupplyAmmo(int quantity)
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
        return delta >= quantity;
    }
    public bool SupplyBattery(int quantity)
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
        return delta >= quantity;
    }
    public bool Repair(int quantity)
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
        return delta >= quantity;
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

    //===============================================================================<<アップグレード関連>>=========================================
   public void Upgrade(ItemData upgrade)
    {
        switch (status.GetUpgradeLevel(upgrade))
        {
            case 0:
                var u = Instantiate(upgrade.obj, upgradeP);
                u.GetComponent<UpgradeModule>().Init(this);
                status.upgrades.Add(u.GetComponent<UpgradeModule>());
                break;
            case 5:
                break;
            default:
                status.LevelUpUpgrade(upgrade);
                break;
        }
        
    }
    
    public void AddDMG_mul(float value)
    {
        status.DMG_mul += value;
        status.DMG = Mathf.RoundToInt(status.turretData.DMG * (100 + status.DMG_mul) / 100f);
    }
    public void AddAttackSpeed_mul(float value)
    {
        status.attackSpeed_mul += value;
        status.attackSpeed = Mathf.RoundToInt(status.turretData.attackSpeed * (100 + status.attackSpeed_mul) / 100f);
    }
    public void AddMaxHP_mul(float value)
    {
        status.maxHP_mul += value;
        int i = Mathf.RoundToInt(status.maxHP * value);
        status.maxHP = Mathf.RoundToInt(status.turretData.maxHP * (100 + status.maxHP_mul) / 100f);
        status.HP = Mathf.Min(status.maxHP, status.HP + i);
        statusUI.SetSliderValue();
    }
    public void AddMaxAmmo_mul(float value)
    {
        status.maxAmmo_mul += value;
        int i = Mathf.RoundToInt(status.maxAmmo * value);
        status.maxAmmo = Mathf.RoundToInt(status.turretData.maxAmmo * (100 + status.maxAmmo_mul) / 100f);
        status.ammo = Mathf.Min(status.maxAmmo, status.ammo + i);
        statusUI.SetSliderValue();
    }
    public void AddMaxBattery_mul(float value)
    {
        status.maxBattery_mul += value;
        int i = Mathf.RoundToInt(status.maxBattery * value);
        status.maxBattery = Mathf.RoundToInt(status.turretData.maxBattery * (100 + status.maxBattery_mul) / 100f);
        status.battery = Mathf.Min(status.maxBattery, status.battery + i);
        statusUI.SetSliderValue();
    }
    public void AddDoubleTap(float value)
    {
        status.doubletap += value;
    }
    public void AddGenerator(int value)
    {
        status.generator += value;
    }
    public void AddExecuter(int value)
    {
        status.executer += value;
    }
    public Turret.TurretStatus GetTurretStatus() { return status; }
    public bool CheckAlive() { return !status.dead; }
    public bool CheckFunctional()
    {
        return !status.dead && status.ammo > 0 && status.battery > 0;
    }
}
