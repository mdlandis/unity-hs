using UnityEngine;
using System.Collections;

public class MortalstrikeEffect : SpellEffect {

    public override bool getSpellTarget()
    {
        return true;
    }

    public override bool setSpellTarget(BoardCharacter c)
    {
        spellTarget = c;
        return true;
    }

    public override void onPlay()
    {
        int amount = 4;
        if(caster.hero.cHealth <= 12)
        {
            amount = 6;
        }
        EffectsController.dealSpellDamage(spellTarget, amount);
    }
}
