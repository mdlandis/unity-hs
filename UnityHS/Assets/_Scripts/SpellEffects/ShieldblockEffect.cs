using UnityEngine;
using System.Collections;

public class ShieldblockEffect : SpellEffect {

	public override void onPlay()
    {
        caster.hero.armor += 5;
        caster.draw(1);
    }

}
