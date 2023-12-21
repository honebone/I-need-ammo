using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DronesUI : MonoBehaviour
{  
    Animator anim;
    bool f;
    [SerializeField]
    Base @base;

    [SerializeField]
    Text ItemSlotsText;
    [SerializeField]
    Text deployText;

    [Space(20)]
    [SerializeField]
    GameObject droneButton;
    [SerializeField]
    Transform droneButtonP;

    [Space(20)]
    [SerializeField]
    GameObject newOrderButton;
    [SerializeField]
    GameObject orderButton;
    [SerializeField]
    Transform orderButtonP;

    [Space(20)]
    [SerializeField]
    GameObject turretButton;
    [SerializeField]
    Transform turretButtonP;

    [Space(20)]
    [SerializeField]
    ItemData repairData;
    [SerializeField]
    ItemData batteryData;
    [SerializeField]
    GameObject itemButton;
    [SerializeField]
    Transform itemButtonP;

    Drone.DroneStatus selectedDrone;
    Drone.DroneOrder selectedOrder;
    Turret.TurretStatus selectedTurret;
    bool deployable;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToggleUI()
    {
        if (!f)
        {
            f = true;
            anim.SetInteger("phase", 1);
        }
        else
        {
            f = false;
            anim.SetInteger("phase", 0);
            anim.SetTrigger("close");
        }
    }
    
    public void CheckDeployable()
    {
        if (selectedDrone != null && selectedDrone.CheckOrders())
        {
            deployable = true;
            deployText.color = Color.yellow;
        }
        else
        {
            deployable = false;
            deployText.color = Color.gray;
        }
    }
    //===============================================================<<ドローン選択>>============================================================
    public void SetDroneButtons(List<Drone.DroneStatus> drones)
    {
        for(int i = 0; i < droneButtonP.childCount; i++) { Destroy(droneButtonP.GetChild(i).gameObject); }
        foreach(Drone.DroneStatus drone in drones)
        {
            var b = Instantiate(droneButton, droneButtonP);
            b.GetComponent<SelectDroneButton>().Init(drone,this);
        }
    }
    public void ResetDroneButtonsSelected()
    {
        foreach (SelectDroneButton button in droneButtonP.GetComponentsInChildren<SelectDroneButton>()) { button.ResetSelected(); }
    }
    public void SelectDrone(Drone.DroneStatus s)
    {
        anim.SetInteger("phase", 2);
        //@base.SendDrone(s);
        selectedDrone = s;

        selectedOrder = null;
        selectedTurret = null;
        ResetTurretButtons();
        ResetItemButtons();

        CheckDeployable();
        SetOrderButtons();
        SetItemSlotsText();
    }
    //===============================================================<<命令追加>>============================================================
    public void SetOrderButtons()
    {
        ResetOrderButtons();
        foreach (Drone.DroneOrder order in selectedDrone.orders)
        {
            var b = Instantiate(orderButton, orderButtonP);
            b.GetComponent<OrderButton>().Init(order, this,selectedOrder==order);
        }
        var n = Instantiate(newOrderButton, orderButtonP);
        n.GetComponent<OrderButton>().Init(null, this,false);
    }
    public void ResetOrderButtons()
    {
        for (int i = 0; i < orderButtonP.childCount; i++) { Destroy(orderButtonP.GetChild(i).gameObject); }
    }
    public void ResetOrderButtonsSelected()
    {
        foreach (OrderButton button in orderButtonP.GetComponentsInChildren<OrderButton>()) { button.ResetSelected(); }
    }
    public void SelectOrder(Drone.DroneOrder o)
    {
        anim.SetInteger("phase", 3);
        selectedOrder = o;

        selectedTurret = null;
        ResetItemButtons();

        CheckDeployable();
        SetTurretButtons();
    }
    //===============================================================<<タレット選択>>============================================================
    public void SetTurretButtons()
    {
        ResetTurretButtons();
        foreach (Turret.TurretStatus turret in @base.GetTurretsStatus())
        {
            var b = Instantiate(turretButton, turretButtonP);
            b.GetComponent<SelectTurretButton>().Init(turret, this);
        }
    }
    public void ResetTurretButtons()
    {
        for (int i = 0; i < turretButtonP.childCount; i++) { Destroy(turretButtonP.GetChild(i).gameObject); }
    }
    public void ResetTurretButtonsSelected()
    {
        foreach(SelectTurretButton button in turretButtonP.GetComponentsInChildren<SelectTurretButton>()) { button.ResetSelected(); }
    }
    public void SelectTurret(Turret.TurretStatus t)
    {
        selectedTurret = t;
        if (selectedOrder == null)//新規命令を選択していたなら、新たな命令を作成してドローンに追加
        {
            selectedOrder = new Drone.DroneOrder();
            selectedOrder.supplyItems = new List<ItemData>();
            selectedOrder.target = selectedTurret;
            selectedDrone.orders.Add(selectedOrder);
        }
        else
        {
            selectedOrder.target = selectedTurret;
        }
        selectedOrder.supplyItems = new List<ItemData>();
        SetItemSlotsText();

        CheckDeployable();
        anim.SetInteger("phase", 4);
        SetOrderButtons();
        SetItemButtons();
    }
    //===============================================================<<アイテム選択>>============================================================
    public void SetItemButtons()
    {
        ResetItemButtons();
        var d = Instantiate(itemButton, itemButtonP);
        d.GetComponent<SelectItemButton>().Init(repairData, selectedOrder, this);

        d = Instantiate(itemButton, itemButtonP);
        d.GetComponent<SelectItemButton>().Init(selectedTurret.turretData.ammoData, selectedOrder, this);

        d = Instantiate(itemButton, itemButtonP);
        d.GetComponent<SelectItemButton>().Init(batteryData, selectedOrder, this);

    }
    public void ResetItemButtons()
    {
        for (int i = 0; i < itemButtonP.childCount; i++) { Destroy(itemButtonP.GetChild(i).gameObject); }
    }
    public void SetItemSlotsText()
    {
        ItemSlotsText.text = string.Format("アイテムスロット{0}/{1}", selectedDrone.CountItemSlots(),selectedDrone.droneData.itemCap);
    }
    //===============================================================<<ドローン出撃>>============================================================
    public void DeployDrone()
    {
        if (deployable)
        {
            @base.SendDrone(selectedDrone);
        }
    }
}
