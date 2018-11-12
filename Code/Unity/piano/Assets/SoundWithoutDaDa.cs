using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWithoutDaDa : MonoBehaviour
{
    AudioSource ticksource;
    float timeSpan;
    float checkTime;
    float speed;
    float angleX;
    Vector3 pressed =new Vector3(90, 0, 0);
    void Start()
    {
        ticksource = GetComponent<AudioSource>();
        timeSpan = 0.2f;
        checkTime = 1f; //2sec
        speed = 15;
        transform.Rotate(-10, 0, 0);
    }
    void Update()
    {
        timeSpan += Time.deltaTime;
        //transform.eulerAngles = pressed;
        
        if (gameObject.transform.rotation.x > 10)
        {
            Debug.Log("Hit!");
        }
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
