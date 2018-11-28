using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AccordingToMagnitude: MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip C;
    public AudioClip D;
    public AudioClip E;
    public AudioSource audio;
    void Start()
    {
      
    }

    void OnCollisionEnter(Collision collision)
    {
        audio = GetComponent<AudioSource>();
        if(collision.gameObject.tag == "target")
            if (collision.relativeVelocity.magnitude <3){
                audio.clip = C;

            }
            else if (collision.relativeVelocity.magnitude > 3 && collision.relativeVelocity.magnitude < 7){
                audio.clip = D;
            }
            else{
                audio.clip = E;
            }
            audio.Play();
    }
    
    void Update() { 
    
    }
}            