using UnityEngine;
using System.Collections;
using Ardunity;

public class MyComponent : MonoBehaviour
{
    public AnalogInput analogInput;
    float val;
    AudioSource source;
    //AudioClip sound;
    void Start()
    {
        source = GetComponent<AudioSource>();
        //sound = GetComponent<AudioClip>();
        InvokeRepeating("Music", 1, 5);
    }

    void Update()
    {
        val = analogInput.Value;
        Debug.Log(val);
    }

    void Music()
    {
        if (val <= 0.4 && val != 0)
        {
            source.volume = 0.3f;
            source.Play();
        }
        else if ( 0.4 < val && val <= 0.6)
        {
            source.volume = 0.6f;
            source.Play();
        }
        else if (val > 0.6)
        {
            source.volume = 1.0f;
            source.Play();
        }
    }
}