using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;

public class CardController : MonoBehaviour {

    //load in the prefabs so that we can instantiate copies of them from here.

    public HandCard baseCard;

    public Minion baseMinion;

    public Hero baseHero;

    public Spell baseSpell;

    public HeroPower baseHeroPower;

    public Weapon baseWeapon;

    

    public bool between;


    //card lists, searchable with methods
    private static List<Card> cardList = new List<Card>();
    private static List<Card> heroList = new List<Card>();
    private static List<HeroPowerCard> heroPowerList = new List<HeroPowerCard>();


    public DamageLabel lb;

    public bool mulliganTurn = true;
    
    //has knowledge of the two players and who is currently taking their turn
    public Player player1;
    public Player player2;
    public static Player[] players = new Player[2];
    public Player activePlayer;

    public SpriteRenderer preview;


    //helper method that shuffles a List. Code not written by me.
    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }


    //method to read in input from text files. Code not written by me (the inside is, but not the Streamreader part).
    private static bool processSomething(string type, string fileName)
    {

        try
        {
            string line;

            StreamReader theReader = new StreamReader(fileName, Encoding.Default);

            using (theReader)
            {

                do
                {
                    line = theReader.ReadLine();
                    if (line != null)
                    {
                        string[] entries;
                        if (type == "cardlist")
                        {
                            entries = line.Split('\t');
                            if (entries.Length > 0)
                            {
                                String tribe;
                                if (entries.Length < 9)
                                {
                                    tribe = "none";
                                }
                                else
                                {
                                    tribe = entries[8];
                                }
                                Card newCard = new Card(entries[0], entries[1], entries[2], entries[3], Convert.ToInt32(entries[4]), Convert.ToInt32(entries[5]), Convert.ToInt32(entries[6]), entries[7], tribe);
                                cardList.Add(newCard);
                            }
                        }
                        else if (type == "herolist")
                        {
                            entries = line.Split('\t');
                            if (entries.Length > 0)
                            {
                                String tribe;
                                if (entries.Length < 8)
                                {
                                    tribe = "none";
                                }
                                else
                                {
                                    tribe = entries[7];
                                }
                                Card newHero = new Card(entries[0], entries[1], entries[2], entries[3], Convert.ToInt32(entries[4]), Convert.ToInt32(entries[5]), Convert.ToInt32(entries[6]), entries[7], tribe);
                                heroList.Add(newHero);
                            }
                        }
                        else if (type == "heropowerlist")
                        {
                            entries = line.Split('\t');
                            if (entries.Length > 0)
                            {
                                HeroPowerCard newHeroPower = new HeroPowerCard(entries[0], entries[1], entries[2], Convert.ToInt32(entries[3]));
                                heroPowerList.Add(newHeroPower);

                            }
                        }


                    }
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

    
    void Awake()
    {
        //read in all the card info from the text files
        processSomething("cardlist", getCorrectPath("cardlist.txt"));
        processSomething("cardlist", getCorrectPath("tokenlist.txt"));
        processSomething("herolist", getCorrectPath("herolist.txt"));
        processSomething("heropowerlist", getCorrectPath("heropowerlist.txt"));
        
        //to be extended with a database 
        //...
        //...maybe not
    }

    public static string getCorrectPath(String filename)
    {
        FileInfo file = new FileInfo(filename);
        Debug.Log(file.FullName);
        string outstring = file.FullName.Substring(0, file.FullName.Length - (filename.Length + 1));
        outstring += "/Assets/Resources/Lists/";
        outstring += filename;
        Debug.Log(outstring);
        return outstring;
    }

    void Start() {

        players[0] = player1;
        players[1] = player2;
              
        //initialize the two players
       
        int indx;
        foreach (Player player in players)
        {
            
            player.processDecklist(getCorrectPath("player" + player.playernum + "decklist.txt"));


            player.deckPosX = 12.0f;
            player.deckPosY = 7.0f * (player.playernum * 2 - 3);
            indx = 0;
            player.startHandWidth = -19.0F;
            player.handWidthDifference = 3.0f;
            player.handHeight = 7.0f * (player.playernum * 2 - 3);

            player.startBoardPos = -1.5f;
            player.boardWidthDifference = 3.0f;
            player.boardHeight = 2.5f * (player.playernum * 2 - 3);

            foreach (Card card in player.decklist)
            {
                if(card == null)
                {
                    break;
                }
                HandCard newCard = Instantiate(baseCard) as HandCard;
                newCard.Player = player;
                newCard.SetCard(card);
                newCard.transform.position = new Vector3(player.deckPosX, player.deckPosY, -3);
                newCard.cardBack.enabled = true;
                indx++;
                player.deck.Add(newCard);
                
            }

            Shuffle<HandCard>(player.deck);
            player.mulligan.getCards();


            player.hero.transform.position = new Vector3(12, 2.5f * (player.playernum * 2 - 3) - .2f, 0);
            player.hero.posToMoveTo = player.hero.transform.position;
            
        }


        activePlayer = player1;
        player1.activeplayer = true;
        player2.activeplayer = false;
        

        player1.startTurn();

    }


    public Vector3 getNextHandPos(Player p, HandCard c)
    {
        int indx = p.hand.Count;
        return new Vector3(p.startHandWidth + indx * p.handWidthDifference, c.transform.position.y, c.originalPos.z);
    }

    public void nextTurn()
    {
        if(!between)
        {
            activePlayer.endTurn();
            between = true;
           
        }
        else
        {
            Player next = getOtherPlayer(activePlayer);
            next.startTurn();
            activePlayer = next;
            between = false;
        }
       
    }

    public static CardController getController()
    {
        return GameObject.FindGameObjectsWithTag("cardcontroller")[0].GetComponent<CardController>();
    }

    public Vector3 getBoardPos(Player p, int num)
    {
        int count = p.board.Count;
        int effectiveNum = num;
        if (p.previewPos != -1)
        {
            if (p.previewPos <= num)
            {
                effectiveNum++;
            }
            count += 1;
        }
        
        float startPos = p.startBoardPos - count * p.boardWidthDifference / 2;
        
        return new Vector3(startPos + effectiveNum * p.boardWidthDifference, 2.5f * (p.playernum * 2 - 3), -2);
    }

    public HandCard createHandCard(Card card, Player p)
    {
        HandCard newCard = Instantiate(baseCard) as HandCard;
        newCard.Player = p;
        newCard.SetCard(card);
        newCard.cardBack.enabled = true;

        return newCard;
    }

    public Minion createMinion(Card card, Player p)
    {
        Minion m = Instantiate(baseMinion) as Minion;
        m.SetCard(card);
        m.type = "minion";
        m.Player = p;
        return m;
    }

    public Hero createHero(string classname, Player p)
    {
        Hero hero = Instantiate(baseHero) as Hero;
        Card heroCard = getHeroByFilename(classname);
        hero.type = "hero";
        hero.SetCard(heroCard);
        hero.Player = p;
        hero.transform.localScale = new Vector3(1.3f, 1.3f, 0);
        return hero;

    }

    public HeroPower createHeroPower(string classname, Player p)
    {
        HeroPower newHeroPower = Instantiate(baseHeroPower) as HeroPower;
        HeroPowerCard heroPowerCard = getHeroPowerCardByClassname(classname);
        newHeroPower.player = p;
        newHeroPower.SetCard(heroPowerCard);
        
        return newHeroPower;
    }

    public Spell createSpell(Card card, Player p)
    {
        Spell spell = Instantiate(baseSpell) as Spell;
        spell.player = p;
        spell.SetCard(card);
        

        return spell;
    }

    public Weapon createWeapon(Card card, Player p)
    {
        Weapon weapon = Instantiate(baseWeapon) as Weapon;
        weapon.player = p;
        weapon.SetCard(card);

        return weapon;
    }

    public Player getOtherPlayer(Player p)
    {
        if(p.playernum == 1)
        {
            return player2;
        }
        else
        {
            return player1;
        }
    }



    public static Card getCardByFilename(string name)
    {
        foreach (Card card in cardList)
        {
            if (card.filename == name)
            {
                return card;
            }
        }
        return null;
    }

    public static Card getHeroByFilename(string name)
    {
        foreach (Card card in heroList)
        {
            if (card.filename == name)
            {
                return card;
            }
        }
        return null;
    }

    public static HeroPowerCard getHeroPowerCardByClassname(string name)
    {
        foreach (HeroPowerCard card in heroPowerList)
        {
            if (card.heroClass == name)
            {
                return card;
            }
        }
        return null;
    }

  
}
