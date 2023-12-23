using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SupplyItemIcon : MonoBehaviour
{
    [SerializeField]
    Image itemImage;
   public void Init(Sprite sprite) { itemImage.sprite = sprite; }
}
