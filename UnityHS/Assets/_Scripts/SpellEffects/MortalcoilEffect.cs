using UnityEngine;
using System.Collections;

public class MortalcoilEffect : SpellEffect {

    public override bool getSpellTarget()
    {
        return true;
    }

    public override bool setSpellTarget(BoardCharacter c)
    {
        if (c.type == "minion")
        {
            spellTarget = c;
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void onPlay()
    {
        EffectsController.dealSpellDamage(spellTarget, 1);
        if(spellTarget.cHealth <= 0)
        {
            caster.draw(1);
        }
    }
}
