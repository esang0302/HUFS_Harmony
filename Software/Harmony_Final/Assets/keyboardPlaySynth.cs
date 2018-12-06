using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyboardPlaySynth : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("a"))
        {
            GameObject.Find("13").GetComponent<AudioSource>().PlayOneShot(GameObject.Find("13").GetComponent<AudioSource>().clip, 5f);
            GameObject.Find("13").GetComponent<Synth>().gain = 0.5f;
            Debug.Log("keydown");
        }

        if(Input.GetKeyUp("a"))
        {
            GameObject.Find("13").GetComponent<Synth>().gain = 0;
            Debug.Log("keyup");
        }


        if (Input.GetKeyDown("w"))
            GameObject.Find("14").GetComponent<Synth>().gain = 0.5f;
        if (Input.GetKeyDown("s"))
            GameObject.Find("15").GetComponent<Synth>().gain = 0.5f;
        if (Input.GetKeyDown("e"))
            GameObject.Find("16").GetComponent<Synth>().gain = 0.5f;
        if (Input.GetKeyDown("d"))
            GameObject.Find("17").GetComponent<Synth>().gain = 0.5f;
        if (Input.GetKeyDown("f"))
        {
            GameObject.Find("18").GetComponent<AudioSource>().PlayOneShot(GameObject.Find("18").GetComponent<AudioSource>().clip, 5f);
            GameObject.Find("18").GetComponent<Synth>().gain = 0.5f;
            Debug.Log("keydown");
        }
        if (Input.GetKeyUp("a"))
        {
            GameObject.Find("18").GetComponent<Synth>().gain = 0;
            Debug.Log("keyup");
        }
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
    /*public void OnAudioFilterRead(float[] data, int channels)
    {
        for (int i = 0; i < data.Length; i += channels)
        {

            data[i] = (1 - harmonicValue) * makeSinWave(1, phaseTest1) + harmonicValue * (makeSinWave(2, phaseTest2) + makeSinWave(3, phaseTest3)); ;

            //data[i] = (1 - harmonicValue) * makeSquareWave(1, phaseTest1) + harmonicValue * (makeSquareWave(2, phaseTest2) + makeSinWave(3, phaseTest3)); ;

            //data[i] = (1 - harmonicValue) * makeTriangleWave(1, phaseTest1) + harmonicValue * (makeTriangleWave(2, phaseTest2) + makeSinWave(3, phaseTest3)); ;

            if (channels == 2)
            {
                data[i + 1] = data[i];
            }
            if (phase > (Mathf.PI * 2))
            {
                phase = 0.0;
                phaseTest1 = 0;
                phaseTest2 = 0;
                phaseTest3 = 0;
                phaseTest4 = 0;
            }
        }
    }*/
}
