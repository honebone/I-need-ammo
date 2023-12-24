using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugFunctions : MonoBehaviour
{
   [SerializeField]
   bool debug = true;
    [SerializeField]
    Turret test;
    [SerializeField]
    ItemData upgrade;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (debug)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                FindObjectOfType<GameManager>().StartGame();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                test.Upgrade(upgrade);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                FindObjectOfType<Base>().AddItem(upgrade);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                foreach(Turret turret in FindObjectOfType<Base>().GetTurrets())
                {
                    turret.SupplyAmmo(1000);
                    turret.Repair(1000);
                    turret.SupplyBattery(1000);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                test.Damage(10000);
            }
        }
    }
}
