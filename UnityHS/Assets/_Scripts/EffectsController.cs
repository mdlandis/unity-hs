using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectsController {

    public static Player p1 = GameObject.FindGameObjectsWithTag("p1")[0].GetComponent<Player>();
    public static Player p2 = GameObject.FindGameObjectsWithTag("p2")[0].GetComponent<Player>();
    public static DamageLabel lb = CardController.getController().lb;

    public static int dealDamage(BoardCharacter target, int amount)
    {
        int dealt = amount;
        if(target.cstatts.Contains("divineshield"))
        {
            target.cstatts.Remove("divineshield");
            target.ostatts.Remove("divineshield");
            dealt = 0;
        }
        else
        {
            target.takeDamage(amount);
            dealt = amount;
        }

        DamageLabel label = GameObject.Instantiate(lb) as DamageLabel;
        label.transform.position = target.transform.position;
        label.GetComponent<TextMesh>().text = "-" + dealt;
        label.attached = target;


        if(dealt > 0)
        { 
            if(target.type == "minion")
            {
                Minion t = (Minion)target;
                t.effect.onSelfDamaged();
                sendTriggerTo(t.Player.board, "onFriendlyMinionDamaged");
                sendTriggerTo(CardController.getController().getOtherPlayer(t.Player).board, "onEnemyMinionDamaged");
                sendTriggerTo(getAllMinions(), "onMinionDamaged");

            }
        }


        return dealt;
    }

    public static void dealSpellDamage(BoardCharacter target, int amount)
    {
        dealDamage(target, amount);
    }

    public static int restoreHealth(BoardCharacter target, int amount)
    {
        int healed = amount;
        if (target.cHealth == target.bHealth)
        {
            
            healed = 0;
        }
        else
        {
            target.restoreHealth(amount);
            healed = amount;
        }

        DamageLabel label = GameObject.Instantiate(lb) as DamageLabel;
        label.transform.position = target.transform.position;
        label.GetComponent<TextMesh>().text = "+" + healed;
        label.attached = target;

        return healed;
       
        
    }

    public static List<BoardCharacter> getAllCharacters(Player p)
    {
        List<BoardCharacter> list = new List<BoardCharacter>();
        foreach(Minion m in p.board)
        {
            if(m.cHealth != 0)
                list.Add((BoardCharacter)m);
        }
        list.Add((BoardCharacter)p.hero);
        return list;
    }

    public static List<Minion> getAllMinions(Player p)
    {
        List<Minion> list = new List<Minion>();
        foreach (Minion m in p.boardByPlay)
        {
            if (m.cHealth != 0)
                list.Add(m);
        }
        return list;
    }

    public static List<Minion> getAllMinions()
    {
        List<Minion> list = new List<Minion>();
        foreach (Minion m in p1.boardByPlay)
        {
            if (m.cHealth != 0)
                list.Add(m);
        }
        foreach (Minion m in p2.boardByPlay)
        {
            if (m.cHealth != 0)
                list.Add(m);
        }
        return list;
    }

    public static List<BoardCharacter> getAllCharacters()
    {
        List<BoardCharacter> list = new List<BoardCharacter>();
        foreach (Minion m in p1.board)
        {
            if (m.cHealth != 0)
                list.Add((BoardCharacter)m);
        }
        list.Add((BoardCharacter)p1.hero);
        foreach (Minion m in p2.board)
        {
            if (m.cHealth != 0)
                list.Add((BoardCharacter)m);
        }
        list.Add((BoardCharacter)p2.hero);
        return list;
    }

    public static void destroyMinion(Minion m)
    {
        m.Player.killMinion(m);
    }

    public static void destroyWeapon(Weapon w)
    {
        w.effect.onDeath();
        GameObject.Destroy(w.gameObject);
    }

    public static void discardFirst(Player p, int num)
    {
        for(int i = 0; i < num; i++)
        {
            HandCard c = p.hand[0];
            p.hand.RemoveAt(0);
            GameObject.Destroy(c.gameObject);
        }
    }

    public static void discardRandom(Player p, int num)
    {
        for(int i = 0; i < num; i++)
        {
            if(p.hand.Count > 0)
            {
                int choice = Random.Range(0, p.hand.Count);
                HandCard c = p.hand[choice];
                p.hand.Remove(c);
                GameObject.Destroy(c.gameObject);
            }
        }
    }

    public static void sendTriggerTo(List<BoardCharacter> targets, string trigger)
    {
        foreach(BoardCharacter target in targets)
        {
            target.SendMessage(trigger);
        }
    }

    public static void sendTriggerTo(List<Minion> targets, string trigger)
    {
        foreach (Minion target in targets)
        {
            target.SendMessage(trigger);
        }
    }

    public static void putHandCardInPlay(Player p, HandCard c)
    {
        p.hand.Remove(c);
        p.summonCard(c.card);
        GameObject.Destroy(c.gameObject);

    }

    public static void takeControlOfMinion(Minion m, Player p, Player e)
    {
        if(p.board.Count < 7)
        {
            e.board.Remove(m);
            p.board.Add(m);
            m.Player = p;
        }
        else
        {
            destroyMinion(m);
        }
    }



}
