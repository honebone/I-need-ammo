using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DroneStatusUI : MonoBehaviour
{
    [SerializeField]
    GameObject UI;
    [SerializeField]
    Slider progressBar;

    public void Init()
    {
        
    }

    public void SetSliderValue()
    {
        //progressBar.value = status.HP;
    }
    public void StartSupply(float time)
    {
        UI.SetActive(true);
        progressBar.maxValue = time;
        progressBar.value = 0;

    }
    public void Progress(float delta)
    {
        progressBar.value += delta;
    }
    public void EndSupply()
    {
        UI.SetActive(false);
        progressBar.maxValue = 1;
        progressBar.value = 0;

    }
}
