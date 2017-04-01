using UnityEngine;
using System.Collections;

public class ExecuteEffect : SpellEffect {

    public override bool getSpellTarget()
    {
        return true;
    }

    public override bool setSpellTarget(BoardCharacter c)
    {
        if(c.cHealth < c.bHealth && c.type == "minion" && c.Player != caster)
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
        EffectsController.destroyMinion((Minion)spellTarget);
    }

}
