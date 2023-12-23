using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretStatusUI : MonoBehaviour
{
    [SerializeField]
    Slider HPBar;
    [SerializeField]
    Slider ammoBar;
    [SerializeField]
    Slider batteryBar;

    Turret.TurretStatus status;
    InfoUI infoUI;
    public void Init(Turret turret)
    {
        status = turret.GetTurretStatus();
        infoUI = FindObjectOfType<InfoUI>();

        HPBar.maxValue = status.maxHP;
        HPBar.value = status.HP;
        ammoBar.maxValue = status.maxAmmo;
        ammoBar.value = status.ammo;
        batteryBar.maxValue = status.maxBattery;
        batteryBar.value = status.battery;
    }

    public void SetSliderValue()
    {
        HPBar.value = status.HP;
        ammoBar.value = status.ammo;
        batteryBar.value = status.battery;

    }

    bool f;
    private void Update()
    {
        if (f) { infoUI.SetText(status.GetInfo()); }
    }
    public void OnMouseEnter()
    {
        f = true;
    }
    public void OnMouseExit()
    {
        f = false;
        infoUI.ResetText();
    }

}
