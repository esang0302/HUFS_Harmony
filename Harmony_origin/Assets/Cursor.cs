using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Attributes;
using Leap;
namespace Leap.Unity
{
    //public class Cursor : Detector
    public class Cursor : MonoBehaviour
    {
        public HandModelBase HandModel = null;
        GameObject cube;
        // Use this for initialization
        void Start()
        {
            GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            cylinder.transform.position = new Vector3(-2, 1, 0);
        }

        // Update is called once per frame
        void Update()
        {
            Instantiate(cube, GetFingerPosition(), Quaternion.identity);
            cube.transform.position = GetFingerPosition()*500;

        }
        private Vector3 GetFingerPosition()
        {
            Finger finger = HandModel.GetLeapHand().Fingers[1];
            Vector3 fingerDirection = finger.Bone(Bone.BoneType.TYPE_DISTAL).Direction.ToVector3();
            Vector3 from = finger.TipPosition.ToVector3();
            Vector3 Direction = finger.Direction.ToVector3();
            return Direction;
        }

    }
}
