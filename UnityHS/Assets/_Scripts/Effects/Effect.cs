using UnityEngine;
using System.Collections;
using System;

public class Effect : MonoBehaviour
{

    public Minion self;

    public BoardCharacter battlecryTarget;

    public virtual bool getBattlecryTarget()
    {
        battlecryTarget = null;
        return false;
    }

    public virtual bool setBattlecryTarget(BoardCharacter c)
    {
        return true;
    }

    public virtual void getStatts()
    {

    }

    public virtual void onPlay()
    {

    }

    public virtual void aura()
    {

    }

    public virtual void onDeath()
    {

    }

    public virtual void onSelfDamaged()
    {

    }

    public virtual void onFriendlyMinionDamaged()
    {

    }

    public virtual void onEnemyMinionDamaged()
    {

    }

    public virtual void onMinionDamaged()
    {

    }

    public virtual void onFriendlyMinionSummoned()
    {

    }
    
    public virtual void onEnemyMinionSummoned()
    {

    }

    public virtual void onMinionSummoned()
    {

    }

    public virtual void onFriendlyMinionPlayed()
    {
        
    }

    public virtual void onEnemyMinionPlayed()
    {

    }

    public virtual void onMinionPlayed()
    {

    }

    public virtual void onFriendlyTurnEnd()
    {

    }

    public virtual void onFriendlyTurnBegin()
    {

    }

    public virtual void onEnemyTurnEnd()
    {

    }

    public virtual void onEnemyTurnBegin()
    {

    }

    public virtual void onTurnEnd()
    {

    }

    public virtual void onTurnBegin()
    {

    }
    

}
