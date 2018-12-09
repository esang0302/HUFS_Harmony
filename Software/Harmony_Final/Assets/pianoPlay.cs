using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class pianoPlay : MonoBehaviour {
    AudioSource source;
    private string inst;//classic, electric, synth
    string path;
    public AudioMixer mixer;
    public Button electricB;
    public Button classicB;
    public Button leapSelectB;

    bool isleap;
    bool isClassic;
    int modeCount;

	void Awake () {
        ClassicForKey();
        isleap = false;
        isClassic = true;
        modeCount = 0;
    }
    private void Start()
    {
        leapSelectB.GetComponentInChildren<Text>().text = "KeyBoard\nmode";
    }

    public void checkleap()
    {
        if (leapSelectB.GetComponentInChildren<Text>().text == "KeyBoard\nmode")
        {
            isleap = true;
            leapSelectB.GetComponentInChildren<Text>().text = "Leap Motion\nmode";
            for (int i = 0; i < 36; i++)
            {
                source = transform.GetChild(i).GetComponent<AudioSource>();
                source.Stop();
                source.volume = 1f;
            }

        }
        else
        {
            isleap = false;
            leapSelectB.GetComponentInChildren<Text>().text = "KeyBoard\nmode";
        }
        if (isClassic)
            checkClassic();
        else
            checkElectric();
        
    }

    public void checkElectric()
    {
        isClassic = false;
        if (isleap)
            electric();
        else if (!isleap)
            ElectricForKey();
    }
    public void checkClassic()
    {
        isClassic = true;
        if (isleap)
            classic();
        else if (!isleap)
            ClassicForKey();
    }
    /// 어떤 버튼을 택했는지 위에서 판단
    /// 
    public void classic()
    {
        path = "ClassicPiano/";
        selectSound(path);
    }

    public void ClassicForKey()
    {
        path = "ClassicPiano_Key/";
        selectSoundForKey(path);
    }

    public void ElectricForKey()
    {
        path = "ElectricPiano_Key/";
        selectSoundForKey(path);
    }

    public void electric()
    {
        path = "ElectricPiano/";
        selectSound(path);
    }

    public void setReverb(float value)
    {
        mixer.SetFloat("Reverb", value);
    }

    public void selectSound(string path)
    {
        for (int i = 0; i < 36; i++)
        {
            source = transform.GetChild(i).GetComponent<AudioSource>();
            source.clip = Resources.Load(path + (i + 1).ToString()) as AudioClip;
          
        }
    }
    public void selectSoundForKey(string path)
    {
        for (int i = 0; i < 24; i++)
        {
            source = transform.GetChild(i+12).GetComponent<AudioSource>();
            source.clip = Resources.Load(path + (i + 1).ToString()) as AudioClip;

        }
    }
}
