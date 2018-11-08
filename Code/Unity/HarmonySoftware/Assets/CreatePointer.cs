using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Attributes;
using Leap;
namespace Leap.Unity
{
    public class CreatePointer : MonoBehaviour
    {
        //public HandModelBase HandModel;
        //public Finger.FingerType FingerName = Finger.FingerType.TYPE_INDEX;
        //public Vector3 tipPosition;
        // Use this for initialization
        Hand leapHand;
        FingerModel finger;
        HandModel handModel;
        Vector3 TipPos;
        GameObject cylinder;
        void Start()
        {
            //FingerDirectionDetector FDD = GameObject.Find("RigidRoundHand_L").GetComponent<FingerDirectionDetector>();

            //Finger finger = HandModel.GetLeapHand().Fingers[1];

            GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            cylinder.transform.position = new Vector3(100, 100, 100);

            //cylinder.transform.position = tipPosition;
            //cylinder.transform.localScale = new Vector3(100, 100, 100);
            /**handModel = GetComponent<HandModel>();
            leapHand = handModel.GetLeapHand();
            if (leapHand == null) Debug.LogError("No leap_hand founded");
                
            finger = handModel.fingers[1];
            TipPos = finger.GetTipPosition();
            Debug.Log(TipPos);
            cylinder.transform.position = TipPos;
            cylinder.transform.localScale = new Vector3(100, 100, 100);
            */

        }

        // Update is called once per frame
        void Update()
        {

        }

        /**private int selectedFingerOrdinal()
        {
            switch (FingerName)
            {
                case Finger.FingerType.TYPE_INDEX:
                    return 1;
                case Finger.FingerType.TYPE_MIDDLE:
                    return 2;
                case Finger.FingerType.TYPE_PINKY:
                    return 4;
                case Finger.FingerType.TYPE_RING:
                    return 3;
                case Finger.FingerType.TYPE_THUMB:
                    return 0;
                default:
                    return 1;
            }
        }**/
    }
}

/** using UnityEngine;
using System.Collections;
using Leap;
namespace Leap.Unity
{
    public class TEST : MonoBehaviour
    {
        HandModel hand_model;
        Hand leap_hand;

        void Start()
        {
            hand_model = GetComponent<HandModel>();
            leap_hand = hand_model.GetLeapHand();
            if (leap_hand == null) Debug.LogError("No leap_hand founded");
        }

        void Update()
        {
            for (int i = 0; i < HandModel.NUM_FINGERS; i++)
            {
                FingerModel finger = hand_model.fingers[i];
                // draw ray from finger tips (enable Gizmos in Game window to see)
                Debug.DrawRay(finger.GetTipPosition(), finger.GetRay().direction, Color.red);
            }
        }
    }
} 
    **/