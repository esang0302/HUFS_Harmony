using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWithDelay : MonoBehaviour
{
    public AudioClip play;
    AudioSource audioSource;
    Collision collider;
    float timeSpan;
    float checkTime;
    public ParticleSystem particles;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        timeSpan = 0.2f;
        checkTime = 0.3f; //2sec
    }
    private void Update()
    {
        timeSpan += Time.deltaTime;
    }
    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(gameObject.name);
            GameObject.Find("Main Camera").GetComponent<TextFileFinder_launch>().fileBrowserOpen(gameObject.name);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("target"))
        {
            if (timeSpan >= checkTime)
            {
                
                audioSource.volume = collision.relativeVelocity.magnitude / 50;
                Debug.Log(collision.relativeVelocity.magnitude);
                audioSource.PlayOneShot(audioSource.clip,audioSource.volume);
                //audioSource.PlayOneShot(play, 1);
                Debug.Log("collision.relativeVelocity.magnitude: " + (collision.relativeVelocity.magnitude) / 100);
                foreach (ContactPoint contact in collision.contacts)
                {
                    GameObject particle = Instantiate(particles.gameObject, contact.point, Quaternion.LookRotation(contact.normal));
                    Destroy(particle, 1);
                }

            }

            timeSpan = 0.1f;
            //Debug.Log("collision.relativeVelocity.magnitude: " + collision.relativeVelocity.magnitude / 100);

        }

    }

}
