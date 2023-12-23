using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectItemButton : MonoBehaviour
{
    [SerializeField]
    Image icon;
    [SerializeField]
    Text nameText;
    [SerializeField]
    Text stackText;

    DronesUI dronesUI;

    ItemData item;
    Drone.DroneOrder order;
    int stack;
    public void Init(ItemData i,Drone.DroneOrder o, DronesUI d)
    {
        item = i;
        order = o;
        icon.sprite = item.itemImage;
        nameText.text = string.Format("{0}\nx{1}", item.itemName,item.quantityPerStack);
        stackText.text = string.Format("{0}スタック", order.GetItemStack(item));
        dronesUI = d;
    }
    public void Select()
    {
        //dronesUI.SelectTurret(status);
    }
    public void Add()
    {
        order.supplyItems.Add(item);
        OnChangeStack();
    }
    public void Remove()
    {
        if (order.GetItemStack(item) > 0)
        {
            order.supplyItems.Remove(item);
            OnChangeStack();
        }
    }
    public void OnChangeStack()
    {
        stackText.text = string.Format("{0}スタック", order.GetItemStack(item));
        dronesUI.SetItemSlotsText();
        dronesUI.CheckDeployable();
        dronesUI.SetSupplyIcons();
    }
}
