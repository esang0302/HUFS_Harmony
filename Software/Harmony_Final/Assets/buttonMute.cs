using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonMute : MonoBehaviour {
    AudioSource myAudio;
    int counter = 0;
    float vol;
    private void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }
    public void mute()
    {
        counter++;
        
        if (counter % 2 == 1)
        {
            vol = myAudio.volume;
            myAudio.volume = -80;
        }
        else
        {
            myAudio.volume = vol;
            counter = 0;
        }

    }
}
