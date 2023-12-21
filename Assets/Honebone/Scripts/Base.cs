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

    
    public void ReturnDrone(Drone.DroneStatus s)
    {
        s.occupied = false;
        dronesUI.SetDroneButtons(drones);
    }
    public void SendDrone(Drone.DroneStatus s)
    {
        var d = Instantiate(s.droneData.obj, transform.position, Quaternion.identity);
        d.GetComponent<Drone>().Init(s, turrets, transform);

        s.occupied = true;
        //bool f = false;
        //foreach(Drone.DroneStatus drone in drones)
        //{
        //    if (drone == s)
        //    {
        //        drone.occupied = false;
        //        f = true;
        //    }
        //}
        //if (!f) { print("error"); }
        dronesUI.SetDroneButtons(drones);
    }
    public List<Turret.TurretStatus> GetTurretsStatus()
    {
        List<Turret.TurretStatus> ts = new List<Turret.TurretStatus>();
        foreach(Turret turret in turrets)
        {
            ts.Add(turret.GetTurretStatus());
        }
        return ts;
    }
}
