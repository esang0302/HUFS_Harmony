using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio : MonoBehaviour {
    AudioSource myAudio;
	// Use this for initialization
	void Start () {
        myAudio = GetComponent<AudioSource>();
        
    }
	
	// Update is called once per frame
	void Update () {
        
    }
    public void Play()
    {
        myAudio.Play();
    }
}
