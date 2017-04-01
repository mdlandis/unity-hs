using UnityEngine;
using System.Collections;

public class GrimpatronEffect : Effect {

	public override void onSelfDamaged()
    {
        if(self.cHealth > 0)
        {
            Card summon = CardController.getCardByFilename("grimpatron");
            self.Player.summonCard(summon);
        }
    }
}
