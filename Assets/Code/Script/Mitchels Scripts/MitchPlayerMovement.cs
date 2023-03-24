using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

[RequireComponent(typeof(Rigidbody))]
public class MitchPlayerMovement : MonoBehaviour
{
    public PlayerInputMap controls;
    [SerializeField] private bool AllowJumping;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float jumpHeight;

    private float desiredKBInputH;
    private float desiredKBInputV;
    private float desiredKBInputT;
    private float desiredKBInputJ;
    private bool isInAir = false;
    private Rigidbody rb;

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    // Start is called before the first frame update
    void Awake()
    {
        controls = new PlayerInputMap();
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerCharacterMovement();

        if (AllowJumping)
        {
            if (!isInAir)
            {
                Jumping();
            }
        }
    }

    void PlayerCharacterMovement()
    {
        desiredKBInputV = (controls.TestMovement.VertMove.ReadValue<float>()); // Go to the StandardMovement action map and read the HorizMove Vector3 of that mapping.

        if (desiredKBInputV == 1) // If the key that is pressed is the positive binding
        {
            this.transform.position += this.transform.forward * moveSpeed * Time.deltaTime;
        }
        else if (desiredKBInputV == -1) // If the key that is pressed is the negative binding
        {
            this.transform.position -= this.transform.forward * moveSpeed * Time.deltaTime;
        }

        desiredKBInputH = (controls.TestMovement.HorizMove.ReadValue<float>()); // Go to the StandardMoivement action map and read the VertMove Vector3 of that mapping.

        if (desiredKBInputH == 1) // If the key that is pressed is the positive binding
        {
            this.transform.position += this.transform.right * moveSpeed * Time.deltaTime;
        }
        else if (desiredKBInputH == -1) // If the key that is pressed is the negative binding
        {
            this.transform.position -= this.transform.right * moveSpeed * Time.deltaTime;
        }

        desiredKBInputT = (controls.TestMovement.Turn.ReadValue<float>());

        if (desiredKBInputT == 1)
        {
            this.transform.RotateAround(this.transform.position, Vector3.up, turnSpeed * Time.deltaTime);
        }
        else if (desiredKBInputT == -1)
        {
            this.transform.RotateAround(this.transform.position, Vector3.up, -turnSpeed * Time.deltaTime);
        }
    }

    void Jumping()
    {
        desiredKBInputJ = (controls.TestMovement.Jump.ReadValue<float>()); // Go to the StandardMoivement action map and read the HorizMove Vector3 of that mapping.

        if (desiredKBInputJ == 1 && Mathf.Abs(rb.velocity.y) < 0.01f) 
        {
            rb.velocity += Vector3.up * (this.jumpHeight);
        }
    }
}
