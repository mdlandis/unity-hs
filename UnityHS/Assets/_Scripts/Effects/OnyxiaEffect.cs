using UnityEngine;
using System.Collections;

public class OnyxiaEffect : Effect
{

    public override void onPlay()
    {
        Player owner = self.Player;
        Card summon = CardController.getCardByFilename("whelp");
        for (int i = 0; i < 7; i++)
        {
            owner.summonCard(summon);
        }
    }
}
