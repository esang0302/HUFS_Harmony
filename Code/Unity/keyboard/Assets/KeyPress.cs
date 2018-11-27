using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private void Awake()
    {
        collided = false;
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("piano"), LayerMask.NameToLayer("piano"), true);
        initialPosition = transform.position;
    }
    void Start () {
        
        currentResetPlayTime = 0;
        audio = gameObject.GetComponent<AudioSource>();
        clip = audio.clip;
        ResetPlayTime = 0.4f;
    }

    // Update is called once per frame
   void Update () {
        //return to origin state of piano
        currentResetPlayTime += Time.deltaTime;

        if (transform.rotation.eulerAngles.z > 1.5f && currentResetPlayTime >= ResetPlayTime && !collided)
        {
            Debug.Log("SOUND ON " + currentResetPlayTime);
            audio.PlayOneShot(clip, 1f);
            currentResetPlayTime = 0f;
        }
        
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
            //t = 0;
        }
        //Can't over the limit of origin state
        if (transform.rotation.eulerAngles.z > 350 || transform.rotation.eulerAngles.z < 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
            GetComponent<Rigidbody>().angularDrag = 0;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            Debug.Log("Right!");
            Debug.Log(transform.rotation.eulerAngles.z);
        }

        transform.position = initialPosition;
        GetComponent<Rigidbody>().velocity = Vector3.zero;

        
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
        //if (GetComponent<Collider>().bounds.max.x > collision.contacts[0].point.x && GetComponent<Collider>().bounds.min.x < collision.contacts[0].point.x
        //    && GetComponent<Collider>().bounds.min.y < collision.contacts[0].point.y && GetComponent<Collider>().bounds.max.y > collision.contacts[0].point.y)
        //{
        //}
    }

}
