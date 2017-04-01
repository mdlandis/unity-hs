using UnityEngine;
using System.Collections;

public class CrueltaskmasterEffect : Effect {

    public override bool getBattlecryTarget()
    {
        if (EffectsController.getAllMinions().Count > 0)

        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public override bool setBattlecryTarget(BoardCharacter c)
    {
        if(c.type == "minion")
        {
            battlecryTarget = c;
            return true;
        }
        else
        {
            return false;
        }
        

    }

    public override void onPlay()
    {
        if(battlecryTarget != null)
        {
            EffectsController.dealDamage(battlecryTarget, 1);
            battlecryTarget.bAttack += 2;
        }
        
    }
}
