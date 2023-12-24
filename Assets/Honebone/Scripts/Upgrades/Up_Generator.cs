using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Up_Generator : UpgradeModule
{
    public override void OnLevelUp()
    {
        turret.AddGenerator(10);
    }
    public override string GetEffectInfo()
    {
        return string.Format("HPが{0}％以下の時、敵を倒すとバッテリーを1充電", 10 * level);
    }
}
