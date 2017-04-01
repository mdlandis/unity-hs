using UnityEngine;
using System.Collections;

public class AbusivesergeantEffect : Effect {

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
            gameObject.AddComponent<AbusivesergeantEffectBuff>();
            Effect effectToGive = gameObject.GetComponent<AbusivesergeantEffectBuff>();
            effectToGive.self = (Minion)battlecryTarget;
            ((Minion)battlecryTarget).effectsToConsider.Add(effectToGive);
            Destroy(GetComponent<AbusivesergeantEffectBuff>());
        }
        
    }

}
