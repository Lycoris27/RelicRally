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

        private GameObject body;

        Vector2 movementInput;


        private void Awake()
        {
            pi = new PlayerInputs();
            pm = GetComponent<PlayerMovement>();
            body = GameObject.FindObjectOfType<Animator>().gameObject;
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
            
        }
        private void WallCheck()
        {
            Vector3 foot = transform.position;
            foot.y -= 0.6f;

            wallFront = Physics.SphereCast(foot, sphereCastRadius, playerOrientation.forward, out frontWallHit, detectionLength, whatIsWall);
            wallLookAngle = Vector2.Angle(playerOrientation.forward, -frontWallHit.normal);
            Debug.DrawRay(transform.position, playerOrientation.forward, Color.green);



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

            if (wallFront && wallLookAngle < maxWallLookAngle && !exitingWall)
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
            rb.velocity = new Vector3(0, climbSpeed, 0);
            body.transform.rotation = Quaternion.LookRotation(-frontWallHit.normal);
        }
        private void StopClimbing()
        {
            climbing = false;
            pm.climbing = false;
            //rb.velocity = new Vector3(0, climbSpeed*1.5f, 0);

            climbTimer = 0f;
        }

    }
}
