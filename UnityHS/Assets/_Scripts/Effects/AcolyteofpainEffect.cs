using UnityEngine;
using System.Collections;

public class AcolyteofpainEffect : Effect {

    public override void onSelfDamaged()
    {
        self.Player.draw(1);
    }
}
