using UnityEngine;
using System.Collections;

public class SoulwellEffect : SpellEffect {

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
        int amount = caster.board.Count;
        EffectsController.dealSpellDamage(spellTarget, amount);
    }
}
