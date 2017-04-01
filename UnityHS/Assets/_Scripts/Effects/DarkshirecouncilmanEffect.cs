using UnityEngine;
using System.Collections;

public class DarkshirecouncilmanEffect : Effect {

    public override void onFriendlyMinionSummoned()
    {
        self.bAttack += 1;
    }

}
