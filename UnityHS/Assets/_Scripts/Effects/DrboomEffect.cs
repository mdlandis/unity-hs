using UnityEngine;
using System.Collections;

public class DrboomEffect: Effect
{


	public override void onPlay()
    {

        Player owner = self.Player;
        Card summon = CardController.getCardByFilename("boombot");
        for(int i = 0; i < 2; i++)
        {
            owner.summonCard(summon);
        }
        
    }

}
