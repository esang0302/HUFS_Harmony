using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class checkLoop : MonoBehaviour {

    LoopStation loopstation;
    Image foot;
    bool imageOn;

    // Use this for initialization
    void Start () {
        foot = this.GetComponent<Image>();
        imageOn = false;
        foot.enabled = false;
        loopstation = GameObject.Find("Main Camera").GetComponent<LoopStation>();
    }
	
	// Update is called once per frame
	void Update () {
        
        if(loopstation.click > 0)
        {
            imageOn = true;
            foot.enabled = true;
        }
        else
        {
            imageOn = false;
            foot.enabled = false;
        }
    }
}
