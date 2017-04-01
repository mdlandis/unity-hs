using UnityEngine;
using System.Collections;
using System;

public class Weapon : MonoBehaviour
{

    public Player player;
    public Card card;
    public WeaponEffect effect;

    public int cAttack;
    public int cDurability;

    public SpriteRenderer aBox;
    public SpriteRenderer hBox;

    public TextMesh aText;
    public TextMesh hText;

    void Update()
    {
        if(cDurability <= 0)
        {
            EffectsController.destroyWeapon(this);
        }
        else if (cDurability < card.health)
        {
            hText.color = Color.red;
        }
        else
        {
            hText.color = Color.green;
        }

        aText.text = cAttack + "";
        hText.text = cDurability + "";
    }

    public void activateOnPlay()
    {
        effect.onPlay();
    }

    public void SetCard(Card c)
    {
        card = c;
        cAttack = card.attack;
        cDurability = card.health;
        GetComponent<SpriteRenderer>().sprite = card.image;
        string effectFile = card.filename + "Effect";
        effectFile = effectFile.Substring(0, 1).ToUpper() + effectFile.Substring(1);
        try
        {
            gameObject.AddComponent(System.Type.GetType(effectFile));
            effect = (WeaponEffect)GetComponent(System.Type.GetType(effectFile));
        }
        catch (Exception e)
        {
            gameObject.AddComponent<WeaponEffect>();
            effect = GetComponent<WeaponEffect>();
        }

        effect.user = player.hero;
    }



}
