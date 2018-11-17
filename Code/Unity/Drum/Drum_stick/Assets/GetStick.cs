using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Leap.Unity.Attributes;
using Leap;
namespace Leap.Unity
{

    public class GetStick : MonoBehaviour
    {
        HandModel HandModels;
        Hand leap_hand;
        Hand hand;
        public HandModelBase HandModel = null;
        GameObject cylinder;
        public GameObject objectToSpawn;
        public Vector3 spawnLocation;
        public void spawn()
        {   
           // Instantiate(objectToSpawn, spawnLocation, Quaternion.identity);
            GameObject Cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            cylinder.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            cylinder.transform.position = new Vector3(0,0,0);
            
        }
    
        // Use this for initialization
        void Start()
        {


        }

        // Update is called once per frame
        void Update()
        {

        }
       /* private Vector3 GetPalmPosition()
        {
            Vector3 from = palm.PalmPosition.ToVector3();
            Vector3 Direction = palm.Direction.ToVector3();
        }
        */
    }
}