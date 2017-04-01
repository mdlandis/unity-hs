using UnityEngine;
using System.Collections;

public class ArmorsmithEffect : Effect {

    public override void onFriendlyMinionDamaged()
    {
        self.Player.hero.armor += 1;
    }

}
