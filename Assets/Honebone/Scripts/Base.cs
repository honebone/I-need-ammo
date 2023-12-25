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

    [SerializeField]
    GameObject damageText;

    DronesUI dronesUI;
    LogUI logUI;
    BaseUI baseUI;
    GameManager gameManager;

    public int HP = 500;
    void Start()
    {
        dronesUI = FindObjectOfType<DronesUI>();
        logUI = FindObjectOfType<LogUI>();
        baseUI = FindObjectOfType<BaseUI>();
        gameManager = FindObjectOfType<GameManager>();
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

    public void Damage(int DMG)
    {
        HP -= DMG;
        var t = Instantiate(damageText, transform.position, Quaternion.identity);
        t.GetComponent<DamageText>().Init(DMG);
        baseUI.SetSliderValue();
        if (HP <= 0)
        {
            gameManager.GameOver();
        }
    }
    
    public void ReturnDrone(Drone.DroneStatus s)
    {
        s.occupied = false;
        s.orders = new List<Drone.DroneOrder>();
        logUI.AddLog(string.Format("{0}Ç™ãAä“", s.droneData.droneName));

        dronesUI.SetDroneButtons(drones);
    }
    public void SendDrone(Drone.DroneStatus s)
    {
        var d = Instantiate(s.droneData.obj, transform.position, Quaternion.identity);
        d.GetComponent<Drone>().Init(s, transform);

        logUI.AddLog(string.Format("{0}Ç™èoåÇ", s.droneData.droneName));

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
    public void RemoveTurret(Turret turret)
    {
        turrets.Remove(turret);
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
