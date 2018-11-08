using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricPiano : MonoBehaviour
{
    public AudioSource audio;

    public AudioClip[] sound;
    public GameObject[] objects;

    public AudioClip sound1C;
    public AudioClip sound1Csp;
    public AudioClip sound1D;
    public AudioClip sound1Dsp;
    public AudioClip sound1E;
    public AudioClip sound1F;
    public AudioClip sound1Fsp;
    public AudioClip sound1G;
    public AudioClip sound1Gsp;
    public AudioClip sound1A;
    public AudioClip sound1Asp;
    public AudioClip sound1B;

    public AudioClip sound2C;
    public AudioClip sound2Csp;
    public AudioClip sound2D;
    public AudioClip sound2Dsp;
    public AudioClip sound2E;
    public AudioClip sound2F;
    public AudioClip sound2Fsp;
    public AudioClip sound2G;
    public AudioClip sound2Gsp;
    public AudioClip sound2A;
    public AudioClip sound2Asp;
    public AudioClip sound2B;

    public AudioClip sound3C;
    public AudioClip sound3Csp;
    public AudioClip sound3D;
    public AudioClip sound3Dsp;
    public AudioClip sound3E;
    public AudioClip sound3F;
    public AudioClip sound3Fsp;
    public AudioClip sound3G;
    public AudioClip sound3Gsp;
    public AudioClip sound3A;
    public AudioClip sound3Asp;
    public AudioClip sound3B;

    public GameObject object1C;
    public GameObject object1Csp;
    public GameObject object1D;
    public GameObject object1Dsp;
    public GameObject object1E;
    public GameObject object1F;
    public GameObject object1Fsp;
    public GameObject object1G;
    public GameObject object1Gsp;
    public GameObject object1A;
    public GameObject object1Asp;
    public GameObject object1B;

    public GameObject object2C;
    public GameObject object2Csp;
    public GameObject object2D;
    public GameObject object2Dsp;
    public GameObject object2E;
    public GameObject object2F;
    public GameObject object2Fsp;
    public GameObject object2G;
    public GameObject object2Gsp;
    public GameObject object2A;
    public GameObject object2Asp;
    public GameObject object2B;

    public GameObject object3C;
    public GameObject object3Csp;
    public GameObject object3D;
    public GameObject object3Dsp;
    public GameObject object3E;
    public GameObject object3F;
    public GameObject object3Fsp;
    public GameObject object3G;
    public GameObject object3Gsp;
    public GameObject object3A;
    public GameObject object3Asp;
    public GameObject object3B;


    // Use this for initialization
    void Start()
    {
        sound = new AudioClip[] {sound1C, sound1Csp, sound1D, sound1Dsp, sound1E, sound1F, sound1Fsp, sound1G, sound1Gsp, sound1A, sound1Asp, sound1B, 
            sound2C, sound2Csp, sound2D, sound2Dsp, sound2E, sound2F, sound2Fsp, sound2G, sound2Gsp, sound2A, sound2Asp, sound2B, 
            sound3C, sound3Csp, sound3D, sound3Dsp, sound3E, sound3F, sound3Fsp, sound3G, sound3Gsp, sound3A, sound3Asp, sound3B};
        objects = new GameObject[] {object1C, object1Csp, object1D, object1Dsp, object1E, object1F, object1Fsp, object1G, object1Gsp, object1A, object1Asp, object1B, 
            object2C, object2Csp, object2D, object2Dsp, object2E, object2F, object2Fsp, object2G, object2Gsp, object2A, object2Asp, object2B, 
            object3C, object3Csp, object3D, object3Dsp, object3E, object3F, object3Fsp, object3G, object3Gsp, object3A, object3Asp, object3B};
    }
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "target")
        {
            for (int i = 0; i < 36; i++)
            {
                audio = objects[i].GetComponent<AudioSource>();
                audio.clip = sound[i];
            }

        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
