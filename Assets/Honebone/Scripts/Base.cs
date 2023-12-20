using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField]
    List<Turret> turrets;

    [SerializeField]
    List<DroneData> dronesData;
    List<Drone.DroneStatus> drones; 

    DronesUI dronesUI;
    void Start()
    {
        dronesUI = FindObjectOfType<DronesUI>();
        drones = new List<Drone.DroneStatus>();
        foreach(DroneData data in dronesData)
        {
            Drone.DroneStatus d = new Drone.DroneStatus();
            d.Init(data);
            drones.Add(d);
        }
        dronesUI.SetDroneButtons(drones);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReturnDrone(Drone.DroneStatus s)
    {
        drones.Add(s);
        dronesUI.SetDroneButtons(drones);
    }
    public void SendDrone(Drone.DroneStatus s)
    {
        var d = Instantiate(s.droneData.obj, transform.position, Quaternion.identity);
        d.GetComponent<Drone>().Init(s.droneData, turrets, transform);
        drones.Remove(s);
        dronesUI.SetDroneButtons(drones);
    }
}
