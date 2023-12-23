using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugFunctions : MonoBehaviour
{
   [SerializeField]
   bool debug = true;
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
        }
    }
}
