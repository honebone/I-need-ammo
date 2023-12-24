using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    List<Turret> turrets;
    List<ItemData> inventory;
    [SerializeField]
    Transform turretsP;

    [SerializeField]
    List<DroneData> dronesData;
    List<Drone.DroneStatus> drones; 

    DronesUI dronesUI;
    LogUI logUI;
    void Start()
    {
        dronesUI = FindObjectOfType<DronesUI>();
        logUI = FindObjectOfType<LogUI>();
        turrets = new List<Turret>(turretsP.GetComponentsInChildren<Turret>());
        drones = new List<Drone.DroneStatus>();
        foreach(DroneData data in dronesData)
        {
            Drone.DroneStatus d = new Drone.DroneStatus();
            d.Init(data);
            drones.Add(d);
        }
        dronesUI.SetDroneButtons(drones);
        inventory = new List<ItemData>();
    }

    
    public void ReturnDrone(Drone.DroneStatus s)
    {
        s.occupied = false;
        s.orders = new List<Drone.DroneOrder>();
        dronesUI.SetDroneButtons(drones);
    }
    public void SendDrone(Drone.DroneStatus s)
    {
        var d = Instantiate(s.droneData.obj, transform.position, Quaternion.identity);
        d.GetComponent<Drone>().Init(s, transform);

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
    public void AddDrone(DroneData data)
    {
        Drone.DroneStatus newDrone = new Drone.DroneStatus();
        newDrone.Init(data);
        drones.Add(newDrone);
        dronesUI.SetDroneButtons(drones);
    }
    public void DeployTurret(TurretData turret,Vector2 pos)
    {
        var t = Instantiate(turret.obj, pos, Quaternion.identity, turretsP);
        t.GetComponent<Turret>().Init(turret);
        turrets.Add(t.GetComponent<Turret>());
    }
    public void AddItem(ItemData item)
    {
        inventory.Add(item);
    }
    public void RemoveItem(ItemData item)
    {
        inventory.Remove(item);
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
    public List<ItemData> GetInventory() { return inventory; }
    public List<Turret> GetTurrets() { return turrets; }
    public int GetTurretsAmount() { return turrets.Count; }
}
