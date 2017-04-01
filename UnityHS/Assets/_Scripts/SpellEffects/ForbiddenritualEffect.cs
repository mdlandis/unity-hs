using UnityEngine;
using System.Collections;

public class ForbiddenritualEffect : SpellEffect {

    public override void onPlay()
    {
        int mana = self.player.cMana;
        for(int i = 0; i < mana; i++)
        {
            self.player.summonCard(CardController.getCardByFilename("ickytentacle"));
        }
        self.player.cMana = 0;
    }

}
