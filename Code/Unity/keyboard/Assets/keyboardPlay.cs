using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyboardPlay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("a"))
            GameObject.Find("13").GetComponent<AudioSource>().PlayOneShot(GameObject.Find("13").GetComponent<AudioSource>().clip,1);
        if (Input.GetKeyDown("w"))
            GameObject.Find("14").GetComponent<AudioSource>().PlayOneShot(GameObject.Find("14").GetComponent<AudioSource>().clip, 1);
        if (Input.GetKeyDown("s"))
            GameObject.Find("15").GetComponent<AudioSource>().PlayOneShot(GameObject.Find("15").GetComponent<AudioSource>().clip, 1);
        if (Input.GetKeyDown("e"))
            GameObject.Find("16").GetComponent<AudioSource>().PlayOneShot(GameObject.Find("16").GetComponent<AudioSource>().clip, 1);
        if (Input.GetKeyDown("d"))
            GameObject.Find("17").GetComponent<AudioSource>().PlayOneShot(GameObject.Find("17").GetComponent<AudioSource>().clip, 1);
        if (Input.GetKeyDown("f"))
            GameObject.Find("18").GetComponent<AudioSource>().PlayOneShot(GameObject.Find("18").GetComponent<AudioSource>().clip, 1);
        if (Input.GetKeyDown("t"))
            GameObject.Find("19").GetComponent<AudioSource>().PlayOneShot(GameObject.Find("19").GetComponent<AudioSource>().clip, 1);
        if (Input.GetKeyDown("g"))
            GameObject.Find("20").GetComponent<AudioSource>().PlayOneShot(GameObject.Find("20").GetComponent<AudioSource>().clip, 1);
        if (Input.GetKeyDown("y"))
            GameObject.Find("21").GetComponent<AudioSource>().PlayOneShot(GameObject.Find("21").GetComponent<AudioSource>().clip, 1);
        if (Input.GetKeyDown("h"))
            GameObject.Find("22").GetComponent<AudioSource>().PlayOneShot(GameObject.Find("22").GetComponent<AudioSource>().clip, 1);
        if (Input.GetKeyDown("u"))
            GameObject.Find("23").GetComponent<AudioSource>().PlayOneShot(GameObject.Find("23").GetComponent<AudioSource>().clip, 1);
        if (Input.GetKeyDown("j"))
            GameObject.Find("24").GetComponent<AudioSource>().PlayOneShot(GameObject.Find("24").GetComponent<AudioSource>().clip, 1);
        if (Input.GetKeyDown("k"))
            GameObject.Find("25").GetComponent<AudioSource>().PlayOneShot(GameObject.Find("25").GetComponent<AudioSource>().clip, 1);
        if (Input.GetKeyDown("o"))
            GameObject.Find("26").GetComponent<AudioSource>().PlayOneShot(GameObject.Find("26").GetComponent<AudioSource>().clip, 1);
        if (Input.GetKeyDown("l"))
            GameObject.Find("27").GetComponent<AudioSource>().PlayOneShot(GameObject.Find("27").GetComponent<AudioSource>().clip, 1);
        if (Input.GetKeyDown("p"))
            GameObject.Find("28").GetComponent<AudioSource>().PlayOneShot(GameObject.Find("28").GetComponent<AudioSource>().clip, 1);
        if (Input.GetKeyDown(";"))
            GameObject.Find("29").GetComponent<AudioSource>().PlayOneShot(GameObject.Find("29").GetComponent<AudioSource>().clip, 1);

    }
}

