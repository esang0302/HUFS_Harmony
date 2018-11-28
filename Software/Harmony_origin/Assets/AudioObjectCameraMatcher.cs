using UnityEngine;
using System.Collections;

public class AudioObjectCameraMatcher : MonoBehaviour {
    AudioSource audioObject;
	// Use this for initialization
	void Start () {
        foreach (AudioSource audioObject in (AudioSource[])GameObject.FindObjectsOfType(typeof(AudioSource)))
        {
            audioObject.gameObject.transform.position = Camera.main.transform.position;
        }
    }
	
	// Update is called once per frame
	void LateUpdate () {
	    // Find all objects with name starting with "AudioObject"
        // and set their position to main camera position
        
	}
}
