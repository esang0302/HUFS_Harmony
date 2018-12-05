using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class enableKeyboard : MonoBehaviour {


	// Use this for initialization
	void Awake () {
        enablePiano();
	}
	
    public void enableSynth()
    {

        for (int i = 0; i < 36; i++)
        {
            transform.GetChild(i).GetComponent<KeyPress>().enabled = false;
            transform.GetChild(i).GetComponent<Synth>().enabled = true;
        }
    }

    public void enablePiano()
    {

        for (int i = 0; i < 36; i++)
        {
            transform.GetChild(i).GetComponent<KeyPress>().enabled = true;
            transform.GetChild(i).GetComponent<Synth>().enabled = false;
        }
    }
}
