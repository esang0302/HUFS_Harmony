using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityWithDelay : MonoBehaviour
{
    public float startWait { get; private set; }
    public float spawnWait { get; private set; }

    void start()
    {
        StartCoroutine(spawn());
        startWait = 3.0f;
        spawnWait = 2.0f;
    }
    IEnumerator spawn()
    {
        yield return new WaitForSeconds(startWait);

        while (true)
        {
            yield return new WaitForSeconds(spawnWait);
        }
        // Update is called once per frame
    }
    void OnCollisionEnter(Collision collision)
    {
        AudioSource audioSource;
        if (collision.collider.CompareTag("target"))
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.volume = collision.relativeVelocity.magnitude / 50;
            audioSource.Play();
            Debug.Log("collision.relativeVelocity.magnitude: " + collision.relativeVelocity.magnitude / 100);
        }
    }
}

