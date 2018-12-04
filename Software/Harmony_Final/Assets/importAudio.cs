using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
//#if UNITY_EDITOR
using UnityEditor;
//#endif
[RequireComponent(typeof(AudioSource))]
public class importAudio : MonoBehaviour {
    GameObject t;
    AudioSource audio;
    AudioClip loadedClip;
    private string musicFile;
//#if UNITY_EDITOR
    public void findFile () {

     //   musicFile = EditorUtility.OpenFilePanel("Load files", "", "");
        t = GameObject.Find("ttt");
        t.transform.Rotate(0, 0, 90);
        StartCoroutine(loadAudio());
    }
//#endif
    public IEnumerator loadAudio()
    {
        WWW audio_loader = new WWW("file:///"+musicFile);
        while (!audio_loader.isDone)
            Debug.Log(audio_loader.progress);
        yield return audio_loader;
        
        Debug.Log(audio_loader.progress);
        Debug.Log(audio_loader.url);
        loadedClip = audio_loader.GetAudioClip(false, false,AudioType.WAV);
        audio = gameObject.GetComponent<AudioSource>();
        audio.clip = loadedClip;
    }
}
