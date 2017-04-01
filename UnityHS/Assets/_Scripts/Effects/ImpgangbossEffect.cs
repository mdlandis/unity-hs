using UnityEngine;
using System.Collections;

public class ImpgangbossEffect : Effect {

    public override void onSelfDamaged()
    {
        Player owner = self.Player;
        Card summon = CardController.getCardByFilename("imp");
        owner.summonCard(summon);
    }

}
