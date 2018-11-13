using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPress : MonoBehaviour {
    public float MaxZRotation = 6.0f;
    public float ZRotationResetVelocity = -8;
   public float ResetPlayTime = 0.5f;

    private float currentResetPlayTime = 0;
    private float pushingTime = 0;
    private float pushingPower = 0;
    private Vector3 initialPosition;
    AudioSource audio;
    AudioClip clip;

    void Start () {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("piano"), LayerMask.NameToLayer("piano"), true);
        initialPosition = transform.position;
        currentResetPlayTime = ResetPlayTime;
        audio = gameObject.GetComponent<AudioSource>();
        clip = audio.clip;
    }

    // Update is called once per frame
   void Update () {
        //return to origin state of piano
        if (transform.rotation.eulerAngles.z > 0.1f)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, Time.deltaTime * ZRotationResetVelocity));
            GetComponent<Rigidbody>().angularDrag = 0;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            Debug.Log(transform.rotation.eulerAngles.z);
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
        pushingTime += Time.deltaTime;

        if (currentResetPlayTime >= ResetPlayTime)
            {
                currentResetPlayTime = 0.0f;
            }
        //play sound when eulerAngles.z > 1.5
        if (transform.rotation.eulerAngles.z > 1.5f)
        {
            pushingPower = (transform.rotation.eulerAngles.z) / pushingTime;
            audio.PlayOneShot(clip, 1f);
        }
            
        
    }

}
