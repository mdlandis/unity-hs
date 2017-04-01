using UnityEngine;
using System.Collections;
using System;

public class HeroPower : MonoBehaviour {

    public HeroPowerCard heroPowerCard;
    public Player player;
    public bool used;
    public bool playable;
    public HeroPowerEffect effect;

    public void SetCard(HeroPowerCard _heroPowerCard)
    {
        heroPowerCard = _heroPowerCard;
        GetComponent<SpriteRenderer>().sprite = _heroPowerCard.image;
        string effectFile = heroPowerCard.filename + "Effect";
        effectFile = effectFile.Substring(0, 1).ToUpper() + effectFile.Substring(1);
        try
        {
            gameObject.AddComponent(System.Type.GetType(effectFile));
            effect = (HeroPowerEffect)GetComponent(System.Type.GetType(effectFile));
            effect.owner = player;
        }
        catch (Exception e)
        {
            gameObject.AddComponent<HeroPowerEffect>();
            effect = GetComponent<HeroPowerEffect>();
        }

        
    }

    void Update()
    {
        if(player != null && player.playernum != 3)
        {
            transform.position = new Vector3(15.5f, 2.2f * (player.playernum * 2 - 3), -2);
            checkIfPlayable();
            updateGlow();
        }


        
        
    }

    public void checkIfPlayable()
    {

        
        if(player.activeplayer && player.cMana >= heroPowerCard.cost && !used)
        {
            playable = true;
        }
        else
        {
            playable = false;
        }
    }

    public void updateGlow()
    {
        Behaviour halo = (Behaviour)GetComponent("Halo");
        if (playable)
        {
            halo.enabled = true;
        }
        else
        {
            halo.enabled = false;
        }
    }

    public void OnMouseDown()
    {
        if(playable)
        {
            effect.onUse();
            used = true;
            player.cMana -= heroPowerCard.cost;
        }
    }

    public void OnMouseOver()
    {

        PreviewRenderer pv = CardController.getController().preview.GetComponent<PreviewRenderer>();

        if (pv.timeTillPreview < pv.timeTillFullSize)
        {
            if (pv.timeTillPreview > pv.timeTillBegin)
            {
                pv.GetComponent<SpriteRenderer>().sprite = heroPowerCard.image;
                pv.GetComponent<SpriteRenderer>().enabled = true;
            }

            pv.timeTillPreview += 1000 * Time.deltaTime;
        }
        else
        {
            pv.GetComponent<SpriteRenderer>().sprite = heroPowerCard.image;
            pv.GetComponent<SpriteRenderer>().enabled = true;
        }

    }

    public void OnMouseExit()
    {
        PreviewRenderer pv = CardController.getController().preview.GetComponent<PreviewRenderer>();
        pv.GetComponent<SpriteRenderer>().enabled = false;
        pv.timeTillPreview = 0;
    }


}
