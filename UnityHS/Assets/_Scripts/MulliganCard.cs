using UnityEngine;
using System.Collections;

public class MulliganCard : MonoBehaviour {

    public SpriteRenderer image;
    public Mulligan m;
    public int pos;
    public bool replace;


    public void OnMouseDown()
    {
        Behaviour halo = (Behaviour)GetComponent("Halo");
        replace = !halo.enabled;
        halo.enabled = replace;
    }

    
}
