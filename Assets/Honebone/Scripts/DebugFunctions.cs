using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugFunctions : MonoBehaviour
{
   [SerializeField]
   bool debug = true;
    [SerializeField]
    DroneData test_drone;
    [SerializeField]
    List<Turret> turrets;
    [SerializeField]
    Transform baseTF;
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
                var d = Instantiate(test_drone.obj, transform.position, Quaternion.identity);
                d.GetComponent<Drone>().Init(test_drone, turrets, baseTF);
            }
        }
    }
}
