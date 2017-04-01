using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StampedingkodoEffect : Effect
{


    public override void onPlay()
    {
        List<Minion> minions = CardController.getController().getOtherPlayer(self.Player).board;
        List<Minion> validTargets = new List<Minion>();
        foreach(Minion m in minions)
        {
            if(m.cAttack <= 2)
            {
                validTargets.Add(m);

            }
        }
        if(validTargets.Count > 0)
        {
            int choice = Random.Range(0, validTargets.Count);
            Minion m = validTargets[choice];
            EffectsController.destroyMinion(m);
        }
        

    }

}
