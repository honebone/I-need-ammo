using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Up_Executer : UpgradeModule
{
    public override void OnLevelUp()
    {
        turret.AddExecuter(5);
        if (level == 1 || level == 5) { turret.AddExecuter(5); }
    }
    public override string GetEffectInfo()
    {
        int e = 5 * level + 5;
        if (level == 5) { e += 5; }
        return string.Format("�G�Ƀ_���[�W��^����HP��{0}���ȉ��ɂ����Ƃ��A����������", e);
    }
}
