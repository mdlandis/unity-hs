using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoombotEffect : Effect
{

    public override void onDeath()
    {

        List<BoardCharacter> characters = EffectsController.getAllCharacters(CardController.getController().getOtherPlayer(self.Player));
        int choice = Random.Range(0, characters.Count);
        int damage = Random.Range(1, 5);
        EffectsController.dealDamage(characters[choice], damage);

    }

}
