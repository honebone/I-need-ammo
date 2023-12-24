using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Up_HighCapMag : UpgradeModule
{
    public override void OnLevelUp()
    {
        turret.AddMaxAmmo_mul(10);
    }
    public override string GetEffectInfo()
    {
        return string.Format("Å‘å’e–ò+{0}“", 10 * level);
    }
}
