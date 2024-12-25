using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OrderButton : MonoBehaviour
{

    Drone.DroneOrder order;
    [SerializeField]
    Image icon;
    [SerializeField]
    Image selected;
    [SerializeField]
    Text text;

    [SerializeField]
    GameObject supplyItemIcon;
    [SerializeField]
    GameObject supplyUpgradeIcon;
    [SerializeField]
    Transform supplyItemP;

    DronesUI dronesUI;
    public void Init(Drone.DroneOrder s, DronesUI d,bool se,int index,ScrollRect sr)
    {
        order = s;
        if (order != null)
        {
            icon.sprite = order.target.turretData.turretImage;
            text.text = string.Format("–½—ß{0}", index + 1);
        }
        dronesUI = d;
        scroll = sr;
        if (se) { selected.enabled = true; }
        SetSupplyIcons();
    }
    public void Select()
    {
        dronesUI.SelectOrder(order);
        dronesUI.ResetOrderButtonsSelected();
        selected.enabled = true;
    }
    public void Remove()
    {
        dronesUI.RemoveOrder(order);
    }
    public void SetSupplyIcons()
    {
        if(order != null)
        {
            for (int i = 0; i < supplyItemP.childCount; i++) { Destroy(supplyItemP.GetChild(i).gameObject); }
            foreach (ItemData item in order.supplyItems)
            {
                if (item.itemTag == ItemData.ItemTag.upgrade)
                {
                    var b = Instantiate(supplyUpgradeIcon, supplyItemP);
                    b.GetComponent<SupplyItemIcon>().Init(item.itemImage);
                }
                else
                {
                    var b = Instantiate(supplyItemIcon, supplyItemP);
                    b.GetComponent<SupplyItemIcon>().Init(item.itemImage);
                }
            }
        }
    }
    public void ResetSelected()
    {
        selected.enabled = false;
    }

    ScrollRect scroll;
    float wheel;
    bool p;
    private void Update()
    {
        if (p)
        {
            wheel += Input.mouseScrollDelta.y;
            if (wheel != 0)
            {
                scroll.verticalNormalizedPosition += wheel * 0.1f;
                wheel = 0;
            }
        }
    }
    public void OnMouseEnter()
    {
        p = true;
    }
    public void OnMouseExit()
    {
        p = false;
    }
}
