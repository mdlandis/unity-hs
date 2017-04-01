using UnityEngine;
using System.Collections;



public class Board : MonoBehaviour {

    public PlayerInput playerInput;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        if (playerInput.currentAction == "attacking")
        {
            playerInput.SendMessage("clickAttackTarget", gameObject);
        }
        if (playerInput.currentAction == "aimingbattlecry")
        {
            playerInput.SendMessage("clickBattlecryTarget", gameObject);
        }
    }
}
