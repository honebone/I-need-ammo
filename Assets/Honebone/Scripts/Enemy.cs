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

        public float fireRate_mul;
        public float fireRate;

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
            fireRate = data.fireRate;

            HP = maxHP;
        }
    }
    [SerializeField]
    EnemyData test;
    EnemyStatus status;

    bool targetingTurret;
    Transform targetTransform;
    Vector2 targetDiff;

    EnemySpawner enemySpawner;

    SpriteRenderer sprite;
    bool flipped;
    public void Init(EnemyData data)
    {
        status = new EnemyStatus();
        status.Init(data);
        sprite = GetComponent<SpriteRenderer>();
        enemySpawner = FindObjectOfType<EnemySpawner>();//test
        targetDiff = new Vector2();
    }
    private void Start()
    {
        Init(test);
        //targetTransform = targetTurret.transform;
    }
    public void SetBaseTF(Transform baseTF) { targetTransform = baseTF; }

    public void Damage(int DMG,Transform attackerTF)
    {
        status.HP -= DMG;
        if (status.HP <= 0)
        {
            status.dead = true;

            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;

            enemySpawner.RemoveEnemyTF(transform);
        }
        else
        {
            if (!targetingTurret && (attackerTF.position - transform.position).magnitude <= targetDiff.magnitude)//ベースをターゲットしていて、攻撃してきたタレットの方がベースより近かったら
            {
                targetingTurret = true;
                targetTransform = attackerTF;
            }
        }
    }
    void Update()
    {
        //if (targetTurret == null || !targetTurret.CheckAlive()) { SetTarget(); }
    }
    //public void SetTarget()
    //{
    //    Vector2 origin = transform.position;//originに自身の座標を代入
    //    Vector2 direction = new Vector2(1, 0);//directionに自身からプレイヤーに向かう単位ベクトルを代入
    //    RaycastHit2D hit2D = Physics2D.CircleCast(origin, status.range, direction);
    //    Debug.DrawRay(origin, direction * status.range, Color.red);//Rayと同じ始点、方向、長さの赤い線を1フレーム描画
    //    if (hit2D.CheckRaycastHit("Turret"))
    //    {
    //        targetTurret = hit2D.collider.GetComponent<Turret>();
    //        print("ok");
    //        StartCoroutine(Interval());
    //    }
    //}
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


        if (CheckAlive()&&targetDiff.magnitude>status.range)
        {
            transform.Translate(targetDiff.normalized * status.speed / 50f);
        }
    }
    public bool CheckAlive() { return !status.dead; }
}
