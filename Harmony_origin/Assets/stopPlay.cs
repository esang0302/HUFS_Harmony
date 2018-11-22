using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stopPlay : MonoBehaviour {

    public GameObject track1;
    public GameObject track2;
    public GameObject button;
    public Sprite on;
    public Sprite pause;

	// Use this for initialization
	void Start () {

            button.GetComponent<Image>().sprite = on;
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SoundControl()
    {
        if (AudioListener.pause == true)
        {
            AudioListener.pause = false;
            button.GetComponent<Image>().sprite = on;
            track1.GetComponent<AudioSource>().Pause();
            track2.GetComponent<AudioSource>().Pause();
        }
        else
        {
            AudioListener.pause = true;
            button.GetComponent<Image>().sprite = pause;
            track1.GetComponent<AudioSource>().UnPause();
            track2.GetComponent<AudioSource>().UnPause();
        }
    }
}
