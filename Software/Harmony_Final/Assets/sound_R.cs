using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound_R : MonoBehaviour
{
    public AudioClip play;
    AudioSource audioSource;
    Collision collider;
    float timeSpan;
    float checkTime;
    //public ParticleSystem particles;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        timeSpan = 0.2f;
        checkTime = 0.225f; //2sec
    }
    private void Update()
    {
        timeSpan += Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.CompareTag("stick2"))

        {

            if (timeSpan >= checkTime)
            {

                audioSource.volume = collision.relativeVelocity.magnitude;
                audioSource.PlayOneShot(play, audioSource.volume);
                Debug.Log("collision.relativeVelocity.magnitude: " + (collision.relativeVelocity.magnitude));
                // foreach (ContactPoint contact in collision.contacts)
                {
                    // GameObject particle = Instantiate(particles.gameObject, contact.point, Quaternion.LookRotation(contact.normal));
                    //  Destroy(particle, 1);
                }

            }

            timeSpan = 0.1f;
            //Debug.Log("collision.relativeVelocity.magnitude: " + collision.relativeVelocity.magnitude / 100);

        }

    }

}
