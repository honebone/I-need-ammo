using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatusUI : MonoBehaviour
{
    [SerializeField]
    GameObject HPBarObj;
    [SerializeField]
    Slider HPBar;

    Enemy.EnemyStatus status;
    InfoUI infoUI;
    public void Init(Enemy enemy,InfoUI info)
    {
        status = enemy.GetEnemyStatus();
        infoUI = info;

        HPBar.maxValue = status.maxHP;
        HPBar.value = status.HP;
    }

    public void SetSliderValue()
    {
        HPBar.value = status.HP;
    }

    bool f;
    private void Update()
    {
        if (f)
        {
            SetSliderValue();
            infoUI.SetText(status.GetInfo());
        }
    }
    public void OnMouseEnter()
    {
        if (!status.dead)
        {
            f = true;
            HPBarObj.SetActive(true);
        }
    }
    public void OnMouseExit()
    {
        f = false;
        HPBarObj.SetActive(false);
        infoUI.ResetText();
    }
}
