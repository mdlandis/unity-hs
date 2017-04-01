using UnityEngine;
using System.Collections;

public class WeaponEffect : MonoBehaviour {

    public Hero user;
    
    public virtual bool getBattlecryTarget()
    {
        return false;
    }

    public virtual bool setBattlecryTarget()
    {
        return true;
    }

    public virtual void onPlay()
    {

    }

    public virtual void onDeath()
    {

    }

}
