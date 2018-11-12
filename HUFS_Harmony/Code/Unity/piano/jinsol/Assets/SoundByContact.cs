using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundByContact : MonoBehaviour {

    public AudioSource ticksource;
    public Transform transform;
    // Use this for initialization
    void Start () {
        ticksource = GetComponent<AudioSource>();
        //transform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "target")
        {
            ticksource.Play();
            //transform.Rotate(2, 0, 0);
            //ticksource.PlayOneShot(collisionSound);
        }
    		
	}


    private void Update()
    {
        
    }
}
