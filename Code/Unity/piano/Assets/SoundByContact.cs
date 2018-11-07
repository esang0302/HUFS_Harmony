using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundByContact : ElectricPiano {

    public AudioSource ticksource;
    public AudioClip EC;
    public AudioClip CC;
    public AudioClip OC;

	// Use this for initialization
	void Start () {
        ticksource = GetComponent<AudioSource>(); 
        
	}
	
	// Update is called once per frame
	void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "target")
        {/*
            if (IsChanged == 1)
            {
                ticksource.clip = EC;
            }
            else
            {
                ticksource.clip = CC;
            }*/
            //ticksource.Play();
            //ticksource.PlayOneShot(collisionSound);
        }
    		
	}

    private void Update()
    {
        
    }
}
