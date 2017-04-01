using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BiggamehunterEffect : Effect {

    public override bool getBattlecryTarget()
    {
        List<Minion> allMinions = EffectsController.getAllMinions();
        List<Minion> validTargets = new List<Minion>();
        foreach(Minion m in allMinions)
        {
            if(m.cAttack >= 7)
            {
                return true;
            }
        }
        return false;
        
    }

    public override bool setBattlecryTarget(BoardCharacter c)
    {
        if(c.cAttack >= 7)
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
            EffectsController.destroyMinion((Minion) battlecryTarget);
        }
        
    }

}
