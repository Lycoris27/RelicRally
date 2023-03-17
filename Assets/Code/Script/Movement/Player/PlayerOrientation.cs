using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ronan.player
{
    public class PlayerOrientation : MonoBehaviour
    {
        [SerializeField] private Transform cameraObject;
        private PlayerInputs pi;
        private Vector2 desiredDir;
        Vector3 forward;
        Vector3 backward;
        Vector3 left;
        Vector3 right;

        public float turnSmoothTime = 0.5f;
        float turnSmoothVelocity;

        private void Awake()
        {
            pi = new PlayerInputs();
        }
        private void OnEnable()
        {
            pi.Enable();
        }
        private void OnDisable()
        {
            pi.Disable();
        }

        private void FixedUpdate()
        {
            Vector2 rawMovementInput = pi.Player.Movement.ReadValue<Vector2>();
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
