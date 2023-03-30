using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.XR;

namespace ronan.player
{
    public class Climbing : MonoBehaviour
    {
        /// <summary>
        /// fix player being able to bounce against the wall to reset their climb
        /// </summary>

        [Header("Reference")]
        public Transform camOrientation;
        public Transform playerOrientation;
        public Rigidbody rb;
        public LayerMask whatIsWall;
        public PlayerInputs pi;
        private PlayerMovement pm;

        [Header("Climbing")]
        public float climbSpeed = 10f;
        public float maxClimbTime = 0.75f;
        private float climbTimer;

        private bool climbing;

        [Header("ClimbJumping")]
        public float climbJumpUpForce = 10;
        public float climbJumpBackForce = 6;

        public int climbJumps = 0;
        private int climbJumpsLeft;

        [Header("Detection")]
        public float detectionLength = 0.7f;
        public float sphereCastRadius = 0.25f;
        public float maxWallLookAngle = 30;
        private float wallLookAngle;

        private RaycastHit frontWallHit;
        private bool wallFront;

        private Transform lastWall;
        private Vector3 lastWallNormal;
        public float minWallNormalAngleChange;

        [Header("Exiting")]
        public bool exitingWall;
        public float exitWallTime = 0.2f;
        private float exitWallTimer;

        Vector2 movementInput;


        private void Awake()
        {
            pi = new PlayerInputs();
            pm = GetComponent<PlayerMovement>();
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
            Vector2 movIn = pi.Player.Movement.ReadValue<Vector2>();
            movementInput = movIn;

            WallCheck();
            StateMachine();

            if (climbing && !exitingWall) ClimbingMovement();
            //if (wallFront && !climbing && climbTimer < maxClimbTime && climbTimer > 0) rb.velocity = new Vector3(rb.velocity.x, -4, rb.velocity.z);
        }
        private void WallCheck()
        {
            wallFront = Physics.SphereCast(transform.position, sphereCastRadius, playerOrientation.forward, out frontWallHit, detectionLength, whatIsWall);
            wallLookAngle = Vector2.Angle(playerOrientation.forward, -frontWallHit.normal);

            bool newWall = frontWallHit.transform != lastWall || Mathf.Abs(Vector3.Angle(lastWallNormal, frontWallHit.normal)) > minWallNormalAngleChange;

            if (pm.grounded)
            {
                climbTimer = maxClimbTime;
            }
        }

        private void StateMachine()
        {
            if (wallFront && !climbing && climbTimer == 0)
            {
                if (rb.velocity.y >= -5)
                {
                    Vector3 forceToApply = new Vector3(0, -0.2f, 0);
                    rb.AddForce(forceToApply, ForceMode.Impulse);
                }
            } else

            if (wallFront && pi.Player.Jump.IsInProgress() && wallLookAngle < maxWallLookAngle && !exitingWall)
            {
                if (!climbing && climbTimer > 0) StartClimbing();

                if (climbTimer > 0) climbTimer -= Time.deltaTime;
                if (climbTimer < 0) StopClimbing();
            } 

            else if (exitingWall)
            {
                if (climbing) StopClimbing();

                if (exitWallTimer > 0) exitWallTimer -= Time.deltaTime;
                if (exitWallTimer < 0) exitingWall = false;
            }

            else
            {
                if (climbing) StopClimbing();
            }

            if (wallFront && pi.Player.Jump.IsPressed() && climbJumpsLeft > 0) ClimbJump();
        }
        private void StartClimbing()
        {
            climbing = true;
            pm.climbing = true;

            lastWall = frontWallHit.transform;
            lastWallNormal = frontWallHit.normal;
        }
        private void ClimbingMovement()
        {
            rb.velocity = new Vector3(rb.velocity.x, climbSpeed, rb.velocity.z);
        }
        private void StopClimbing()
        {
            climbing = false;
            pm.climbing = false;

            climbTimer = 0f;
        }

        private void ClimbJump()
        {
            exitingWall = true;
            exitWallTimer = exitWallTime;

            Vector3 forceToApply = transform.up * climbJumpUpForce + frontWallHit.normal * climbJumpBackForce;

            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(forceToApply, ForceMode.Impulse);

            climbJumpsLeft--;
        }

    }
}
