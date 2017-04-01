using UnityEngine;
using System.Collections;

public class SpellEffect : MonoBehaviour {

    public Player caster;
    public BoardCharacter spellTarget;
    public Spell self;

    public virtual bool getSpellTarget()
    {
        return false;
    }

    public virtual bool setSpellTarget(BoardCharacter c)
    {
        spellTarget = c;
        return true;

    }
    

	public virtual void onPlay()
    {

    }
}
