using UnityEngine;
using System.Collections;

public class FlameimpEffect : Effect {

    public override void onPlay()
    {
        EffectsController.dealDamage(self.Player.hero, 3);
    }

}
