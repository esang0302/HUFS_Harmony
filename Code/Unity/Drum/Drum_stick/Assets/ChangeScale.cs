using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScale : MonoBehaviour {
    //public GameObject objectToBig;
	// Use this for initialization
	void Start () {
        Physics.IgnoreLayerCollision(0,3, true);
    }
    public void scaleUp()
    {
        
        transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        
    }
    public void scaleDown()
    {

        transform.localScale = new Vector3(0f, 0f, 0f);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
