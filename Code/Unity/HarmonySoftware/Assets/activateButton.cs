using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class activateButton : MonoBehaviour {

    GameObject sphere;
    // Use this for initialization
    void Start () {

    }
    public void CreateObject()
    {
        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = new Vector3(410, 200, -100);
        sphere.transform.localScale = new Vector3(100, 100, 100);
        Rigidbody gameObjectsRigidBody = sphere.AddComponent<Rigidbody>();
        gameObjectsRigidBody.useGravity = true;

    }

    // Update is called once per frame
    void Update () {
        
    }
}

    
