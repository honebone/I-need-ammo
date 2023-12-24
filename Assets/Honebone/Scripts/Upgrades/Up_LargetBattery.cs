using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Up_LargetBattery : UpgradeModule
{
    public override void OnLevelUp()
    {
        turret.AddMaxBattery_mul(10);
    }
    public override string GetEffectInfo()
    {
        return string.Format("�o�b�e���[�e��+{0}��", 10 * level);
    }
}
