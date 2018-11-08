using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWithDelay : MonoBehaviour
{

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
        checkTime = 0.5f; //2sec
    }
    private void Update()
    {
        timeSpan += Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("target"))
        {
            if (timeSpan >= checkTime)
            {
                Debug.Log(timeSpan);
                audioSource.volume = collision.relativeVelocity.magnitude / 50;
                audioSource.Play();
                Debug.Log("Click!!");
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
