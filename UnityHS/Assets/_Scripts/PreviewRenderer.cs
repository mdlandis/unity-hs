using UnityEngine;
using System.Collections;

public class PreviewRenderer : MonoBehaviour {

    public float timeTillPreview;
    public float timeTillBegin;
    public float timeTillFullSize;
    

    public float fullSizeScale = 2.4f;

    // Use this for initialization
    void Start () {
        GetComponent<SpriteRenderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (timeTillPreview > timeTillBegin && timeTillPreview < timeTillFullSize)
        {
            transform.localScale = new Vector3(fullSizeScale * ((timeTillPreview - timeTillBegin) / (timeTillFullSize - timeTillBegin)), fullSizeScale * ((timeTillPreview - timeTillBegin) / (timeTillFullSize - timeTillBegin)), 1);
        }
        else
        {
            transform.localScale = new Vector3(fullSizeScale, fullSizeScale, 1);
        }
    }
}
