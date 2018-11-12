using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
[RequireComponent(typeof(AudioSource))]
public class importAudio : MonoBehaviour {

    AudioSource audio;
    AudioClip loadedClip;
    private string musicFile;

#if UNITY_EDITOR
    public void findFile () {
        musicFile = EditorUtility.OpenFilePanel("Load files", "", "");
        StartCoroutine(loadAudio());
    }
#endif
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
