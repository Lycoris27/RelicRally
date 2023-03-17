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
    private Rigidbody rb;
    private float mass;
    
    // Start is called before the first frame update
    void Awake()
    {
        controls = new PlayerInputs();
        rb = GetComponent<Rigidbody>();
        mass = rb.mass;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
