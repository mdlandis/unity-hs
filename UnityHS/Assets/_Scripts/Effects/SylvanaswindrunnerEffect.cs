using UnityEngine;
using System.Collections;

public class SylvanaswindrunnerEffect : Effect {

	public override void onDeath()
    {
        Player owner = self.Player;
        Player enemy = CardController.getController().getOtherPlayer(self.Player);

        if(enemy.board.Count > 0)
        {
            int choice = Random.Range(0, enemy.board.Count);
            Minion picked = enemy.board[choice];
            EffectsController.takeControlOfMinion(picked, owner, enemy);
        }

        
    }
}
