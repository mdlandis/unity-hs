using UnityEngine;
using System.Collections;

public class DamageLabel : MonoBehaviour {

    public float totalLife;
    public float finishFadeIn;
    public float startFadeOut;
    public float currentstate;
    public float speed;
    public BoardCharacter attached;


	// Use this for initialization
	void Start () {
        currentstate = 0.0f;
        Color tmp = GetComponent<TextMesh>().color;
        tmp.a = 0f;
        GetComponent<TextMesh>().color = tmp;
    }
	
	// Update is called once per frame
	void Update () {
        Color tmp = GetComponent<TextMesh>().color;
        if (currentstate < finishFadeIn)
        {

            tmp.a = currentstate / finishFadeIn * 1.0f;

        }
        else if (currentstate >= finishFadeIn && currentstate < startFadeOut)
        {
            tmp.a = 1.0f;
        }
        else
        {
            tmp.a = 1.0f - (currentstate - startFadeOut) / (totalLife - startFadeOut);
            if (tmp.a <= 0.001f)
            {
                Destroy(gameObject);
            }
        }
        
        GetComponent<TextMesh>().color = tmp;
        currentstate += 1.0f * Time.deltaTime;
        //transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        if(attached)
            transform.position = attached.transform.position;
    }
}
