using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Up_Armor : UpgradeModule
{

    public override void OnLevelUp()
    {
        turret.AddMaxHP_mul(10);
    }
    public override string GetEffectInfo()
    {
        return string.Format("ç≈ëÂHP+{0}Åì", 10 * level);
    }
}
