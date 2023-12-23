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
    InfoUI infoUI;
    public void Init(Turret.TurretStatus s, DronesUI d)
    {
        status = s;
        icon.sprite = status.turretData.turretImage;
        text.text = status.turretData.turretName;
        dronesUI = d;
        infoUI = FindObjectOfType<InfoUI>();
    }
    public void Select()
    {
        dronesUI.SelectTurret(status);
        dronesUI.ResetTurretButtonsSelected();
        selected.enabled = true;
    }
    public void ResetSelected() { selected.enabled = false; }

    float timer;
    bool f;
    private void Update()
    {
        if (f)
        {
            timer += Time.unscaledDeltaTime;
        }
        if (timer > .5f)
        {
            infoUI.SetText(status.GetInfo());
        }
    }
    public void OnMouseEnter()
    {
        f= true;
    }
    public void OnMouseExit()
    {
        f = false;
        timer = 0;
        infoUI.ResetText();
    }
}
