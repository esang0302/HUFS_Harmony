using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pianoPlay : MonoBehaviour {
    AudioSource source;
    
	void Awake () {
        for (int i = 0; i < 36 ;i++)
        {
            source = transform.GetChild(i).GetComponent<AudioSource>();
            source.clip = Resources.Load("ClassicPiano/"+(i+1).ToString()) as AudioClip;
        }
    }

}
