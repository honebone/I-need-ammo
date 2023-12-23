using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class EnemyProjectile : MonoBehaviour
{
    Enemy enemy;
    Enemy.EnemyStatus enemyStatus;
    Transform tf;
   
    float projectileSpeed;

    bool disabled;
    public void Init(Enemy e)
    {
        enemy = e;
        enemyStatus = e.GetEnemyStatus();
        

        projectileSpeed = enemyStatus.enemyData.projectileSpeed;

        tf = transform;

        StartCoroutine(CountDown());
    }
    void FixedUpdate()
    {
        //if (followTargetSpeed > 0)//追尾弾
        //{
        //    if (turretData.followCurrentTarget)//現在のプレイヤーの位置を追尾する場合は、ターゲットの位置を常に更新
        //    {
        //        targetPos = targetTF.position;
        //    }
        //    targetPosDiff = (targetPos - tf.position);
        //    Vector2 dis = targetPosDiff;
        //    if (dis.magnitude < 0.5f) { followTargetSpeed = 0; }//ターゲットの位置に到着したら追尾停止

        //    float rot = (Mathf.Atan2(targetPosDiff.y, targetPosDiff.x) * Mathf.Rad2Deg) - tf.localEulerAngles.z - 90;
        //    if (rot < -180) { rot += 360; }
        //    tf.Rotate(0, 0, Mathf.Clamp(rot, followTargetSpeed * -0.5f, followTargetSpeed * 0.5f));
        //}

        tf.Translate(Vector3.up * projectileSpeed / 50f);
    }

    void DestroyPJTL(bool expired)
    {
        disabled = true;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        Light2D light = GetComponentInChildren<Light2D>();
        if (light != null) { light.enabled = false; }

        AtTheEnd(expired);


        Destroy(gameObject, 1f);
    }
    /// <summary>expired:時間切れによる破壊か</summary>
    public virtual void AtTheEnd(bool expired) { }//消滅時誘発

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(enemyStatus.enemyData.projectileDuration);
        if (!disabled) { DestroyPJTL(true); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Turret"))
        {
            collision.GetComponent<Turret>().Damage(enemyStatus.DMG);//test
            if (!disabled) { DestroyPJTL(false); }

        }
    }
}
