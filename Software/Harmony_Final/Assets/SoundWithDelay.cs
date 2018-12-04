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
    float colortimeSpan;
    float checkTime;
    Renderer rend;
    Color origin_color;
    Color emission_color;
    Color color;
    float intensity;
    private Material _material;
    public GameObject Lights;
    
    //public ParticleSystem particles;

    // Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>();
        _material = GetComponent<Renderer>().material;

        audioSource = GetComponent<AudioSource>();
        timeSpan = 0.2f;
        checkTime = 0.3f; //2sec
        colortimeSpan = 0.0f;

        origin_color = new Color(179, 253, 255);
        emission_color = new Color(0.6941973f, 3.692549f, 3.773585f);
        intensity = 4.416925f;
        
        
        // Turn off emission
        _material.EnableKeyword("_EMISSION");
        //원래 컬러 new Color(0.6941973f, 3.692549f, 3.773585f);



    }
    private void Update()
    {
        timeSpan += Time.deltaTime;
        colortimeSpan += Time.deltaTime;
        if(colortimeSpan >= checkTime)
        {
            //rend.material.SetColor("_EmissionColor", new Color(0, 9.082055f, 16));
            _material.DisableKeyword("_EMISSION");
            rend.material.SetColor("_EmissionColor", new Color(0.6941973f, 3.692549f, 3.773585f));
            rend.material.SetFloat("_EmissionScaleUI", intensity);
            colortimeSpan = 0.0f;
            Lights.SetActive(false);
        }
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
        //origin_color = new Color(179, 253, 0);
        //rend.material.SetColor("_EmissionColor", new Color(0, 9.082055f, 16));
        //rend.material.SetFloat("_EmissionScaleUI",intensity);

        // Turn on emission
        //_material.EnableKeyword("_EMISSION");
        Lights.SetActive(true);
        colortimeSpan = 0.0f;
        _material.EnableKeyword("_EMISSION");
        rend.material.SetColor("_EmissionColor", new Color(0, 9.082055f, 16));
        rend.material.SetFloat("_EmissionScaleUI", intensity);

        if (collision.gameObject.CompareTag("target"))
        {

            if (timeSpan >= checkTime)
            {

                audioSource.volume = collision.relativeVelocity.magnitude / 20;
                Debug.Log(collision.relativeVelocity.magnitude);
                audioSource.PlayOneShot(audioSource.clip, audioSource.volume);
                // Turn on emission


                //rend.material.SetColor("_Color", Random.ColorHSV());
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
        }

    }
    private void OnCollisionStay(Collision collision)
    {

    }

    private void OnCollisionExit(Collision collision)
    {


    }
}
