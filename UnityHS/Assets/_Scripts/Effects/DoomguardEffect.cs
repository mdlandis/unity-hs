using UnityEngine;
using System.Collections;

public class DoomguardEffect : Effect {

    public override void onPlay()
    {
        EffectsController.discardRandom(self.Player, 2);
    }

    public override void getStatts()
    {
        self.ostatts.Add("charge");
        
    }
}
