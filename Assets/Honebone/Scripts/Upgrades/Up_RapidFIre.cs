using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Up_RapidFIre : UpgradeModule
{
    public override void OnLevelUp()
    {
        turret.AddAttackSpeed_mul(10);
    }
    public override string GetEffectInfo()
    {
        return string.Format("攻撃スピード+{0}％", 10 * level);
    }
}
