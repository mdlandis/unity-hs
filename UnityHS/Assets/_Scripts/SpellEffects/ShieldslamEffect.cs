using UnityEngine;
using System.Collections;

public class ShieldslamEffect : SpellEffect
{

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
        int amount = caster.hero.armor;
        EffectsController.dealSpellDamage(spellTarget, amount);
    }

}
