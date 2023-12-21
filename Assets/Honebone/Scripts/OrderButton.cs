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

    DronesUI dronesUI;
    public void Init(Drone.DroneOrder s, DronesUI d,bool se)
    {
        order = s;
        if (order != null)
        {
            icon.sprite = order.target.turretData.turretImage;
            text.text = order.target.turretData.turretName;
        }
        dronesUI = d;
        if (se) { selected.enabled = true; }
    }
    public void Select()
    {
        dronesUI.SelectOrder(order);
        dronesUI.ResetOrderButtonsSelected();
        selected.enabled = true;
    }
    public void ResetSelected()
    {
        selected.enabled = false;
    }
}
