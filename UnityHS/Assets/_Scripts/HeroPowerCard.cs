using UnityEngine;
using System.Collections;

public class HeroPowerCard {

    public string heroClass;
    public string name;
    public string filename;
    public Sprite image;
    public int cost;


    public HeroPowerCard(string _heroClass, string _name, string _filename, int _cost)
    {
        heroClass = _heroClass;
        name = _name;
        filename = _filename;
        cost = _cost;
        image = Resources.Load<Sprite>("heropower/" + _filename);
        
    }

}
