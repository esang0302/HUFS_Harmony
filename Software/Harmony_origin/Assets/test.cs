using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class test : MonoBehaviour {
    AudioSource audio;
    private List<string> playlist;
    private string musicDir = "C:/Users/";
    private int currIndex = 0;

    void Start()
    {
        string[] songs = Directory.GetFiles(@musicDir, "*.wav", SearchOption.TopDirectoryOnly);
        playlist = new List<string>(songs);

        StartAudio();
    }


    IEnumerator StartAudio()
    {
        WWW audioLoader = new WWW("file:///" + playlist[currIndex]);
        while (!audioLoader.isDone)
            yield return null;

        Debug.Log(audioLoader.GetAudioClip().name);
        audio.clip = audioLoader.GetAudioClip();
        audio.Play();

    }
}