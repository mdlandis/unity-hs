using UnityEngine;
using System.Collections;

public class BloodhoofbraveEffect : Effect {

    public override void getStatts()
    {
        self.ostatts.Add("taunt");
    }

    public override void aura()
    {
        if(self.cHealth < self.bHealth)
        {
            self.cAttack += 3;
        }
    }
}
