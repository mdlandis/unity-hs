using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VoidcallerEffect : Effect {

	public override void onDeath()
    {
        List<HandCard> validTargets = new List<HandCard>();
        foreach(HandCard c in self.Player.hand)
        {
            if(c.card.tribe == "demon")
            {
                validTargets.Add(c);
            }
        }

        if(validTargets.Count > 0)
        {
            int choice = Random.Range(0, validTargets.Count - 1);
            EffectsController.putHandCardInPlay(self.Player, validTargets[choice]);
            Debug.Log(choice);
            Debug.Log(validTargets[choice].card.filename);
        }
        
    }
}
