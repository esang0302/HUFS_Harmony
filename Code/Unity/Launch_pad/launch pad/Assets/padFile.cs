using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class padFile : MonoBehaviour {

    

	// Use this for initialization
	void Start () {
		
	}

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(gameObject.name);
            GameObject.Find("Main Camera").GetComponent<TextFileFinder>().fileBrowserOpen(gameObject.name);
        }
    }
        // Update is called once per frame

        void Update () {
		
	}
}
