using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeModule : MonoBehaviour
{
    [SerializeField]
    ItemData data;
    protected Turret turret;
    protected int level;
    public void Init(Turret t)
    {
        turret = t;
        LevelUp();
    }
    public void LevelUp()
    {
        level++;
        OnLevelUp();
    }
    public virtual void OnLevelUp() { }

    public string GetInfo()
    {
        string s = "";
        if (level == 5) { s = string.Format("<<{0}LvMax>>\n", data.itemName); }
        else { s = string.Format("<<{0}Lv{1}>>\n", data.itemName, level); }
        s += GetEffectInfo();
        return s;
    }
    public virtual string GetEffectInfo() { return "error"; }
    public ItemData GetItemData() { return data; }
    public int GetLevel() { return level; }
}
