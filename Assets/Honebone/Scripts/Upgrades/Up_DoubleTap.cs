using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Up_DoubleTap : UpgradeModule
{
    public override void OnLevelUp()
    {
        turret.AddDoubleTap(5);
        if (level == 1) { turret.AddDoubleTap(5); }
    }
    public override string GetEffectInfo()
    {
        return string.Format("{0}���̊m���œ�x�ˌ�����(�e��͏���Ȃ�)", 5 * level + 5);
    }
}
