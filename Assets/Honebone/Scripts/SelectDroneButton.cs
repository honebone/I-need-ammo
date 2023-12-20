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
    Text text;

    DronesUI dronesUI;
   public void Init(Drone.DroneStatus s,DronesUI d)
    {
        status = s;
        icon.sprite = status.droneData.droneImage;
        text.text = status.droneData.droneName;
        dronesUI = d;
    }
    public void Select()
    {
        dronesUI.SelectDrone(status);
    }
}
