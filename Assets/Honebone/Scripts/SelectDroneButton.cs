using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectDroneButton : MonoBehaviour
{
    Drone.DroneStatus status;
    [SerializeField]
    Image icon;
    [SerializeField]
    Image background;
    [SerializeField]
    Text text;
    [SerializeField]
    Image selected;

    DronesUI dronesUI;
    InfoUI infoUI;
   public void Init(Drone.DroneStatus s,DronesUI d)
    {
        status = s;
        icon.sprite = status.droneData.droneImage;
        text.text = status.droneData.droneName;
        dronesUI = d;
        if (status.occupied) { background.color = Color.red; }
        infoUI = FindObjectOfType<InfoUI>();
    }
    public void Select()
    {
        if (!status.occupied) { dronesUI.SelectDrone(status); }//test
        else { FindObjectOfType<CameraController>().MoveTo(status.pos); }
        dronesUI.ResetDroneButtonsSelected();
        FindObjectOfType<TutorialUI>().DisplayTutorial("AddOrder");
        selected.enabled = true;
    }
    public void ResetSelected() { selected.enabled = false; }

    bool p;
    private void Update()
    {
        if (p)
        {
            infoUI.SetText(status.droneData.GetInfo());
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
