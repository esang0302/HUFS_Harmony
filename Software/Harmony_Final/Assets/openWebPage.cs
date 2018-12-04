using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openWebPage : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
    public void openUrl()
    {
        Application.OpenURL("http://www.naver.com");
    }


	// Update is called once per frame
	void Update () {
		
	}
}
