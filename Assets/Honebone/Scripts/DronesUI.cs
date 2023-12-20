using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronesUI : MonoBehaviour
{  
    Animator anim;
    bool enabled;
    [SerializeField]
    Base @base;
    [SerializeField]
    GameObject droneButton;
    [SerializeField]
    Transform droneButtonP;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToggleUI()
    {
        if (!enabled)
        {
            enabled = true;
            anim.SetInteger("phase", 1);
        }
        else
        {
            enabled = false;
            anim.SetInteger("phase", 0);
            anim.SetTrigger("close");
        }
    }
    public void SetDroneButtons(List<Drone.DroneStatus> drones)
    {
        for(int i = 0; i < droneButtonP.childCount; i++) { Destroy(droneButtonP.GetChild(i).gameObject); }
        foreach(Drone.DroneStatus drone in drones)
        {
            var b = Instantiate(droneButton, droneButtonP);
            b.GetComponent<SelectDroneButton>().Init(drone,this);
        }
    }
    public void SelectDrone(Drone.DroneStatus s)
    {
        @base.SendDrone(s);
    }
}
