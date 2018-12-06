using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class checkSustain : MonoBehaviour {

    public GameObject sustain;
    Image foot;
    bool imageOn;
    bool isS;
    // Use this for initialization
    void Start()
    {
        foot = this.GetComponent<Image>();
        imageOn = false;
        foot.enabled = false;
        isS = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (sustain.GetComponent<sustain>().isSustain)
        {
            imageOn = true;
            foot.enabled = true;
        }
        else
        {
            imageOn = false;
            foot.enabled = false;
        }
    }

    void test()
    {

    }
}
