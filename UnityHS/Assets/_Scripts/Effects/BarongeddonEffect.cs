using UnityEngine;
using System.Collections;

public class BarongeddonEffect : Effect {

	public override void onFriendlyTurnEnd()
    {
        foreach(BoardCharacter c in EffectsController.getAllCharacters())
        {
            if(c != self)
            {
                EffectsController.dealDamage(c, 2);
            }
            
        }
    }
}
