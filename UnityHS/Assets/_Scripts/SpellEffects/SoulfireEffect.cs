using UnityEngine;
using System.Collections;

public class SoulfireEffect : SpellEffect {

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

        EffectsController.dealSpellDamage(spellTarget, 4);
        EffectsController.discardRandom(caster, 1);
    }
}
