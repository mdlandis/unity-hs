using UnityEngine;
using System.Collections;

public class GrommashhellscreamEffect : Effect {

    public override void getStatts()
    {
        self.ostatts.Add("charge");
    }

    public override void aura()
    {
        if(self.cHealth < self.bHealth)
        {
            self.changeCAttack(6);
        }
    }
}
