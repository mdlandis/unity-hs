using UnityEngine;
using System.Collections;

public class PoweroverwhelmingEffectBuff : Effect {

    public override void onPlay()
    {
        self.bAttack += 4;
        self.bHealth += 4;
        self.cHealth += 4;
    }

    public override void aura()
    {
        
    }

    public override void onFriendlyTurnEnd()
    {
        EffectsController.destroyMinion(self);
    }

}
