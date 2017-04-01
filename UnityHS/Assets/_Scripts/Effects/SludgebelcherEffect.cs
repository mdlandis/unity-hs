using UnityEngine;
using System.Collections;

public class SludgebelcherEffect : Effect {

    public override void getStatts()
    {
        self.ostatts.Add("taunt");
    }

    public override void onDeath()
    {
        Player owner = self.Player;
        Card summon = CardController.getCardByFilename("slime");
        owner.summonCard(summon);
        
    }
}
