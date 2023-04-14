using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace ronan.player {
    public class Sliding : MonoBehaviour
    {
        [Header("References")]
        public Transform camOrientation;
        public Transform playerOrientation;
        public Transform playerObj;
        private Rigidbody rb;
        private PlayerMovement pm;
        private PlayerInputs pi;
        private CapsuleCollider capCol;

        [Header("Sliding")]
        public float maxSlideTime = 0.75f;
        public float slideForce = 300;
        [SerializeField] private float slideTimer;
        private float slideCooldown;

        public float slideYScale = 0.5f;
        private float startYScale;


        bool slide = false;
        Vector2 movementInput;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            pm = GetComponent<PlayerMovement>();
            capCol = GetComponent<CapsuleCollider>();

            startYScale = capCol.height;
        }

        public void OnSlide(InputAction.CallbackContext context)
        {
            float input = context.ReadValue<float>();
            if (input > 0.5f) slide = true; else slide = false;
        }
        public void OnMove(InputAction.CallbackContext context)
        {
            movementInput = context.ReadValue<Vector2>();
        }

        private void Update()
        {
            if (slideCooldown <= 0 && slide && !pm.sliding && movementInput != Vector2.zero)
            {
                StartSlide();
            }

            if (slideCooldown > 0)
            {
                slideCooldown -= Time.deltaTime;
            }
        }

        private void FixedUpdate()
        {
            if (pm.sliding)
                SlidingMovement();
        }

        private void StartSlide()
        {
            pm.sliding = true;

            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

            slideTimer = maxSlideTime;

            capCol.height = slideYScale;
            capCol.center = new Vector3(0, -0.5f,0);
        }

        private void SlidingMovement()
        {
            Vector3 inputDirection = camOrientation.forward * movementInput.y + camOrientation.right * movementInput.x;

            // sliding normal
            if (!pm.OnSlope() || rb.velocity.y > -0.1f)
            {
                rb.AddForce(inputDirection.normalized * slideForce, ForceMode.Force);

                slideTimer -= Time.deltaTime;
            }

            // sliding down a slope
            else
            {
                rb.AddForce(pm.GetSlopeMoveDirection(inputDirection) * slideForce * 1.4f, ForceMode.Force);
                rb.AddForce(Vector3.down * 10f, ForceMode.Impulse);
            }

            if (slideTimer <= 0)
                StopSlide();
        }

        private void StopSlide()
        {
            pm.sliding = false;
            slideCooldown = maxSlideTime;
            capCol.height = startYScale;
            capCol.center = Vector3.zero;
        }
    }
}
