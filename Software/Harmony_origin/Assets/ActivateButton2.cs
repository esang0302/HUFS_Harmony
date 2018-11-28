using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivateButton2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    public void ChangeGameScene()
    {
        SceneManager.LoadScene("Keyboard");
    }

    // Update is called once per frame
    void Update () {
		
	}
}