using UnityEngine;
using System.Collections;

public class ElvenarcherEffect : Effect {

    public override bool getBattlecryTarget()
    {
        return true;
    }

    public override bool setBattlecryTarget(BoardCharacter c)
    {
        battlecryTarget = c;
        return true;
            
    }

    public override void onPlay()
    {
        EffectsController.dealDamage(battlecryTarget, 1);
    }


}
