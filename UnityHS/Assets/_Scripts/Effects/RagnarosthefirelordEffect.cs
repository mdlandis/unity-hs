using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RagnarosthefirelordEffect : Effect {

    public override void onFriendlyTurnEnd()
    {
        List<BoardCharacter> validTargets = EffectsController.getAllCharacters(CardController.getController().getOtherPlayer(self.Player));
        int choice = Random.Range(0, validTargets.Count);
        EffectsController.dealDamage(validTargets[choice], 8);
    }
}
