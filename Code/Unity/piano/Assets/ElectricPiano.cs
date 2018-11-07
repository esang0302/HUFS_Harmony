using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricPiano : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip jinosl;
    public AudioClip D;
    public AudioClip E;
    public AudioClip F;
    public AudioClip G;
    public AudioClip A;
    public AudioClip B;
    public GameObject object1;
    public int IsChanged = 0;

    // Use this for initialization
    void Start()
    {

    }
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "target")
        {
            IsChanged = 1;
            object1 = GameObject.Find("Tecla1__1");
            //audio = object1.GetComponent<AudioSource>();
            audio.clip = jinosl;
            //audio.Play();
            //object1 = audio;
            /*audio.clip = C;
            object1 = GameObject.Find("ID343 1");
            audio = object1.GetComponent<AudioSource>();
            audio.clip = D;*/
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
