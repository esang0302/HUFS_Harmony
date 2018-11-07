using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class velocity : MonoBehaviour {

    AudioSource audioSource;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.collider.CompareTag("target"))
        {
            audioSource.volume = collision.relativeVelocity.magnitude/50;
            audioSource.Play();
            Debug.Log("collision.relativeVelocity.magnitude: " + collision.relativeVelocity.magnitude/100);
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
