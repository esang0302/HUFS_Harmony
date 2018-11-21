/******************************************************************************
 * Copyright (C) Leap Motion, Inc. 2011-2018.                                 *
 * Leap Motion proprietary and confidential.                                  *
 *                                                                            *
 * Use subject to the terms of the Leap Motion SDK Agreement available at     *
 * https://developer.leapmotion.com/sdk_agreement, or another agreement       *
 * between Leap Motion and you, your company or other organization.           *
 ******************************************************************************/

using UnityEngine;
using System.Collections;
using Leap;

namespace Leap.Unity{
  /** A physics finger model for our rigid hand made out of various cube Unity Colliders. */
  public class RigidFinger : SkeletalFinger {
  
    public float filtering = 0.5f;
  
    void Start() {
      for (int i = 0; i < bones.Length; ++i) {
        if (bones[i] != null) {
          bones[i].GetComponent<Rigidbody>().maxAngularVelocity = Mathf.Infinity;
        }
      }
    }
        public override void UpdateFinger() {
      for (int i = 0; i < bones.Length; ++i) {
        if (bones[i] != null) {
          // Set bone dimensions.
          CapsuleCollider capsule = bones[i].GetComponent<CapsuleCollider>();
          if (capsule != null) {
            // Initialization
            capsule.direction = 2;
            bones[i].localScale = new Vector3(0.5f/transform.lossyScale.x, 0.5f / transform.lossyScale.y, 0.5f / transform.lossyScale.z);

                        // Update
                        bones[i].position = GetBoneCenter(i);
                        //capsule.transform.rotation = bone.Rotation.ToQuaternion();
                        capsule.radius = GetBoneWidth(i) / 2f;
            capsule.height = GetBoneLength(i) + 5*GetBoneWidth(i);
          }
  
          Rigidbody boneBody = bones[i].GetComponent<Rigidbody>();
          if (boneBody) {
                        //Debug.Log(GetBoneCenter(i));
            boneBody.MovePosition(GetBoneCenter(i));
            boneBody.MoveRotation(GetBoneRotation(i));
          } else {
            bones[i].position = GetBoneCenter(i);
            bones[i].rotation = GetBoneRotation(i);
          }
        }
      }
    }
  }
}
