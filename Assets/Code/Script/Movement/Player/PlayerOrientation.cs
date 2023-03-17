using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ronan.player
{
    public class PlayerOrientation : MonoBehaviour
    {
        [SerializeField] private Transform cameraObject;
        private Vector2 rawMovementInput;
        private Vector2 desiredDir;
        Vector3 forward;
        Vector3 backward;
        Vector3 left;
        Vector3 right;

        public float turnSmoothTime = 0.5f;
        float turnSmoothVelocity;

        public void OnMove(InputAction.CallbackContext context)
        {
            rawMovementInput = context.ReadValue<Vector2>();
        }

        private void FixedUpdate()
        {
            Vector3 direction = new Vector3(rawMovementInput.x, 0f, rawMovementInput.y).normalized;

            Vector3 tarForward = cameraObject.forward;

            Vector3 tarRight = cameraObject.right;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraObject.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
        }
    }
}
