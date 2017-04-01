using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mulligan : MonoBehaviour {

    public MulliganCard[] cards3 = new MulliganCard[3];
    public MulliganCard[] cards4 = new MulliganCard[4];

    public MulliganCard[] usingCollection;

    public Player p;

	void Start()
    {
        if(p.playernum == 1)
        {
            usingCollection = cards3;
        }
        else
        {
            usingCollection = cards4;
        }
    }
	void Update () {
        int indx = 0;
        
	    foreach(MulliganCard m in usingCollection)
        {
            m.transform.position = new Vector3(-10 + (7 * indx), 0, -10);
            indx++;
        }
	}

    public void getCards()
    {
        for(int i = 0; i < usingCollection.Length; i++)
        {
            usingCollection[i].image.sprite = p.deck[i].card.image;
            usingCollection[i].pos = i;
        }
        changeVisibility(false);        

    }

    public void changeVisibility(bool what)
    {
        if(p.playernum == 2)
        {
            if(!what)
            {
                Debug.Log("Player 2 going invis");
            }
        }
        
        foreach (MulliganCard m in usingCollection)
        {
            m.image.enabled = what;
            m.GetComponent<Collider2D>().enabled = what;
            if(!what)
            {
                Behaviour halo = (Behaviour)m.GetComponent("Halo");
                halo.enabled = false;
            }
        }

    }

    public void finishMulligan()
    {
        List<HandCard> cardsToKeep = new List<HandCard>();

        foreach(MulliganCard m in usingCollection)
        {
            if(!m.replace)
            {
                cardsToKeep.Add(p.deck[m.pos]);
            }
        }

        foreach(HandCard m in cardsToKeep)
        {
            p.hand.Add(m);
            p.deck.Remove(m);
        }

        CardController.Shuffle<HandCard>(p.deck);
        p.draw(usingCollection.Length - cardsToKeep.Count);
    }
}
