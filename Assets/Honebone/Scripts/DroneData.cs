using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "D_", menuName = "ScriptableObjects/DroneData")]

public class DroneData : ScriptableObject
{
    public GameObject obj;
    public string droneName;
    public Sprite droneImage;

    public int itemCap;
    public int moveSpeed;

    public string GetInfo()
    {
        string s = string.Format("<<{0}>>\n",droneName);
        s+= string.Format("�ړ����x�F{0}\n", moveSpeed);
        s += string.Format("�����ύڗʁF{0}\n", itemCap);
        return s;
    }
}
