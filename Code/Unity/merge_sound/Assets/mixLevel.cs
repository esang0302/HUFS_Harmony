using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class mixLevel : MonoBehaviour {

    public AudioMixer masterMixer;

    public void SetMasterLvl(float masterLvl)
    {
        masterMixer.SetFloat("masterVol", masterLvl);
    }
    public void Settrack1Vol(float track1Lvl)
    {
        masterMixer.SetFloat("track1Vol", track1Lvl);
    }
    public void Settrack2Vol(float track2Lvl)
    {
        masterMixer.SetFloat("track2Vol", track2Lvl);
    }
}
