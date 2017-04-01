using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{

    public CardController controller;
    public Aimer target;
    public string currentAction;
    public Player p;
    public bool cheatsEnabled;
    public KeyCode[] cheatCode;
    public int pos;

    // Use this for initialization
    void Start()
    {
        pos = 0;
        p = controller.activePlayer;
        cheatCode = new KeyCode[10] {KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.B, KeyCode.A};
    }

    // Update is called once per frame
    void Update()
    {
        p = controller.activePlayer;
        if(p.currentObj)
        {
            if (p.currentObj.GetComponent<HandCard>())
            {
                if(p.currentObj.GetComponent<HandCard>().aimingBattlecry)
                {
                    currentAction = "aimingbattlecry";
                }
                else
                {
                    currentAction = "playinghandcard";
                }
                
            }
            else if (p.currentObj.GetComponent<BoardCharacter>())
            {
                currentAction = "attacking";
            }
        }
        
        else
        {
            currentAction = "none";
        }

        if (p.currentObj != null)
        {
            if(currentAction == "playinghandcard")
            {
                Vector3 v3 = Input.mousePosition;
                v3 = Camera.main.ScreenToWorldPoint(v3);
                bool inBounds;
                if (p.playernum == 1)
                {
                    inBounds = v3.y > -6 && v3.y < 0;
                }
                else
                {
                    inBounds = v3.y > 0 && v3.y < 6;
                }
                if(inBounds)
                {
                    
                    if (p.board.Count > 0)
                    {

                        float cX = v3.x;

                        int indx = 0;

                        for (int i = 0; i < p.board.Count; i++)
                        {
                            if (p.board[i].transform.position.x < cX)
                            {

                                indx += 1;

                            }
                        }


                        p.previewPos = indx;

                    }
                    else
                    {
                        p.previewPos = 0;
                    }
                }
                
            }
            if(currentAction == "attacking")
            {
                target.GetComponent<SpriteRenderer>().enabled = true;

                Vector3 v3 = Input.mousePosition;
                v3 = Camera.main.ScreenToWorldPoint(v3);
                v3.z = -5;
                target.transform.position = new Vector3(v3.x, v3.y, v3.z);
                Cursor.visible = false;
            }
            if(currentAction == "aimingbattlecry")
            {
                target.GetComponent<SpriteRenderer>().enabled = true;

                Vector3 v3 = Input.mousePosition;
                v3 = Camera.main.ScreenToWorldPoint(v3);
                v3.z = -5;
                target.transform.position = new Vector3(v3.x, v3.y, v3.z);
                //Cursor.visible = false;
            }
        }
        else
        {
            Cursor.visible = true;
            target.GetComponent<SpriteRenderer>().enabled = false;
            p.previewPos = -1;
        
        }

        if(cheatsEnabled)
        {
            if (Input.GetKeyDown(KeyCode.D) == true)
            {
                p.draw(1);
            }

            if (Input.GetKeyDown(KeyCode.M) == true)
            {
                p.mMana += 1;
            }

            if (Input.GetKeyDown(KeyCode.C) == true)
            {
                p.cMana += 1;
            }

            if (Input.GetKeyDown(KeyCode.S) == true)
            {
                controller.nextTurn();
            }
        }
        else
        {
            if(pos >= cheatCode.Length)
            {
                cheatsEnabled = true;
            }
            else if(Input.GetKeyDown(cheatCode[pos]))
            {
                pos++;
            }
        }
        
    }

    public void clickHandCard(HandCard cardClicked)
    {
  
        if (!p.currentObj)
        {
            p.currentObj = cardClicked.gameObject;
            cardClicked.originalPos = cardClicked.transform.position;
        }
        else if (p.currentObj == cardClicked.gameObject)
        {

            bool drop = true;
            Vector3 height = Input.mousePosition;
            height = Camera.main.ScreenToWorldPoint(height);

            bool inBounds;
            if (p.playernum == 1)
            {
                inBounds = height.y > -6 && height.y < 0;
            }
            else
            {
                inBounds = height.y > 0 && height.y < 6;
            }

            if (!inBounds || !(cardClicked.cCost <= p.cMana))
            {
                
                cardClicked.posToMoveTo = cardClicked.originalPos;
            }
            else
            {
                if (cardClicked.cCost <= p.cMana)
                {
                    p.startPlayHandCard(cardClicked);
                    drop = false;
                }
                
            }
            if(drop)
            {
                p.currentObj = null;
            }

        }
    }

    public void clickBoardCharacter(BoardCharacter charClicked)
    {
        if (!p.currentObj)
        {
            p.currentObj = charClicked.gameObject;
            p.originalPos = p.currentObj.transform.position;
            foreach (BoardCharacter c in charClicked.getValidAttackTargets())
            {
                Debug.Log(c.card.filename);
            }
        }
    }

    public void clickAttackTarget(GameObject g)
    {
        if(g.GetComponent<BoardCharacter>())
        {
            
            BoardCharacter target = g.GetComponent<BoardCharacter>();
            BoardCharacter attacker = p.currentObj.GetComponent<BoardCharacter>();
            foreach(BoardCharacter c in attacker.getValidAttackTargets())
            {
                Debug.Log(c.card.filename);
            }
            

            if (attacker.getValidAttackTargets().Contains(target))
            {
                p.attack(attacker, target);
            }
            
        }
       
        
        p.currentObj = null;
    }

    public void clickBattlecryTarget(GameObject g)
    {
        if (g.GetComponent<BoardCharacter>())
        {

            BoardCharacter c = g.GetComponent<BoardCharacter>();
            if (p.currentObj.GetComponent<HandCard>().representative != null)
            {
                if (p.currentObj.GetComponent<HandCard>().representative.effect.setBattlecryTarget(c))
                {
                    p.currentObj.GetComponent<HandCard>().aimingBattlecry = false;
                    Debug.Log("Aimed a battlecry");
                }
            }
            else if (p.currentObj.GetComponent<HandCard>().spellRepresentative != null)
            {
                if (p.currentObj.GetComponent<HandCard>().spellRepresentative.effect.setSpellTarget(c))
                {
                    Debug.Log("Aimed a spell");
                    p.currentObj.GetComponent<HandCard>().aimingBattlecry = false;
                }
            }


        }
        else
        {
            HandCard cardCancelled = p.currentObj.GetComponent<HandCard>();
            cancelBattlecry(cardCancelled);
            
        }
    }

    public void cancelBattlecry(HandCard cardCancelled)
    {
        if(cardCancelled.representative)
        {
            Destroy(cardCancelled.representative.gameObject);
        }
        if(cardCancelled.spellRepresentative)
        {
            Destroy(cardCancelled.spellRepresentative.gameObject);
        }
        
        cardCancelled.representative = null;
        cardCancelled.spellRepresentative = null;
        cardCancelled.aimingBattlecry = false;
        cardCancelled.needToAimBattlecry = false;
        cardCancelled.started = false;
        p.currentObj = null;
    }

}