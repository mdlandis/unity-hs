using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class BoardCharacter : MonoBehaviour {

    public Card card;
    public Player Player;

    public int cAttack;
    public int cHealth;
    public int bAttack;
    public int bHealth;

    public SpriteRenderer aBox;
    public SpriteRenderer hBox;

    public TextMesh aText;
    public TextMesh hText;

    public SpriteRenderer divineshield;
    public SpriteRenderer taunt;

    public string type;

    public bool canAttack = false;
    public bool asleep;

    private Vector3 originalScale;
    private Vector3 newScale;

    public Vector3 posToMoveTo;
    public Vector3 originalPos;

    public BoardCharacter attackTarget;

    

    public bool moving = false;
    public float speed = 45.0f;

    public bool dead;


    public List<string> ostatts;
    public List<string> cstatts;    


    // Use this for initialization
    void Start() {
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update() {

        GetComponent<Collider2D>().enabled = false;
        GetComponent<Collider2D>().enabled = true;

        if(cAttack == 0 || !Player.activeplayer)
        {
            canAttack = false;
        }

        processScale();
        processMovement();
        
        updateStatts();
        checkDead();

        updateStats();
        doOtherStuff();
        updateStatBoxes();

        

        alignStatBoxes();
        updateGlow();


        if (dead)
        {
            progressDeathAnimation();
        }

        GetComponent<Collider2D>().enabled = false;
        GetComponent<Collider2D>().enabled = true;


    }

    
    public virtual void doOtherStuff()
    {

    }

    public void processScale()
    {

        if (Player.currentObj == gameObject)
        {
            newScale = new Vector3(1.2f, 1.2f, 0);
        }
        else
        {
            newScale = originalScale;
        }

        for (int i = 0; i < 3; i++)
        {
            if (Math.Round(transform.localScale.x, 2) != Math.Round(newScale.x, 2))
            {
                int direction = 1;
                if (transform.localScale.x >= newScale.x)
                {
                    direction = -1;
                }
                transform.localScale += new Vector3(0.01f * direction, 0.01f * direction, 0);
            }
        }
    }



    public virtual void processMovement()
    {
        
        
    }

    public virtual void alignStatBoxes()
    {

    }

    public void updateStats()
    {
        cAttack = 0;
        cAttack += bAttack;
        if(type == "hero")
        {
            if(((Hero)this).equipped != null && Player.activeplayer)
            {
                cAttack += ((Hero)this).equipped.cAttack;
            }
        }
    }

    public void updateStatBoxes()
    {
        


        aText.text = "" + cAttack;
        hText.text = "" + cHealth;



        if (cHealth < bHealth)
        {
            hText.color = Color.red;
        }
        
        else if (cHealth > card.health)
        {
            hText.color = Color.green;
        }
        else
        {
            hText.color = Color.white;
        }
        if (cAttack > card.attack)
        {
            aText.color = Color.green;
        }
        else
        {
            aText.color = Color.white;
        }
    }

    public void updateGlow()
    {
        Behaviour halo = (Behaviour)GetComponent("Halo");
        if (canAttack)
        {
            halo.enabled = true;
        }
        else
        {
            halo.enabled = false;
        }
    }

    public void changeCAttack(int num)
    {
        cAttack += num;
        updateStatBoxes();
    }

    public virtual void checkDead()
    {
        
    }

    public void updateStatts()
    {
        divineshield.enabled = false;
        taunt.enabled = false;
        cstatts.Clear();
        foreach(string statt in ostatts)
        {
            cstatts.Add(statt);
        }
        foreach (string statt in cstatts)
        {
            if(statt == "divineshield")
            {
                divineshield.enabled = true;
            }
            if(statt == "taunt")
            {
                taunt.enabled = true;
            }
            if(statt == "charge")
            {
                if(asleep)
                {
                    asleep = false;
                    canAttack = true;
                }
            }
            
        }
    }

    public void progressDeathAnimation()
    {
        Color tmp = GetComponent<SpriteRenderer>().color;
        tmp.a -= 0.03f;

        GetComponent<SpriteRenderer>().color = tmp;
        if (tmp.a <= 0.0f)
        {
            die();

        }
    }

    public virtual void die()
    {
        Destroy(gameObject);
    }

    public List<BoardCharacter> getValidAttackTargets()
    {
        List<BoardCharacter> list = new List<BoardCharacter>();
        bool needToRecalc = false;

        Player enemy = CardController.getController().getOtherPlayer(Player);
        list.Add(enemy.hero);
        foreach(BoardCharacter c in enemy.board)
        {
            if(c.cstatts.Contains("taunt"))
            {
                needToRecalc = true;
                break;
                
            }
            else
            {
                list.Add(c);
            }
        }

        if(needToRecalc)
        {
            list.Clear();
            if(enemy.hero.cstatts.Contains("taunt"))
            {
                list.Add(enemy.hero);
            }

            foreach(BoardCharacter c in enemy.board)
            {
                if(c.cstatts.Contains("taunt"))
                {
                    list.Add(c);
                }
            }

        }



        return list;
    }

    public virtual void takeDamage(int num)
    {
        cHealth -= num;
    }

    public virtual void restoreHealth(int amount)
    {
        cHealth += amount;
        if(cHealth > bHealth)
        {
            cHealth = bHealth;
        }
    }

    public virtual void SetCard(Card _card)
    {
        
        card = _card;
        GetComponent<SpriteRenderer>().sprite = card.image;
        cAttack = card.attack;
        cHealth = card.health;
        bAttack = cAttack;
        bHealth = cHealth;

    }

    public void OnMouseDown()
    {
        Debug.Log("ow");
        if (canAttack && Player.currentObj == null)
        {
            Player.playerInput.SendMessage("clickBoardCharacter", this);
        }

        else if (Player.playerInput.currentAction == "attacking")
        {
            Player.playerInput.SendMessage("clickAttackTarget", gameObject);
        }     

        else if (Player.playerInput.currentAction == "aimingbattlecry")
        {
            Player.playerInput.SendMessage("clickBattlecryTarget", gameObject);
            
        }
    }

    public void OnMouseOver()
    {
        
        PreviewRenderer pv = CardController.getController().preview.GetComponent<PreviewRenderer>();
       
        if (pv.timeTillPreview < pv.timeTillFullSize)
        {
            if(pv.timeTillPreview > pv.timeTillBegin)
            {
                pv.GetComponent<SpriteRenderer>().sprite = card.image;
                pv.GetComponent<SpriteRenderer>().enabled = true;
            }

            pv.timeTillPreview += 1000 * Time.deltaTime;
        }
        else
        {
            pv.GetComponent<SpriteRenderer>().sprite = card.image;
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