using UnityEngine;
using System.Collections;

public class PoweroverwhelmingEffect : SpellEffect {

    public override bool getSpellTarget()
    {
        return true;
    }

    public override bool setSpellTarget(BoardCharacter c)
    {
        if (c.type == "minion" && self.player.board.Contains((Minion)c))
        {
            spellTarget = c;
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void onPlay()
    {
        string effectFile = self.card.filename + "EffectBuff";
        effectFile = effectFile.Substring(0, 1).ToUpper() + effectFile.Substring(1);
        gameObject.AddComponent(System.Type.GetType(effectFile));
        Effect effectToGive = gameObject.GetComponent<PoweroverwhelmingEffectBuff>();
        effectToGive.self = (Minion) spellTarget;
        ((Minion)spellTarget).effectsToConsider.Add(effectToGive);
        effectToGive.onPlay();
        Destroy(GetComponent(System.Type.GetType(effectFile)));
    }





}
