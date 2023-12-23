using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    List<Turret> turrets;
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
    public void DeployTurret(TurretData turret,Vector2 pos)
    {
        var t = Instantiate(turret.obj, pos, Quaternion.identity, turretsP);
        t.GetComponent<Turret>().Init(turret);
        logUI.AddLog(string.Format("タレットを追加：{0}", turret.turretName));
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
    public int GetTurretsAmount() { return turrets.Count; }
}
