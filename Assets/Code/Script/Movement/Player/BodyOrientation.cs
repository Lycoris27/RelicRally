using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ronan.player
{
    public class BodyOrientation : MonoBehaviour
    {
        [SerializeField] private GameObject targetObject;
        [SerializeField] private float turnSmoothTime;
        private PlayerMovement pm;
        private void Awake()
        {
            pm = GetComponentInParent<PlayerMovement>();
        }
        // Update is called once per frame
        void FixedUpdate()
        {
            if (pm.climbing) return;
            if (targetObject != null)
            {
                // Get the target object's y rotation
                Quaternion targetRotation = targetObject.transform.rotation;

                gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, targetRotation, turnSmoothTime * Time.deltaTime);
            }
            else if (targetObject == null)
            {
                Debug.Log("No Target Set");
            }
        }
    }
}
