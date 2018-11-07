using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

[RequireComponent(typeof(AudioSource))]
public class importAudio : MonoBehaviour {

    AudioSource audio;
    AudioClip loadedClip;
    private string musicFile;

    void Start () {
        musicFile = EditorUtility.OpenFilePanel("Load files", "", "");
        Debug.Log(musicFile);
        StartCoroutine(loadAudio());
    }

    IEnumerator loadAudio()
    {
        WWW audioLoader = new WWW("file:///" + musicFile);
        while (!audioLoader.isDone) 
            Debug.Log(audioLoader.progress);
        yield return audioLoader;
        
            
        Debug.Log(audioLoader.progress);

        Debug.Log(audioLoader.url);
        Debug.Log(audioLoader.GetAudioClip(true,false,AudioType.WAV).name);
        audio.clip = audioLoader.GetAudioClip(true, false, AudioType.WAV);
        audio.Play();
    }

}
