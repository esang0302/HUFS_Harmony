using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followHand : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = GetComponentInParent<Transform>().position;
        Debug.Log(GetComponentInParent<Transform>().position);

    }
}
