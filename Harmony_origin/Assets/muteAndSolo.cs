using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class muteAndSolo : MonoBehaviour {
    AudioSource myAudio;
    public AudioSource otherAudio1;
    public AudioSource otherAudio2;
    public Button buttonM;
    public Button buttonS;


    int counter = 0;
    float vol, othervol1,othervol2;
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
            othervol1 = otherAudio1.volume;
            otherAudio1.volume = -80;
            othervol2 = otherAudio2.volume;
            otherAudio2.volume = -80;
            buttonS.image.color = Color.yellow;
        }
        else
        {
            otherAudio1.volume = othervol1;
            otherAudio2.volume = othervol2;
            counter = 0;
            buttonS.image.color = Color.white;
        }

    }
}
