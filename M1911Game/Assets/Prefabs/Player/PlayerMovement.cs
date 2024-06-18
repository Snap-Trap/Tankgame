using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    //Variables for the player movement so it can read the input
    public InputAction playerInput;
    public float movementX;
    public float movementY;
    public float movementZ;

    //Variable for better overview of the numbers I want to see
    public float CurrentSpeed;
    public float rotationSpeed =0.1f;

    //small adjustment for speed
    public float extraSpeed;


    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        movementX = playerInput.ReadValue<Vector3>().x;
        movementY = playerInput.ReadValue<Vector3>().y;
        movementZ = playerInput.ReadValue<Vector3>().z;


        Vector3 movement = new Vector3(movementX, movementY, movementZ);
        rb.velocity = transform.forward * (movementZ);
        transform.Rotate(Vector3.up, movementX * rotationSpeed);

        //movementZ = CurrentSpeed;


    }

    void OnEnable()
    {
        playerInput.Enable();
    }

    void OnDisable()
    {
        playerInput.Disable();
    }
}
