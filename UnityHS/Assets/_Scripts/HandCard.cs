using UnityEngine;
using System.Collections;
using System;

public class HandCard : MonoBehaviour {

    public Player Player;
    public Card card;

    public int oCost;
    public int cCost;

    public TextMesh manaText;

    public bool canBeClicked;

    public Vector3 originalPos;
    public Vector3 posToMoveTo;
    public float speed;

    public Spell spellRepresentative;
    public Minion representative;
    public SpriteRenderer cardBack;
    public int posToAddAt;

    public bool needToAimBattlecry;
    public bool aimingBattlecry;

    public bool started;
    public bool vanishing;

    
	//each step is split into methods because
    //if a subclass behaves differently it can just override the methods instead of the update method
	void Update () {


        updateCardBack();

        updateManaCost();

        updateManaText();

        canBeClicked = checkIfPlayable();

        updateGlow();

        processMovement();

        processBattlecryTarget();

        if(vanishing)
        {
            progressVanish();
        }
        
	}

    //cover up the card if it belongs to the other player
    public void updateCardBack()
    {
        if (Player.activeplayer && !Player.deck.Contains(this))
        {
            cardBack.enabled = false;
        }
        else
        {
            cardBack.enabled = true;
        }
    }

    //mana costs can change, so prepare to re-update it every frame
    public void updateManaCost()
    {
        cCost = 0;
        cCost += oCost;
    }

    //after mana cost has been updated, change the color based on the new value
    public void updateManaText()
    {
        manaText.text = cCost + "";
        if(cCost < card.cost)
        {
            manaText.color = Color.green;
        }
        else if(cCost  > card.cost)
        {
            manaText.color = Color.red;
        }
        else
        {
            manaText.color = Color.white;
        }
    }

    //if this card is playable, make it glow
    public void updateGlow()
    {
        Behaviour halo = (Behaviour)GetComponent("Halo");
        if (canBeClicked)
        {
            halo.enabled = true;
        }
        else
        {
            halo.enabled = false;
        }
    }

    //based on what type of card this is, check the factors that determine whether or not it is playable
    public bool checkIfPlayable()
    {
        if(card.type == "minion")
        {
            if (Player.board.Count < 7 && Player.cMana >= cCost && Player.activeplayer == Player && Player.hand.Contains(this) && transform.position == posToMoveTo && started == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if(card.type == "spell")
        {

            if (Player.cMana >= cCost && Player.activeplayer == Player && Player.hand.Contains(this) && transform.position == posToMoveTo && started == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if(card.type == "weapon")
        {
            if (Player.cMana >= cCost && Player.activeplayer == Player && Player.hand.Contains(this) && transform.position == posToMoveTo && started == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }
        
    }

    //move each frame
    public void processMovement()
    {
        //if player is holding this card and it hasn't started to be played yet
        if (Player.currentObj == gameObject && Player.playerInput.currentAction == "playinghandcard" && !started)
        {
            Vector3 v3 = Input.mousePosition;
            v3 = Camera.main.ScreenToWorldPoint(v3);
            v3.z = -5;
            transform.position = new Vector3(v3.x, v3.y, v3.z);
        }
        else
        {
            //if it is in its correct position after being played, do nothing
            if (Player.currentObj == gameObject && posToMoveTo == transform.position && Player.playerInput.currentAction == "aimingbattlecry")
            {

            }

            //if it is not where it needs to be
            else if (posToMoveTo != transform.position && !vanishing)
            {
                //move in that direction
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, posToMoveTo, step);
                if (transform.position == posToMoveTo)
                {
                    if (started)
                    {
                        Player.playHandCard(this);
                    }
                }

            }

            else
            {
                //otherwise move it to hand or deck/where it belongs
                Player p = Player;
                if (p.hand.Contains(this))
                {
                    int indx = p.hand.IndexOf(this);
                    posToMoveTo = new Vector3(p.startHandWidth + indx * p.handWidthDifference, p.handHeight, -3);
                }
                else if (p.deck.Contains(this))
                {
                    posToMoveTo = new Vector3(p.deckPosX, p.deckPosY, 0);
                }
            }
        }

    }

    public void processBattlecryTarget()
    {
        //if it was aiming a battlecry but now it isn't
        if(needToAimBattlecry == true && aimingBattlecry == false)
        {
            if(representative != null)
            {
                //summon it if it's a minion
                Player.completeSummon(this, representative);
                Player.currentObj = null;
                Destroy(gameObject);
            }
            else if (spellRepresentative != null)
            {
                //play it if its a spell
                Player.completeSpellcast(this, spellRepresentative);
            }
        }            
    }

    public void SetCard(Card _card)
    {
        card = _card;
        GetComponent<SpriteRenderer>().sprite = card.image;
        oCost = card.cost;
        cCost = card.cost;
    }

    public void changeOCost(int num)
    {
        oCost += num;
        if(oCost < 0)
        {
            oCost = 0;
        }
    }

    public void setPosToMoveTo(Vector3 newPos)
    {
        originalPos = transform.position;
        posToMoveTo = newPos;
    }
   
    //when clicked
    public void OnMouseDown()
    {
        if(CardController.getController().between)
        {
            return;
        }
        if(Player.hand.Contains(this))
            //tell the input controller that it got clicked
            Player.playerInput.SendMessage("clickHandCard", this);        
    }

    //when moused over, show a larger version for better readability
    public void OnMouseEnter()
    {
        if ((Player.currentObj == null || Player.currentObj == gameObject) && Player.activeplayer)
        {
            PreviewRenderer pv = CardController.getController().preview.GetComponent<PreviewRenderer>();
            pv.GetComponent<SpriteRenderer>().sprite = card.image;
            pv.GetComponent<SpriteRenderer>().enabled = true;
            pv.timeTillPreview = pv.timeTillFullSize;
        }
    }

    //when mouse leaves, get rid of the preview
    public void OnMouseExit()
    {
        if(Player.currentObj != gameObject && Player.currentObj == null)
        {
            PreviewRenderer pv = CardController.getController().preview.GetComponent<PreviewRenderer>();
            pv.GetComponent<SpriteRenderer>().enabled = false;
            pv.timeTillPreview = 0;       
        }       
    }

    //make it fade out
    public void progressVanish()
    {
        Color tmp = GetComponent<SpriteRenderer>().color;
        tmp.a -= 0.03f;
        GetComponent<SpriteRenderer>().color = tmp;
        if (tmp.a <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}

