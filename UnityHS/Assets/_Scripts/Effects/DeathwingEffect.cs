using UnityEngine;
using System.Collections;

public class DeathwingEffect : Effect {

    public override void onPlay()
    {
        EffectsController.discardFirst(self.Player, self.Player.hand.Count);
        foreach(Minion m in EffectsController.getAllMinions())
        {
            if(m != self)
            {
                EffectsController.destroyMinion(m);
            }
            
        }



    }
}
