using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundByContact : MonoBehaviour {

    public AudioSource ticksource;

	// Use this for initialization
	void Start () {
        ticksource = GetComponent<AudioSource>(); 
        
	}
	
	// Update is called once per frame
	void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "target")
        {
            ticksource.Play();
            //ticksource.PlayOneShot(collisionSound);
        }
    		
	}

    private void Update()
    {
        
    }
}
