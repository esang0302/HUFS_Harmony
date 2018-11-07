using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class col_sound : MonoBehaviour
{

    public AudioSource ticksource;


    // Use this for initialization
    void Start()
    {
        ticksource = GetComponent<AudioSource>();
        ticksource.Play();
    }

    

    private void Update()
    {

    }
}