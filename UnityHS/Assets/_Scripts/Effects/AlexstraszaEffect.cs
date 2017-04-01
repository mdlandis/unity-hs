using UnityEngine;
using System.Collections;

public class AlexstraszaEffect : Effect {

    public override bool getBattlecryTarget()
    {
        return true;
    }

    public override bool setBattlecryTarget(BoardCharacter c)
    {
        if(c.type == "hero")
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

        battlecryTarget.cHealth = 15;
    }
}
