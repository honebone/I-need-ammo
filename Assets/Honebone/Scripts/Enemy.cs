using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public class EnemyStatus
    {
        public EnemyData enemyData;

        public float maxHP_mul;
        public int maxHP;

        public float DMG_mul;
        public int DMG;

        public float speed_mul;
        public float speed;

        public float range_mul;
        public float range;

        public float attackSpeed_mul;
        public float attackSpeed;

        public bool dead;
        public int HP;
        public int shield;

        public void Init(EnemyData data)
        {
            enemyData = data;
            maxHP = data.maxHP;
            DMG = data.DMG;
            speed = data.speed;
            range = data.range;
            attackSpeed = data.attackSpeed;

            HP = maxHP;
        }
        public string GetInfo()
        {
            string s = "";
            s += string.Format("[{0}]\n\n", enemyData.enemyName);
            s += string.Format("HP：{0}/{1}\n\n", HP, maxHP);
            s += string.Format("DMG：{0}\n", DMG);
            s += string.Format("攻撃速度：毎秒{0}回\n", attackSpeed);
            if (enemyData.charger)
            {
                s += "<突撃>\nタレットに攻撃されてもベースを狙い続ける";
            }

            return s;
        }
    }
    EnemyStatus status;

    Transform baseTF;
    bool targetingTurret;
    Transform targetTransform;
    Vector2 targetDiff;

    [SerializeField]
    GameObject damageText;
    [SerializeField]
    GameObject blood;
    [SerializeField]
    EnemyStatusUI statusUI;
    EnemySpawner enemySpawner;
    InfoUI infoUI;
    ScoreManager scoreManager;

    SpriteRenderer sprite;
    bool flipped;
    float timer_attack;
    bool readyAttack;
    public void Init(Transform b,InfoUI info,EnemyData enemyData,ScoreManager score)
    {
        baseTF = b;
        infoUI = info;
        targetTransform = baseTF;
        scoreManager = score;

        status = new EnemyStatus();
        status.Init(enemyData);
        sprite = GetComponent<SpriteRenderer>();
        enemySpawner = FindObjectOfType<EnemySpawner>();//test
        targetDiff = new Vector2();

        statusUI.Init(this, infoUI);
    }    

    public void Damage(int DMG,Transform attackerTF)
    {
        if (CheckAlive())
        {
            status.HP -= DMG;
            var t = Instantiate(damageText, transform.position, Quaternion.identity);
            t.GetComponent<DamageText>().Init(DMG);
            if (status.HP <= 0)
            {
                status.dead = true;

                for (int i = 0; i < 3; i++) { Bleed(); }

                GetComponent<Collider2D>().enabled = false;
                GetComponent<SpriteRenderer>().enabled = false;

                enemySpawner.RemoveEnemyTF(transform);
                //scoreManager.AddScore(1, "");

                Destroy(gameObject, 5f);
            }
            else
            {
                if (!status.enemyData.charger&&!targetingTurret && (attackerTF.position - transform.position).magnitude <= targetDiff.magnitude)//ベースをターゲットしていて、攻撃してきたタレットの方がベースより近かったら
                {
                    targetingTurret = true;
                    targetTransform = attackerTF;
                }
            }
            if (33f.Probability()) { Bleed(); }
        }
    }
    void Bleed()
    {
        Vector2 bloodPos = transform.position;
        bloodPos.y -= 2;
        bloodPos.x += Random.Range(-2f, 2f);
        Instantiate(blood, bloodPos, Quaternion.identity);
    }
    void Update()
    {
        timer_attack += Time.deltaTime;
        if (timer_attack >= (1f / status.attackSpeed)) { readyAttack = true; }
        if (targetDiff.magnitude <= status.range && readyAttack&&CheckAlive())
        {
            readyAttack = false;
            timer_attack = 0;
            if (!status.enemyData.rangedAttack)//近接攻撃をするなら、直接攻撃
            {
                if (targetingTurret)//タレットをターゲット中なら
                {
                    targetTransform.GetComponent<Turret>().Damage(status.DMG);
                }
            }
            else
            {
                FireProjectile();
            }
        }
    }
    public void FireProjectile()
    {
        //if (status.enemyData.SE_Fire != null) { soundManager.PlaySE(transform.position, status.turretData.SE_Fire); }

        Vector3 dir = (targetTransform.transform.position - transform.position).normalized;
        Quaternion quaternion = Quaternion.FromToRotation(Vector3.up, dir);
       
        var pjtl = Instantiate(status.enemyData.projectile, transform.position, quaternion);
        pjtl.GetComponent<EnemyProjectile>().Init(this);
    }
    void FixedUpdate()
    {
        targetDiff = targetTransform.position - transform.position;
        if (targetDiff.x < 0 && !flipped)
        {
            flipped = true;
            sprite.flipX = true;
            //foreach (GameObject flipObject in flipObjects)
            //{
            //    Vector2 pos = flipObject.transform.localPosition;
            //    pos.x = -1;
            //    flipObject.transform.localPosition = pos;
            //}

        }
        if (targetDiff.x > 0 && flipped)
        {
            flipped = false;
            sprite.flipX = false;
            //foreach (GameObject flipObject in flipObjects)
            //{
            //    Vector2 pos = flipObject.transform.localPosition;
            //    pos.x= -1;
            //    flipObject.transform.localPosition = pos;
            //}
        }

        if (targetingTurret && !targetTransform.GetComponent<Turret>().CheckAlive())//ターゲット中のタレットが破壊されたら、ターゲットをベースに戻す
        {
            targetingTurret = false;
            targetTransform = baseTF;
        }
        if (CheckAlive()&&targetDiff.magnitude>status.range)
        {
            //rb.velocity = targetDiff.normalized * status.speed;
            transform.Translate(targetDiff.normalized * status.speed / 50f);
        }
       
    }
    public bool CheckAlive() { return !status.dead; }
    public EnemyStatus GetEnemyStatus() { return status; }
}
