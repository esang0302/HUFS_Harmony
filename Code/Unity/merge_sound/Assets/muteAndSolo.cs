using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class muteAndSolo : MonoBehaviour {
    AudioSource myAudio;
    public AudioSource otherAudio;
    public Button buttonM;
    public Button buttonS;


    int counter = 0;
    float vol, othervol;
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
            buttonM.image.color = Color.red;
        }
        else
        {
            myAudio.volume = vol;
            counter = 0;
            buttonM.image.color = Color.white;
        }

    }
    public void solo()
    {
        counter++;

        if (counter % 2 == 1)
        {
            othervol = otherAudio.volume;
            otherAudio.volume = -80;
            buttonS.image.color = Color.yellow;
        }
        else
        {
            otherAudio.volume = othervol;
            counter = 0;
            buttonS.image.color = Color.white;
        }

    }
}
