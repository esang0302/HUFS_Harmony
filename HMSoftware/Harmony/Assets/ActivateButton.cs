using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ActivateButton : MonoBehaviour {

    // Use this for initialization
    void Start () {

    }

    public void ChangeGameScene()
{
    SceneManager.LoadScene("Drum");
}

// Update is called once per frame
void Update () {
        
    }
}

    
