using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SelectTurretButton : MonoBehaviour
{
    Turret.TurretStatus status;
    [SerializeField]
    Image icon;
    [SerializeField]
    Text text;
    [SerializeField]
    Image selected;

    [SerializeField]
    Slider HPBar;
    [SerializeField]
    Slider ammoBar;
    [SerializeField]
    Slider batteryBar;

    DronesUI dronesUI;
    InfoUI infoUI;
    public void Init(Turret.TurretStatus s, DronesUI d,ScrollRect sr)
    {
        status = s;
        icon.sprite = status.turretData.turretImage;
        text.text = status.turretData.turretName;
        dronesUI = d;
        infoUI = FindObjectOfType<InfoUI>();
        scroll = sr;
    }
    public void Select()
    {
        dronesUI.SelectTurret(status);
        dronesUI.ResetTurretButtonsSelected();
        selected.enabled = true;

        FindObjectOfType<TutorialUI>().DisplayTutorial("SelectItem");
    }
    public void ResetSelected() { selected.enabled = false; }

    ScrollRect scroll;
    float wheel;
    bool f;
    private void Update()
    {
        if (f)
        {
            infoUI.SetText(status.GetInfo());
            wheel += Input.mouseScrollDelta.y;
            if (wheel != 0)
            {
                scroll.verticalNormalizedPosition += wheel * 0.1f;
                wheel = 0;
            }
        }
        HPBar.maxValue = status.maxHP;
        HPBar.value = status.HP;
        ammoBar.maxValue = status.maxAmmo;
        ammoBar.value = status.ammo;
        batteryBar.maxValue = status.maxBattery;
        batteryBar.value = status.battery;
    }
    public void OnMouseEnter()
    {
        f= true;
    }
    public void OnMouseExit()
    {
        f = false;
        infoUI.ResetText();
    }
}
