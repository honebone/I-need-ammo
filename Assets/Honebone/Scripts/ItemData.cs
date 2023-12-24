using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "I_", menuName = "ScriptableObjects/ItemData")]


public class ItemData : ScriptableObject
{
    public string itemName;
    [TextArea(3, 10)]
    public string itemInfo;
    public Sprite itemImage;
    public enum ItemTag { repair, ammo, battery, upgrade, booster }
    public ItemTag itemTag;
    public int quantityPerStack = 1;

    [Header("upgrade用")]
    public GameObject obj;

    public string GetInfo()
    {
        string s = string.Format("<<{0}>>\n", itemName);
        if (itemTag == ItemTag.upgrade) { s += "タレットを強化：\n"; }
        s += itemInfo;
        return s;
    }
}
