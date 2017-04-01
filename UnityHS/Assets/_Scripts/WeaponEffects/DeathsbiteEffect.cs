using UnityEngine;
using System.Collections;

public class DeathsbiteEffect : WeaponEffect {

    public override void onDeath()
    {
        foreach (Minion m in EffectsController.getAllMinions())
        {
            EffectsController.dealDamage(m, 1);
        }
    }
}
