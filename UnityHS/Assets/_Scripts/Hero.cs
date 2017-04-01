using UnityEngine;
using System.Collections;

public class Hero : BoardCharacter {


    public int armor;
    public Weapon equipped;

    public SpriteRenderer armorBox;
    public TextMesh armorVal;

    public bool attacked;

    public override void processMovement()
    {
        if (posToMoveTo != transform.position)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, posToMoveTo, step);
            if (transform.position == posToMoveTo)
            {
                if (attackTarget != null)
                {
                    Player.completeAttack(this, attackTarget);
                }
            }
        }
    }

    public override void alignStatBoxes()
    {
        if (cAttack <= 0)
        {
            aBox.enabled = false;
            aText.text = "";
            canAttack = false;

        }
        else
        {
            aBox.enabled = true;
            aText.text = ""+cAttack;
            if (!attacked)
            {
                canAttack = true;
            }
            else
            {
                canAttack = false;
            }

        }
        if(armor <= 0)
        {
            armorBox.enabled = false;
            armorVal.text = "";
        }
        else
        {
            armorBox.enabled = true;
            armorVal.text = armor + "";
        }
    }

    public void changeWeapon(Weapon w)
    {
        if(equipped != null)
        {
            EffectsController.destroyWeapon(equipped);
        }
        
        equipped = w;
    }

    public override void checkDead()
    {
        if (cHealth <= 0)
        {
            Player.killHero(this);
        }
    }

    public override void takeDamage(int num)
    {
        if(armor > 0)
        {
            int remainingArmor = armor - num;
            if (remainingArmor >= 0)
            {
                armor = remainingArmor;
            }
            else
            {
                armor = 0;
                cHealth += remainingArmor;
            }
        }
        else
        {
            cHealth -= num;
        }
        
    }

    public override void die()
    {
        bAttack = -1000;
        aBox.enabled = false;
        hBox.enabled = false;
        aText.text = "";
        hText.text = "";

    }

}
