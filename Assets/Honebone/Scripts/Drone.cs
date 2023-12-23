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

    LogUI logUI;
    ScoreManager scoreManager;

    int targetIndex;
    bool supplying;
    bool returning;
    float timer;
    public void Init(DroneStatus droneStatus,Transform b)
    {
        status = droneStatus;
        //sprite = GetComponent<SpriteRenderer>();
        targetDiff = new Vector2();
        //status.orders = new List<DroneOrder>();
        status.checkList = new bool[status.orders.Count];
        targetIndex = 0;
        targetTransform = status.orders[targetIndex].target.turret.transform;
        baseTF = b;
        logUI = FindObjectOfType<LogUI>();
        scoreManager = FindObjectOfType<ScoreManager>();
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
            if(status.orders[targetIndex] == null || !status.orders[targetIndex].target.turret.CheckAlive())
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
        if (targetIndex >= status.orders.Count)//�S��t�̃^�[�Q�b�g�ɕ⋋����������x�[�X�ɋA��
        {
            returning = true;
            targetTransform = baseTF;
        }
        else if (status.orders[targetIndex] == null || !status.orders[targetIndex].target.turret.CheckAlive())//�^�[�Q�b�g���̃^���b�g���j�󂳂ꂽ��A���̃^�[�Q�b�g��
        {
            targetIndex++;
            targetTransform = status.orders[targetIndex].target.turret.transform;
        }
        else { targetTransform = status.orders[targetIndex].target.turret.transform; }
        targetDiff = targetTransform.position - transform.position;
    }
    void Supply()
    {
        logUI.AddLog(string.Format("<<{0}�ɕ�����⋋>>", status.orders[targetIndex].target.turretData.turretName));
        int baseScore = FindObjectOfType<GameManager>().GetBaseScore();
        bool f = false;

        int suppliedAmount = status.orders[targetIndex].supplyItems.Count;
        scoreManager.AddScore(baseScore * 0.1f * suppliedAmount, string.Format("{0}�̃A�C�e����⋋", suppliedAmount), true);
        foreach (ItemData item in status.orders[targetIndex].supplyItems)
        {
            switch (item.itemTag)
            {
                case ItemData.ItemTag.repair:
                    f = status.orders[targetIndex].target.turret.Repair(item.quantityPerStack);
                    logUI.AddLog(string.Format("�E�^���b�g��{0}�C��", item.quantityPerStack));
                    break;
                case ItemData.ItemTag.ammo:
                    f = status.orders[targetIndex].target.turret.SupplyAmmo(item.quantityPerStack);
                    logUI.AddLog(string.Format("�E�e���{0}��[", item.quantityPerStack));
                    break;
                case ItemData.ItemTag.battery:
                    f = status.orders[targetIndex].target.turret.SupplyBattery(item.quantityPerStack);
                    logUI.AddLog(string.Format("�E�o�b�e���[��{0}�[�d", item.quantityPerStack));
                    break;
            }
            if (f)
            {
                scoreManager.AddScore(baseScore * 0.4f, "�������X�Ȃ�!", true);
            }
            f = false;
        }
        
    }
}
