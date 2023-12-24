using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectItemButton : MonoBehaviour
{
    [SerializeField]
    Image icon;
    [SerializeField]
    Image frame;
    [SerializeField]
    Image background;
    [SerializeField]
    Color selected;
    [SerializeField]
    Color deselected;
    [SerializeField]
    Sprite upgradeFrame;
    [SerializeField]
    Text nameText;
    [SerializeField]
    Text stackText;

    DronesUI dronesUI;
    Base @base;
    InfoUI infoUI;

    ItemData item;
    Drone.DroneOrder order;
    bool f;
    bool uprade;
    public void Init(ItemData i,Drone.DroneOrder o, DronesUI d,Base b)
    {
        item = i;
        order = o;
        uprade = item.itemTag == ItemData.ItemTag.upgrade;
        icon.sprite = item.itemImage;
        if (uprade) { 
            nameText.text = item.itemName;
            frame.sprite = upgradeFrame;
            frame.color = Color.white;
        }
        else { nameText.text = string.Format("{0}\nx{1}", item.itemName, item.quantityPerStack); }
        stackText.text = string.Format("{0}スタック", order.GetItemStack(item));
        dronesUI = d;
        @base = b;

        infoUI = FindObjectOfType<InfoUI>();

    }
    public void Select()
    {
        //dronesUI.SelectTurret(status);
    }
    public void Add()
    {
        if (!uprade || !f)
        {
            f = true;
            if (uprade) { @base.RemoveItem(item); }
           
            order.supplyItems.Add(item);
            OnChangeStack();
        }
    }
    public void Remove()
    {
        if ((order.GetItemStack(item) > 0 && !uprade) || f)
        {
            f = false;
            if (uprade) { @base.AddItem(item); }
            order.supplyItems.Remove(item);
            OnChangeStack();
        }
    }
    public void OnChangeStack()
    {
        stackText.text = string.Format("{0}スタック", order.GetItemStack(item));
        if (order.GetItemStack(item) > 0) { background.color = selected; }
        else { background.color = deselected; }
        dronesUI.SetItemSlotsText();
        dronesUI.CheckDeployable();
        dronesUI.SetSupplyIcons();
    }

    bool p;
    private void Update()
    {
        if (p)
        {
            infoUI.SetText(item.GetInfo());
        }
    }
    public void OnMouseEnter()
    {
        p = true;
    }
    public void OnMouseExit()
    {
        p = false;
        infoUI.ResetText();
    }
}
