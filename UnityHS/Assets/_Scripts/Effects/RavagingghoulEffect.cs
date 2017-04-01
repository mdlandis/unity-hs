using UnityEngine;
using System.Collections;

public class RavagingghoulEffect: Effect {

	 public override void onPlay()
    {
        foreach(Minion m in EffectsController.getAllMinions())
        {
            if(m != self)
            {
                EffectsController.dealDamage(m, 1);
            }
            
        }
    }
}
