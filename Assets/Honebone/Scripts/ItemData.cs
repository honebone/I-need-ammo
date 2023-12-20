using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "I_", menuName = "ScriptableObjects/ItemData")]


public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public enum ItemTag { repair, ammo, battery, upgrade, booster }
    public ItemTag itemTag;
    public int quantityPerStack;
}
