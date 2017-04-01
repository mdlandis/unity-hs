using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RenojacksonEffect : Effect {

    public override void onPlay()
    {
        bool shouldIHeal = true;
        List<HandCard> foundcards = new List<HandCard>();
        foreach(HandCard c in self.Player.deck)
        {
            bool match = false;
            foreach(HandCard foundcard in foundcards)
            {
                if (foundcard.card.filename == c.card.filename)
                {
                    shouldIHeal = false;
                    match = true;
                }
            }
            if(!match)
            {
                foundcards.Add(c);
                foreach(HandCard car in foundcards)
                {
                    Debug.Log(car.card.filename);
                }
            }
        }
        if(shouldIHeal)
        {
            EffectsController.restoreHealth(self.Player.hero, 30);
        }

    }

}
