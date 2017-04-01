using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KnifejugglerEffect : Effect {

    public override void onFriendlyMinionSummoned()
    {
        Player enemy = CardController.getController().getOtherPlayer(self.Player);
        List<BoardCharacter> enemyChars = EffectsController.getAllCharacters(enemy);
        int choice = Random.Range(0, enemyChars.Count);
        Debug.Log(choice);
        EffectsController.dealDamage(enemyChars[choice], 1);
    }
}
