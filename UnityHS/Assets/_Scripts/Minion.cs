using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Minion : BoardCharacter {

    public Effect effect;
    public List<Effect> effectsToConsider = new List<Effect>();

    public override void doOtherStuff()
    {
        activateAura();
    }

    public void activateAura()
    {
        effect.aura();
        foreach(Effect oeffect in effectsToConsider)
        {
            oeffect.aura();
        }
    }

    public void activateOnPlay()
    {
        effect.onPlay();    
    }

    public void activateOnDeath()
    {
        effect.onDeath();
    }

    public void activateOnFriendlyTurnEnd()
    {
        effect.onFriendlyTurnEnd();
        foreach(Effect oeffect in effectsToConsider)
        {
            oeffect.onFriendlyTurnEnd();
        }
    }

    public override void processMovement()
    {
        
        if (posToMoveTo != transform.position)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, posToMoveTo, step);
            if (transform.position == posToMoveTo)
            {
                if (attackTarget != null)
                {
                    Player.completeAttack(this, attackTarget);
                }
            }
        }
        else
        {
            Player p = Player;
            if (p.board.Contains(this))
            {
                int indx = p.board.IndexOf(this);
                posToMoveTo = CardController.getController().getBoardPos(Player, indx);
            }
        }
    }

    public override void SetCard(Card _card)
    {

        card = _card;
        GetComponent<SpriteRenderer>().sprite = card.image;
        cAttack = card.attack;
        cHealth = card.health;
        bAttack = cAttack;
        bHealth = cHealth;
        string effectFile = card.filename + "Effect";
        effectFile = effectFile.Substring(0, 1).ToUpper() + effectFile.Substring(1);
        try
        {
            gameObject.AddComponent(System.Type.GetType(effectFile));
            effect = (Effect)GetComponent(System.Type.GetType(effectFile));
        }
        catch (Exception e)
        {
            gameObject.AddComponent<Effect>();
            effect = GetComponent<Effect>();
        }
        
        effect.self = this;
        effect.getStatts();
        asleep = true;
    }

    public override void checkDead()
    {
        if (cHealth <= 0 && !dead)
        {
            Player.killMinion(this);
        }
    }
}
