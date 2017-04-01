using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;
using System;
using System.Collections.Generic;

//represents one of the two players.
public class Player : MonoBehaviour {

    public GameObject currentObj;
    public Vector3 originalPos;

    public Hero hero;
    public HeroPower heroPower;

    public Card[] decklist = new Card[30];

    public List<HandCard> deck = new List<HandCard>();    
    public List<HandCard> hand = new List<HandCard>();

    public List<Minion> board = new List<Minion>();
    public List<Minion> boardByPlay = new List<Minion>();

    public int mMana;
    public int cMana;

    public TextMesh cManaT;
    public TextMesh mManaT;

    public int playernum;
    public bool activeplayer;

    public PlayerInput playerInput;
    public Mulligan mulligan;

    public int fatigue = 1;

    public float startHandWidth;
    public float handWidthDifference;
    public float handHeight;

    public float startBoardPos;
    public float boardWidthDifference;
    public float boardHeight;

    public float deckPosX;
    public float deckPosY;

    public int previewPos;

   
       
    void Start () {      
        currentObj = null;
        cMana = 0;
        mMana = cMana;
    }

    //creates a list of Cards for the CardController to turn into HandCards
    //First card is hero 
    public bool processDecklist(string fileName)
    {
        
        try
        {
            string line;
            StreamReader theReader = new StreamReader(fileName, Encoding.Default);
            
            using (theReader)
            {
                int count = 0;
                do
                {
                    line = theReader.ReadLine();
                    
                    if (line != null)
                    {
                        if (count == 0)
                        {
                            hero = playerInput.controller.createHero(line, this);
                            heroPower = playerInput.controller.createHeroPower(hero.card.heroClass, this);
                            hero.Player = this;
                            heroPower.player = this;

                            

                        }
                        else
                        {
                            
                            decklist[count-1] = CardController.getCardByFilename(line);
                        }
                        
                    }
                    count++;
                }
                while (line != null);
                theReader.Close();
                

                return true;
            }

        }
        
        catch (Exception e)
        {
            Console.WriteLine("{0}\n", e.Message);
            return false;
        }
    }

    


    
    void Update()
    {
        //hard cap mana at 10
        if (mMana > 10)
        {
            mMana = 10;
        }
        if (cMana > 10)
        {
            cMana = 10;
        }
        
            
        //update display    
        cManaT.text = "" + cMana;
        mManaT.text = "" + mMana;
        
        
        
    }

    //called at the start of each turn
    public void startTurn()
    {

        //become the active player
        activeplayer = true;

        //if I need to mulligan, get ready and dont do anything else
        if(CardController.getController().mulliganTurn)
        {
            mulligan.changeVisibility(true);
            return;
        }
        
        //increment and refill mana, draw a card
        mMana += 1;
        cMana = mMana;
        draw(1);

        //refresh once-per-turn stuff
        heroPower.used = false;
        hero.canAttack = true;
        hero.attacked = false;

        foreach(Minion m in board)
        {
            //refersh once-per-turn stuff
            m.asleep = false;
            m.canAttack = true;

            //trigger start turn effects
            m.effect.onFriendlyTurnBegin();
            m.effect.onTurnBegin();
            
        }

        //trigger start turn effects
        foreach (Minion em in playerInput.controller.getOtherPlayer(this).board)
        {
            em.effect.onEnemyTurnBegin();
            em.effect.onTurnBegin();
        }
    }

    //called at the end of each turn
    public void endTurn()
    {
        //if I was just doing a mulligan, finish it and dont mulligan ever again
        if(playerInput.controller.mulliganTurn)
        {
            mulligan.finishMulligan();
            mulligan.changeVisibility(false);
            if(playernum == 2)
            {
                playerInput.controller.mulliganTurn = false;
                Card thecoin = CardController.getCardByFilename("thecoin");
                hand.Add(CardController.getController().createHandCard(thecoin, this));
            }
        }
        
        //make sure once-per-turn things are not usuable until refreshed
        hero.attacked = true;
        activeplayer = false;

        //trigger end of turn effects, escape concurrentmodification in case any of them destroy themselves
        Minion[] currentMinions = new Minion[7];
        for(int i = 0; i < board.Count; i++)
        {
            currentMinions[i] = board[i];
            
        }

        for(int i = 0; i < currentMinions.Length; i++)
        {
            if(currentMinions[i] != null)
            {
                Minion m = currentMinions[i];
                m.activateOnFriendlyTurnEnd();
                m.effect.onTurnEnd();
                m.canAttack = false;
            }
        }
        foreach(Minion m in playerInput.controller.getOtherPlayer(this).board)
        {
            m.effect.onEnemyTurnEnd();
            m.effect.onTurnEnd();
        }
        if (playerInput.currentAction == "aimingbattlecry") { 
            playerInput.cancelBattlecry(currentObj.GetComponent<HandCard>()); 
        }
        currentObj = null;
        
    }

    public void draw(int num)
    {
        
            //use a coroutine (thread) so that there is delay between drawing multiple cards
            StartCoroutine(drawCards(num));
            

         
    }




    //actually draw a card
    IEnumerator drawCards(int num)
    {
        for (int i = 0; i < num; i++)
        {
            //if I have cards left, draw, else fatigue
            if (deck.Count > 0)
            {
                HandCard cardToDraw = deck[0];
                deck.RemoveAt(0);
                //if I have space, add, otherwise, discard
                if (hand.Count < 10)
                {
                    hand.Add(cardToDraw);
                    Vector3 temp = playerInput.controller.getNextHandPos(this, cardToDraw);
                    temp.x -= 3;
                    cardToDraw.posToMoveTo = temp;
                    
                }
                else
                {
                    Debug.Log("Too many cards in hand");
                    Destroy(cardToDraw.gameObject);
                }
            }
            else
            {
                EffectsController.dealDamage(hero, fatigue);
                fatigue += 1;
                Debug.Log("No cards in deck");
            }
            yield return new WaitForSeconds(1);
        }
    }   

    //for when I dont want to animate it
    public void addCardsToHand(int num)
    {
        for (int i = 0; i < num; i++)
        {
            if (deck.Count > 0)
            {
                HandCard cardToDraw = deck[0];
                deck.RemoveAt(0);
                if (hand.Count < 10)
                {
                    hand.Add(cardToDraw);
                }
                else
                {
                    Debug.Log("Too many cards in hand");
                }
            }
            else
            {
                EffectsController.dealDamage(hero, fatigue);
                fatigue += 1;
                Debug.Log("No cards in deck");
            }
        }
    }

    //directly summon a card without activating battlecry or allowing position to be chosen
    public void summonCard(Card cardToPlay)
    {

        if(board.Count < 7)
        {
            Minion newMinion = playerInput.controller.createMinion(cardToPlay, this);
            int num = board.Count;

            board.Add(newMinion);
            boardByPlay.Add(newMinion);

            newMinion.originalPos = playerInput.controller.getBoardPos(this, num);
            newMinion.transform.position = newMinion.originalPos;
            newMinion.posToMoveTo = newMinion.transform.position;

            foreach (Minion fm in board)
            {
                if (fm != newMinion)
                {
                    fm.effect.onFriendlyMinionSummoned();
                    fm.effect.onMinionSummoned();
                }
            }
            foreach (Minion em in playerInput.controller.getOtherPlayer(this).board)
            {
                em.effect.onEnemyMinionSummoned();
                em.effect.onMinionSummoned();
            }
        }


    }

    //oh god
    //the extremely overcomplicated process of playing a card
    
    //this first method is triggered once a card is placed on the board
    //it figures out what type it is and moves it based on that
    //once it finishes moving, it will call the next step
    public void startPlayHandCard(HandCard cardToPlay)
    {
        //weapons go to the hero portrait, spells and minions go to the board
        if(cardToPlay.card.type != "weapon")
        {
            float startPos = startBoardPos - (board.Count + 1) * boardWidthDifference / 2;
            cardToPlay.posToMoveTo = new Vector3(startPos + previewPos * boardWidthDifference, 2.5f * (playernum * 2 - 3), -2);
            cardToPlay.started = true;
        }
        else
        {
            Vector3 temp = hero.transform.position;
            temp += new Vector3(-3, 0, 0);
            cardToPlay.posToMoveTo = temp;
            cardToPlay.started = true;
        }
        
    }


    //the card has now finished its animation
    //if it doesn't need to aim anything, it will immediately move into the final step
    //if it does, it will put the card into an "aimingbattlecry" state and not move into the final step until the effect is aimed
    public void playHandCard(HandCard cardToPlay)
    {
       
        
        //if it's a minion, it has to pay attention to how the minions are being pushed out of the way and prepare to insert itself
        //into the board at the previewpos
        if(cardToPlay.card.type == "minion")
        {

           
            cardToPlay.posToAddAt = previewPos;


            //create the minion
            Minion newMinion = playerInput.controller.createMinion(cardToPlay.card, this);

            //hide some stuff that is visible by default
            newMinion.divineshield.enabled = false;
            newMinion.taunt.enabled = false;

            //check if the new minion needs to aim something
            bool needToAim = newMinion.effect.getBattlecryTarget();


            if (needToAim)
            {
                //tell the card to aim its battlecry and link the new minion to it
                cardToPlay.aimingBattlecry = true;
                cardToPlay.needToAimBattlecry = true;
                cardToPlay.representative = newMinion;

                //make the new minion invisible so it does not appear until after the aiming
                newMinion.GetComponent<SpriteRenderer>().enabled = false;
                newMinion.aBox.enabled = false;
                newMinion.hBox.enabled = false;
                newMinion.aText.GetComponent<MeshRenderer>().enabled = false;
                newMinion.hText.GetComponent<MeshRenderer>().enabled = false;
                newMinion.GetComponent<BoxCollider2D>().enabled = false;


            }
            else
            {
                //didn't need to aim, immediately finish
                completeSummon(cardToPlay, newMinion);
            }
        }
        else if(cardToPlay.card.type == "spell")
        {
            
            //similar to minion summoning, but no need to create a minion or make it invisible
            Spell newSpell = playerInput.controller.createSpell(cardToPlay.card, this);
            bool needToAim = newSpell.effect.getSpellTarget();

            if(needToAim)
            {
                
                cardToPlay.aimingBattlecry = true;
                cardToPlay.needToAimBattlecry = true;
                cardToPlay.spellRepresentative = newSpell;
            }
            else
            {
                completeSpellcast(cardToPlay, newSpell);
            }
               
        }
        else if(cardToPlay.card.type == "weapon")
        {
            Weapon newWeapon = playerInput.controller.createWeapon(cardToPlay.card, this);
            bool needToAim = newWeapon.effect.getBattlecryTarget();
            if(needToAim)
            {
                //weapons with aimable effects will not be implemented
            }
            else
            {
                completeWeaponplay(cardToPlay, newWeapon);
            }
        }

        

    }

    public void completeSpellcast(HandCard cardToPlay, Spell s)
    {

        //remove the card from hand, subtract the cost, drop the object
        hand.Remove(cardToPlay);
        cMana -= cardToPlay.cCost;

        currentObj = null;

        //activate the effect
        s.activateOnPlay();

        //make it disappear
        cardToPlay.vanishing = true;
        cardToPlay.needToAimBattlecry = false;
    }

    public void completeWeaponplay(HandCard cardToPlay, Weapon w)
    {

        //take the card out of your hand and subtract the cost
        hand.Remove(cardToPlay);
        cMana -= cardToPlay.cCost;

        //drop it
        currentObj = null;    

        //activate the effect
        w.activateOnPlay();

        //move it
        w.transform.position = cardToPlay.transform.position;


        //equip it
        hero.changeWeapon(w);

        //get rid of the HandCard
        Destroy(cardToPlay.gameObject);

    }

    public void completeSummon(HandCard cardToPlay, Minion m)
    {
        //remove the card from hand and subtract the mana cost
        
        hand.Remove(cardToPlay);
        cMana -= cardToPlay.cCost;

        
        //drop it
        currentObj = null;

        //add it into the board if there's any room
        board.Insert(cardToPlay.posToAddAt, m);

        //add it into the boardbyplay
        boardByPlay.Add(m);

        //activate the effect
        m.activateOnPlay();



        //reset previewpos
        previewPos = -1;
            

        //move it to where it needs to be
        m.transform.position = cardToPlay.transform.position;
        m.posToMoveTo = m.transform.position;
        m.originalPos = m.transform.position;

        //can't attack on the turn it's played
        m.canAttack = false;
        Behaviour halo = (Behaviour)m.GetComponent("Halo");     
        halo.enabled = false;

        //make it and all the things attached to it visible
        m.GetComponent<SpriteRenderer>().enabled = true;
        m.aBox.enabled = true;
        m.hBox.enabled = true;
        m.aText.GetComponent<MeshRenderer>().enabled = true;
        m.hText.GetComponent<MeshRenderer>().enabled = true;
        m.GetComponent<BoxCollider2D>().enabled = true;

        //trigger onMinionplayed effects
        foreach(Minion fm in board)
        {
            if(fm != m)
            {
                fm.effect.onFriendlyMinionPlayed();
                fm.effect.onFriendlyMinionSummoned();
                fm.effect.onMinionPlayed();
                fm.effect.onMinionSummoned();
            }
        }
        foreach(Minion em in playerInput.controller.getOtherPlayer(this).board)
        {
            em.effect.onEnemyMinionPlayed();
            em.effect.onEnemyMinionSummoned();
            em.effect.onMinionPlayed();
            em.effect.onMinionSummoned();
        }

        //get rid of the HandCard
        Destroy(cardToPlay.gameObject);
        
    }

    //boardcharacters attack each other
    public void attack(BoardCharacter attacker, BoardCharacter target)
    {
        //give the attacker information about who its attacking so it knows how to
        //call the completeAttack method
        attacker.attackTarget = target;
        attacker.canAttack = false;
        
        //start moving
        attacker.originalPos = attacker.transform.position;
        attacker.posToMoveTo = target.transform.position;
    }
    public void completeAttack(BoardCharacter attacker, BoardCharacter target)
    {
        //move back to starting location & reset
        attacker.posToMoveTo = attacker.originalPos;
        attacker.attackTarget = null;

        //deal damage to each other
        EffectsController.dealDamage(target, attacker.cAttack);
        EffectsController.dealDamage(attacker, target.cAttack);

        //if a hero is the one attacking, subtract durability from weapon
        if (attacker.type == "hero")
        {
            Hero attackingHero = (Hero)attacker;
            attackingHero.attacked = true;
            if (attackingHero.equipped != null)
            {
                attackingHero.equipped.cDurability--;
            }

        }
    }

    public void killMinion(Minion minion)
    {
        //make the minion use its death animation
        minion.dead = true;

        //remove from board
        board.Remove(minion);
        boardByPlay.Remove(minion);

        //trigger deathrattle
        minion.activateOnDeath();

        
        
    }

    public void killHero(Hero hero)
    {
        hero.dead = true;
    }
}
