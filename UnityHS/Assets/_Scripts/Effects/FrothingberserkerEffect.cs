using UnityEngine;
using System.Collections;

public class FrothingberserkerEffect : Effect {

    public override void onMinionDamaged()
    {  
        self.bAttack += 1;
    }
}
