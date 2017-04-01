using UnityEngine;
using System.Collections;

public class TurnButton : MonoBehaviour {

    public TextMesh label;
    public CardController c;

	
	void Update () {
        if(c.activePlayer.playernum == 1)
        {
            if(c.between)
            {
                label.text = "P2 Start";
            }
            else
            {
                label.text = "P1 End";
            }
        }
        else
        {
            if(c.between)
            {
                label.text = "P1 Start";
            }
            else
            {
                label.text = "P2 End";
            }
        }

	}

    public void OnMouseDown()
    {
        c.nextTurn();
    }
}
