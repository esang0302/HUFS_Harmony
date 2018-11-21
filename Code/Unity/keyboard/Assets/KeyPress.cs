using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPress : MonoBehaviour {
    public float MaxZRotation = 6.0f;
    public float ZRotationResetVelocity = -6;
   public float ResetPlayTime = 0.5f;

    private float currentResetPlayTime = 0;
    private Vector3 initialPosition;
    private float t;
    AudioSource audio;
    AudioClip clip;
    private void Awake()
    {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("piano"), LayerMask.NameToLayer("piano"), true);
    }
    void Start () {
       
        initialPosition = transform.position;
        currentResetPlayTime = ResetPlayTime;
        audio = gameObject.GetComponent<AudioSource>();
        clip = audio.clip;
        
    }

    // Update is called once per frame
   void Update () {
        //return to origin state of piano
        t = Time.deltaTime;
        if (transform.rotation.eulerAngles.z > 0.3f)
        { 
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, t * (-9)));
            //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
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
        if (transform.rotation.eulerAngles.z < 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
            GetComponent<Rigidbody>().angularDrag = 0;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }

        if (currentResetPlayTime < ResetPlayTime)
            currentResetPlayTime += Time.deltaTime;

        transform.position = initialPosition;
        GetComponent<Rigidbody>().velocity = Vector3.zero;

        
    }
    void OnCollisionEnter(Collision collision)
    {
        if (GetComponent<Collider>().bounds.max.x > collision.contacts[0].point.x && GetComponent<Collider>().bounds.min.x < collision.contacts[0].point.x
            && GetComponent<Collider>().bounds.min.y < collision.contacts[0].point.y)
        {
            if (currentResetPlayTime >= ResetPlayTime)
            {
                currentResetPlayTime = 0.0f;
            }
            //play sound when eulerAngles.z > 1.5
            if (transform.rotation.eulerAngles.z > 1f)
            {
                audio.PlayOneShot(clip, 1f);
            }
        }
        //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, MaxZRotation);
    }

}
