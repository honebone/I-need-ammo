using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogUI : MonoBehaviour
{
    [SerializeField]
    GameObject logText;
    [SerializeField]
    Color logColor;
    public void AddLog(string log)
    {
        var l = Instantiate(logText, transform);
        l.GetComponent<LogText>().Init(log, logColor);
    }
}
