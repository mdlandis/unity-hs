using UnityEngine;
using System.Collections;

public class EmperorthaurissanEffect : Effect {

    public override void onFriendlyTurnEnd()
    {
        foreach(HandCard c in self.Player.hand)
        {
            c.changeOCost(-1);
        }
    }


}
