using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DefenderofargusEffect : Effect {

	public override void onPlay()
    {
        List<Minion> board = self.Player.board;
        int pos = board.IndexOf(self);
        if (pos != board.Count - 1)
        {
            Minion toTheRight = board[pos + 1];
            toTheRight.ostatts.Add("taunt");
            toTheRight.bAttack += 1;
            toTheRight.bHealth += 1;
            toTheRight.cHealth += 1;

        }

        if (pos != 0)
        {
            Minion toTheLeft = board[pos - 1];
            toTheLeft.ostatts.Add("taunt");
            toTheLeft.bAttack += 1;
            toTheLeft.bHealth += 1;
            toTheLeft.cHealth += 1;

        }
    }
}
