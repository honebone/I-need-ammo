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
    public void Init(Turret turret)
    {
        status = turret.GetTurretStatus();

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
  
}
