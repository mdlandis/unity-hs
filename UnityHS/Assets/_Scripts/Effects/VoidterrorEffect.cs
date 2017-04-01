using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VoidterrorEffect : Effect {

	public override void onPlay()
    {
        List<Minion> board = self.Player.board;
        int pos = board.IndexOf(self);
        if(pos != board.Count - 1)
        {
            Minion toTheRight = board[pos + 1];
            self.bAttack += toTheRight.cAttack;
            self.bHealth += toTheRight.cHealth;
            self.cHealth += toTheRight.cHealth;
            EffectsController.destroyMinion(board[pos + 1]);

        }

        if (pos != 0)
        {
            Minion toTheLeft = board[pos - 1];
            self.bAttack += toTheLeft.cAttack;
            self.bHealth += toTheLeft.cHealth;
            self.cHealth += toTheLeft.cHealth;
            EffectsController.destroyMinion(board[pos - 1]);

        }

    }
}
