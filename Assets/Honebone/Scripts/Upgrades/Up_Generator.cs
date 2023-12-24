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
        return string.Format("HP��{0}���ȉ��̎��A�G��|���ƃo�b�e���[��1�[�d", 10 * level);
    }
}
