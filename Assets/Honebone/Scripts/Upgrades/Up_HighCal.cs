using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Up_HighCal : UpgradeModule
{
    public override void OnLevelUp()
    {
        turret.AddDMG_mul(10);
    }
    public override string GetEffectInfo()
    {
        return string.Format("DMG+{0}Åì", 10 * level);
    }
}
