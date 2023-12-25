using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseUI : MonoBehaviour
{
    [SerializeField]
    Slider HPBar;

    Base basement;
    InfoUI infoUI;

    public void SetSliderValue()
    {
        HPBar.value = basement.HP;
    }
    private void Start()
    {
        basement = FindObjectOfType<Base>();
        infoUI = FindObjectOfType<InfoUI>();

        HPBar.maxValue = basement.HP;
        HPBar.value = basement.HP;
    }

    bool f;
    private void Update()
    {
        if (f) { infoUI.SetText(string.Format("[ベース]\nHP：{0}", basement.HP)); }
    }
    public void OnMouseEnter()
    {
        f = true;
    }
    public void OnMouseExit()
    {
        f = false;
        infoUI.ResetText();
    }
}
