using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "D_", menuName = "ScriptableObjects/DroneData")]

public class DroneData : ScriptableObject
{
    public GameObject obj;

    public int moveSpeed;
}
