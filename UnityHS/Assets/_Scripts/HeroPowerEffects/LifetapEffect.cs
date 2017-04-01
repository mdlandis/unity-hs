using UnityEngine;
using System.Collections;

public class LifetapEffect : HeroPowerEffect {

    public override void onUse()
    {
        Hero hero = owner.hero;
        EffectsController.dealDamage(hero, 2);
        owner.draw(1);
    }
}
