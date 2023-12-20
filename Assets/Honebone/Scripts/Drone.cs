using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public class DroneStatus
    {
        public DroneData droneData;

        public float moveSpeed_mul;
        public float moveSpeed;

        public List<Turret> targets;
        public bool[] checkList;

        public void Init(DroneData data)
        {
            droneData = data;
            moveSpeed=data.moveSpeed;
        }
    }
    [SerializeField]
    DroneStatusUI statusUI;
    DroneStatus status;

    Transform baseTF;
    Transform targetTransform;
    Vector2 targetDiff;

    int targetIndex;
    bool supplying;
    bool returning;
    float timer;
    public void Init(DroneData data,List<Turret> targets,Transform b)
    {
        status = new DroneStatus();
        status.Init(data);
        //sprite = GetComponent<SpriteRenderer>();
        targetDiff = new Vector2();
        status.targets = new List<Turret>(targets);
        status.checkList = new bool[targets.Count];
        targetIndex = 0;
        targetTransform = status.targets[targetIndex].transform;
        baseTF = b;
    }
    private void FixedUpdate()
    {

        SetTarget();

        if (targetDiff.magnitude > 3f)
        {
            transform.Translate(targetDiff.normalized * status.moveSpeed / 50f);
        }
        else if (!returning && !supplying && !status.checkList[targetIndex])
        {
            supplying = true;
            statusUI.StartSupply(5f);
        }
        else if (returning)
        {
            baseTF.GetComponent<Base>().ReturnDrone(status);
            Destroy(gameObject);
        }


        if (supplying)
        {
            timer += Time.deltaTime;
            statusUI.Progress(Time.deltaTime);
            if (timer >= 5f)
            {
                timer = 0f;
                supplying = false;
                status.checkList[targetIndex] = true;
                Supply();
                statusUI.EndSupply();

                targetIndex++;
                SetTarget();
            }
        }
    }
    void SetTarget()
    {
        if (targetIndex >= status.targets.Count)//全てtのターゲットに補給完了したらベースに帰る
        {
            returning = true;
            targetTransform = baseTF;
        }
        else if (status.targets[targetIndex] == null || !status.targets[targetIndex].CheckAlive())//ターゲット中のタレットが破壊されたら、次のターゲットに
        {
            targetIndex++;
            targetTransform = status.targets[targetIndex].transform;
        }
        else { targetTransform = status.targets[targetIndex].transform; }
        targetDiff = targetTransform.position - transform.position;
    }
    void Supply()
    {
        status.targets[targetIndex].SupplyAmmo(100);
        status.targets[targetIndex].SupplyBattery(20);
        status.targets[targetIndex].Repair(10);
    }
}
