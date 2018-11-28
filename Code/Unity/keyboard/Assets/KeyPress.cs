using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class KeyPress : MonoBehaviour {
    public float MaxZRotation = 5.0f;
    float ZRotationResetVelocity = - 0.3f;
    public float ResetPlayTime;

    private bool collided;
    private float currentResetPlayTime;
    private Vector3 initialPosition;
    private float t;
    private AudioSource audio;
    private AudioClip clip;

    private double increment;
    private double frequencyFixed = 440;
    private float frequency;
    float synthVol;
    public float gain;
    private double phase;
    private double sampling_frequency = 48000.0;

    private void Awake()
    {
        collided = false;
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("piano"), LayerMask.NameToLayer("piano"), true);
        initialPosition = transform.position;
    }
    void Start () {
        frequency = GameObject.Find("piano").GetComponent<Oscillator>().frequencies[Int32.Parse(gameObject.name)];
        currentResetPlayTime = 0;
        audio = gameObject.GetComponent<AudioSource>();
        clip = audio.clip;
        ResetPlayTime = 0.4f;
        synthVol = 1f;
    }

    // Update is called once per frame
   void Update () {
        //return to origin state of piano
        currentResetPlayTime += Time.deltaTime;
        //onPiano();
        onSynth();

        if (transform.rotation.eulerAngles.z > 0.2f && !collided)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, -0.2f));
            GetComponent<Rigidbody>().angularDrag = 0;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
        //Can't over the limit of pressing 
        if (transform.rotation.eulerAngles.z > MaxZRotation)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, MaxZRotation);
            GetComponent<Rigidbody>().angularDrag = 0;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
        //Can't over the limit of origin state
        if (transform.rotation.eulerAngles.z > 350 || transform.rotation.eulerAngles.z < 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
            GetComponent<Rigidbody>().angularDrag = 0;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }

        transform.position = initialPosition;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        
    }
    public void OnAudioFilterRead(float[] data, int channels)
    {
        increment = frequency * 2.0 * Mathf.PI / sampling_frequency;
        for (int i = 0; i < data.Length; i += channels)
        {
            phase += increment;
            data[i] = makeSinWave();

            if (channels == 2)
            {
                data[i + 1] = data[i];
            }
            if (phase > (Mathf.PI * 2))
            {
                phase = 0.0;
            }
        }
    }

    void onSynth()
    {
        if (transform.rotation.eulerAngles.z > 1.5f)
        {
            audio.PlayOneShot(clip, 1f);
            gain = 0.5f;
        }
        else
            gain = 0;
    }

    void onPiano()
    {
        if (transform.rotation.eulerAngles.z > 1.5f && currentResetPlayTime >= ResetPlayTime && !collided)
        {
            audio.PlayOneShot(clip, 1f);
            currentResetPlayTime = 0f;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "target")
            collided = false;
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "target")
            collided = true;

        
    }

   


    public float makeSinWave()
    {
        return (float)(gain * Mathf.Sin((float)phase));
    }

    public float makeSquareWave()
    {
        if (gain * Mathf.Sin((float)phase) >= 0 * gain)
            return (float)gain * 0.6f;
        else
            return (-(float)gain) * 0.6f;
    }

    public float makeTriangleWave()
    {
        return (float)(gain * (double)Mathf.PingPong((float)phase, 1.0f));
    }
}
