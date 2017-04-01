using UnityEngine;
using System.Collections;

public class BattlerageEffect : SpellEffect {

    public override void onPlay()
    {
        int count = 0;
        foreach (BoardCharacter c in EffectsController.getAllCharacters(caster))
        {
           if(c.cHealth < c.bHealth)
            {
                count++;
            }
            
        }
        caster.draw(count);
    }


}
