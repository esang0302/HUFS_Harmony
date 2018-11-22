using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWithoutDaDa : MonoBehaviour
{
    AudioSource ticksource;
    Collision collider;
    float timeSpan;
    float checkTime;
    float speed;
    private Rigidbody rb;
    //public Quaternion m_startRot;
    float angleX;
    // Use this for initialization
    void Start()
    {
        ticksource = GetComponent<AudioSource>();
        timeSpan = 0.2f;
        checkTime = 1f; //2sec
        speed = 15;
    }
    void Update()
    {
        timeSpan += Time.deltaTime;
        /*GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;
        if(!(transform.eulerAngles.x > 0 && transform.eulerAngles.x < 5))
        {
            angleX = 0.0f;
            transform.eulerAngles = new Vector3(angleX, transform.eulerAngles.y, transform.eulerAngles.z);
            //Quaternion target = Quaternion.Euler(0, 0, 0);
            //transform.rotation = Quaternion.Slerp(transform.rotation, target, 0.0f);
        }*/
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "target")
        {
            if (timeSpan >= checkTime)
            {
                Debug.Log(timeSpan);
                ticksource.Play();
                Quaternion rotation = Quaternion.identity;
                rotation.eulerAngles = new Vector3(5, 0, 0);
                GetComponent<Rigidbody>().rotation *= rotation;
                Invoke("Restore", 0.2f);
                //rb = GetComponent<Rigidbody>();
                //FixedUpdate();
                //GetComponent<Rigidbody>().rotation = Quaternion.Euler(-10f, 0.0f, 0.0f);
            }

            timeSpan = 0.1f;
        }
    }
    void Restore()
    {
        Quaternion rotation = Quaternion.identity;
        rotation.eulerAngles = new Vector3(-5, 0, 0);
        GetComponent<Rigidbody>().rotation *= rotation;

    }
    /*void FixedUpdate()
    {
        rb.WakeUp();
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
        rb.AddForce(movement * speed);
        float heading = Mathf.Atan2(moveHorizontal, moveVertical) * Mathf.Rad2Deg;

        if (moveHorizontal != 0 || moveVertical != 0)
        {
            rb.rotation = Quaternion.Euler(0, heading, 0);

        }
    }*/
}
