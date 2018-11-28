using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundWithDelay : MonoBehaviour
{
    public AudioClip play;
    AudioSource audioSource;
    Collision collider;
    float timeSpan;
    float checkTime;
    Renderer rend;
    Material color;

    //public ParticleSystem particles;

    // Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>();

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
                audioSource.PlayOneShot(audioSource.clip, audioSource.volume);
                rend.material.SetColor("_Color", Random.ColorHSV());
                //audioSource.PlayOneShot(play, 1);
                Debug.Log("collision.relativeVelocity.magnitude: " + (collision.relativeVelocity.magnitude) / 100);
                /*foreach (ContactPoint contact in collision.contacts)
                {
                    GameObject particle = Instantiate(particles.gameObject, contact.point, Quaternion.LookRotation(contact.normal));
                    Destroy(particle, 1);
                }
                */
            }

            timeSpan = 0.1f;
            //Debug.Log("collision.relativeVelocity.magnitude: " + collision.relativeVelocity.magnitude / 100);

        }

    }
    private void OnCollisionExit(Collision collision)
    {
        //color = Resources.Load("button_mat2.mat", typeof(Material)) as Material;
       // gameObject.GetComponent<Renderer>().material.color = new Color(206, 238, 235, 150);
    }
}
