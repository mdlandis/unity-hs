using UnityEngine;
using System.Collections;

public class ShieldmaidenEffect : Effect {

	public override void onPlay()
    {
        self.Player.hero.armor += 5;
    }
}
