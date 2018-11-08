using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWithoutDaDa : MonoBehaviour
{
    AudioSource ticksource;
    Collision collider;
    float timeSpan;
    float checkTime;

    // Use this for initialization
    void Start()
    {
        ticksource = GetComponent<AudioSource>();
        timeSpan = 0.2f;
        checkTime = 1f; //2sec
    }
    void Update()
    {
        timeSpan += Time.deltaTime;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "target")
        {
            if (timeSpan >= checkTime)
            {
                Debug.Log(timeSpan);
                ticksource.Play();
            }

            timeSpan = 0.1f;
        }
    }
}
