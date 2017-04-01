using UnityEngine;
using System.Collections;

public class AbusivesergeantEffectBuff : Effect {

    public int duration = 1;

    public override void aura()
    {
        if(duration != 0)
        {
            self.cAttack += 2;
        }
        
    }

    public override void onFriendlyTurnEnd()
    {
        duration = 0;
    }

}
