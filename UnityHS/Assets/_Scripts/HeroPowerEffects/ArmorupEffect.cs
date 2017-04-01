using UnityEngine;
using System.Collections;

public class ArmorupEffect : HeroPowerEffect {

    public override void onUse()
    {
        owner.hero.armor += 2;
    }

	
}
