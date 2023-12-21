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
        public List<DroneOrder> orders;
        public bool[] checkList;
        public bool occupied;
        public Vector2 pos;

        public void Init(DroneData data)
        {
            droneData = data;
            moveSpeed=data.moveSpeed;
            orders = new List<DroneOrder>();
        }
        public int CountItemSlots()
        {
            int count = 0;
            foreach(DroneOrder order in orders)
            {
                count += order.supplyItems.Count;
            }
            return count;
        }
        public bool CheckOrders()
        {
            if(orders.Count == 0) { return false; }
            foreach(DroneOrder order in orders)
            {
                if (!order.Check(droneData.itemCap)) { return false; }
            }
            return true;
        }
    }
    public class DroneOrder
    {
        public Turret.TurretStatus target;
        public List<ItemData> supplyItems;
        public int GetItemStack(ItemData data)
        {
            int stack = 0;
            foreach(ItemData item in supplyItems)
            {
                if (data.itemName == item.itemName) { stack++; }
            }
           return stack;
        }
        public bool Check(int itemCap)
        {
            return target != null && !target.dead && supplyItems.Count > 0 && supplyItems.Count <= itemCap;
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
    public void Init(DroneStatus droneStatus,List<Turret> targets,Transform b)
    {
        status = droneStatus;
        //sprite = GetComponent<SpriteRenderer>();
        targetDiff = new Vector2();
        status.targets = new List<Turret>(targets);
        //status.orders = new List<DroneOrder>();
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
            status.pos=transform.position;
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
            if(status.targets[targetIndex] == null || !status.targets[targetIndex].CheckAlive())
            {
                timer = 0;
                supplying = false;
                targetIndex++;
                statusUI.EndSupply();
                SetTarget();
            }
            else
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
