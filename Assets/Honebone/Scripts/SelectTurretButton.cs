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

    DronesUI dronesUI;
    public void Init(Turret.TurretStatus s, DronesUI d)
    {
        status = s;
        icon.sprite = status.turretData.turretImage;
        text.text = status.turretData.turretName;
        dronesUI = d;
    }
    public void Select()
    {
        dronesUI.SelectTurret(status);
        dronesUI.ResetTurretButtonsSelected();
        selected.enabled = true;
    }
    public void ResetSelected() { selected.enabled = false; }
}
