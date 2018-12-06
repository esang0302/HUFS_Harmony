using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class KeyPress : MonoBehaviour {
    float MaxZRotation = 5.0f;
    float ZRotationResetVelocity = - 0.3f;
    float ResetPlayTime;
    bool isLeap;
    private bool collided;
    private float currentResetPlayTime;
    private Vector3 initialPosition;
    private float t;
    private AudioSource audio;
    private AudioClip clip;
    public Button modeChange;
    public string[] key = new string[17] { "a", "w", "s", "e", "d", "f", "t", "g", "y", "h", "u", "j", "k", "o", "l", "p", ";" };
    public string myKey;

    private void Awake()
    {
        collided = false;
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("piano"), LayerMask.NameToLayer("piano"), true);
        initialPosition = transform.position;
    }
    void Start () {

        modeChange.GetComponentInChildren<Text>().text = "Leap motion\nmode";
        if (Int32.Parse(gameObject.name) > 12 && Int32.Parse(gameObject.name) < 30)
            myKey = key[Int32.Parse(gameObject.name) - 13];
        else
            myKey = "0";
        isLeap = true;
        currentResetPlayTime = 0;
        audio = gameObject.GetComponent<AudioSource>();
        ResetPlayTime = 0.4f;
        modeChange.onClick.AddListener(isleap);
    }

    // Update is called once per frame
   void Update () {
        currentResetPlayTime += Time.deltaTime;

        if (isLeap)

            Leap();
        else
            NotLeap();

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
    void isleap()
    {
        isLeap = !isLeap;
        if (!isLeap)
            modeChange.GetComponentInChildren<Text>().text = "Keyboard\nmode";
        else
        {
            modeChange.GetComponentInChildren<Text>().text = "Leap motion\nmode";
        }

    }

    void Leap()
    {
        if (transform.rotation.eulerAngles.z > 1.5f && currentResetPlayTime >= ResetPlayTime && !collided)
        {
            audio.PlayOneShot(audio.clip, 1f);
            currentResetPlayTime = 0f;
        };
    }
    void NotLeap()
    {
        if (Input.GetKeyDown(myKey))
        {
            audio.PlayOneShot(audio.clip, 1f);
            currentResetPlayTime = 0f;
            Debug.Log("눌렀다");
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
}
