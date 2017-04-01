using UnityEngine;
using System.Collections;

public class Card {

    public string heroClass;
    public string name;
    public Sprite image;
    public string type;
    public int cost;
    public int attack;
    public int health;
    public string rarity;
    public string filename;
    public string tribe;

	public Card (string _heroClass, string _name, string _filename, string _type, int _cost, int _attack, int _health, string _rarity, string _tribe)
    {
        heroClass = _heroClass;
        name = _name;
        
        type = _type;
        cost = _cost;
        attack = _attack;
        health = _health;
        rarity = _rarity;
        filename = _filename;
        tribe = _tribe;

        if (_type == "hero")
        {
            image = Resources.Load<Sprite>("hero/" + _filename);
            
        }
        else
        {
            image = Resources.Load<Sprite>(_heroClass + "/" + _filename);
        }
    }

    public Sprite getSprite()
    {
        return image;

    }
}
