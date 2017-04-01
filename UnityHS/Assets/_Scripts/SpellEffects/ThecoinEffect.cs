using UnityEngine;
using System.Collections;

public class ThecoinEffect : SpellEffect {

	public override void onPlay()
    {
        caster.cMana++;
    }
}
