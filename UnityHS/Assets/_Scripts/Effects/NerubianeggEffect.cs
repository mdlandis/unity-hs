using UnityEngine;
using System.Collections;

public class NerubianeggEffect : Effect {

    public override void onDeath()
    {
        Player owner = self.Player;
        Card summon = CardController.getCardByFilename("nerubian");
        owner.summonCard(summon);
    }
}
