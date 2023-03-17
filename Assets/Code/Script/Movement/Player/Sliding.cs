using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.InputSystem;

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

        [Header("Sliding")]
        public float maxSlideTime = 0.75f;
        public float slideForce = 300;
        private float slideTimer;

        public float slideYScale = 0.5f;
        private float startYScale;

        Vector2 movementInput;


        private void Awake()
        {
            pi = new PlayerInputs();
        }
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            pm = GetComponent<PlayerMovement>();

            startYScale = playerObj.localScale.y;

        }
        private void OnEnable()
        {
            pi.Enable();
        }
        private void OnDisable()
        {
            pi.Disable();
        }

        private void Update()
        {
            Vector2 movIn = pi.Player.Movement.ReadValue<Vector2>();
            movementInput = movIn;

            if (pi.Player.Slide.WasPressedThisFrame() && movementInput != Vector2.zero)
            {
                StartSlide();
            }

            if (pi.Player.Slide.WasReleasedThisFrame() && pm.sliding)
            {
                StopSlide();
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

            playerObj.localScale = new Vector3(playerObj.localScale.x, slideYScale, playerObj.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

            slideTimer = maxSlideTime;
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

            playerObj.localScale = new Vector3(playerObj.localScale.x, startYScale, playerObj.localScale.z);
        }
    }
}
