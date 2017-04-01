using UnityEngine;
using System.Collections;

public class HauntedcreeperEffect : Effect
{


    public override void onDeath()
    {
        Player owner = self.Player;
        Card summon = CardController.getCardByFilename("spectralspider");
        for (int i = 0; i < 2; i++)
        {
            owner.summonCard(summon);
        }
    }

}
