using UnityEngine;
using System.Collections;

public class WhirlwindEffect : SpellEffect {

    public override void onPlay()
    {
        foreach(Minion m in EffectsController.getAllMinions())
        {
            EffectsController.dealSpellDamage(m, 1);
        }

    }

}
