using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MitchPlayerMovement : MonoBehaviour
{
    public PlayerInputs controls;
    [SerializeField] private bool AllowJumping;
    [SerializeField] private bool isJumpingRestricted;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;
    private float desiredKBInputH;
    private float desiredKBInputV;
    private float desiredKBInputJ;
    private Rigidbody gameObj;
    private float mass;
    private bool isInAir = false;

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
        controls = new PlayerInputs();
        gameObj = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (AllowJumping)
        {
            if (!isInAir && isJumpingRestricted || !isJumpingRestricted)
            {
                Jumping();
            }
        }
    }

    void VerticalMovement()
    {
        desiredKBInputH = (controls.StandardMovement.HorizMove.ReadValue<float>()); // Go to the StandardMoivement action map and read the HorizMove Vector3 of that mapping.

        if (desiredKBInputH == 1) // If the key that is pressed is the positive binding
        {
            gameObj.velocity = transform.up * moveSpeed; // Create new Vector 3 for moving up.
        }
        else if (desiredKBInputH == -1) // If the key that is pressed is the negative binding
        {
            gameObj.velocity = -transform.up * moveSpeed;
        }
        else // If neither the positive or negative bindings are being detected
        {
            gameObj.velocity = Vector3.Scale((gameObj.velocity), new Vector3(1, 0, 1));
        }
    }

    void HorizontalMovement()
    {
        desiredKBInputV = (controls.StandardMovement.VertMove.ReadValue<float>()); // Go to the StandardMoivement action map and read the VertMove Vector3 of that mapping.

        if (desiredKBInputV == 1) // If the key that is pressed is the positive binding
        {
            gameObj.velocity = transform.right * moveSpeed;
        }
        else if (desiredKBInputV == -1) // If the key that is pressed is the negative binding
        {
            gameObj.velocity = -transform.right * moveSpeed;
        }
        else // If neither the positive or negative bindings are being detected
        {
            gameObj.velocity = Vector3.Scale((gameObj.velocity), new Vector3(0, 1, 1));
        }
    }

    void Jumping()
    {
        desiredKBInputJ = (controls.StandardMovement.Jump.ReadValue<float>()); // Go to the StandardMoivement action map and read the HorizMove Vector3 of that mapping.

        if (desiredKBInputJ == 1 && (gameObj.velocity.y) < 0.001f) // If the key that is pressed is the 
        {
            gameObj.velocity += Vector3.up * jumpHeight;
            if (isJumpingRestricted)
            {
                isInAir = true;
            }
        }
    }
}
