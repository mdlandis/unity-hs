using UnityEngine;
using System.Collections;
using System;

public class Spell : MonoBehaviour {

    public Player player;
    public Card card;
    public SpellEffect effect;

    void Update()
    {

    }

    public void activateOnPlay()
    {
        effect.onPlay();
    }

    public void SetCard(Card c)
    {
        card = c;
        string effectFile = card.filename + "Effect";
        effectFile = effectFile.Substring(0, 1).ToUpper() + effectFile.Substring(1);
        try
        {
            gameObject.AddComponent(System.Type.GetType(effectFile));
            effect = (SpellEffect)GetComponent(System.Type.GetType(effectFile));
        }
        catch (Exception e)
        {
            gameObject.AddComponent<SpellEffect>();
            effect = GetComponent<SpellEffect>();
        }

        effect.caster = player;
        effect.self = this;
    }

    

}
