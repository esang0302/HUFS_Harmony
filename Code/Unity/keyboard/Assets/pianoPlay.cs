using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pianoPlay : MonoBehaviour {
    AudioSource source;
    private string inst;//classic, electric, synth
    private string path;
	void Awake () {
        classic();
        //electric();
    }

    public void classic()
    {
        path = "ClassicPiano/";
        selectSound(path);
    }
    public void electric()
    {
        path = "ElectricPiano/";
        selectSound(path);
    }
    public void synth()
    {

    }

    void selectSound(string path)
    {
        for (int i = 0; i < 36; i++)
        {
            source = transform.GetChild(i).GetComponent<AudioSource>();
            source.clip = Resources.Load(path + (i + 1).ToString()) as AudioClip;
        }
    }

}
