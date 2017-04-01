using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrawlEffect : SpellEffect {

    public override void onPlay()
    {
        List<Minion> allMinions = EffectsController.getAllMinions();
        if(allMinions.Count > 0)
        {
            int choice = Random.Range(0, allMinions.Count);
            Minion winner = allMinions[choice];
            List<Minion> deadMinions = new List<Minion>();
            foreach(Minion m in allMinions)
            {
                if (m != winner)
                {
                    deadMinions.Add(m);
                }
            }

            foreach(Minion m in deadMinions)
            {
                EffectsController.destroyMinion(m);
            }
        }
    }

}
