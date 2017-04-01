using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LordxaviusEffect : Effect {

    public override void onDeath()
    {
        Debug.Log("DO I EVEN GET HERE?");
        List<Minion> board = self.Player.board;
        List<Minion> validtargets = new List<Minion>();
        Debug.Log(board.Count);

        foreach(Minion m in board)
        {
            if(m != self)
            {
                validtargets.Add(m);
            }
        }

        if(validtargets.Count > 0)
        {
            
            int choice = Random.Range(0, validtargets.Count);
            Debug.Log(choice + "!!!!!");
            EffectsController.destroyMinion(validtargets[choice]);
            Card summon = CardController.getCardByFilename("lordxavius");
            self.Player.summonCard(summon);
        }
        
    }
}
