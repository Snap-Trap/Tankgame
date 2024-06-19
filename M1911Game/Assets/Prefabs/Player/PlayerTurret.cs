using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTurret : MonoBehaviour
{
    // InputAction for the turret so it can read the input
    public InputAction TurretInput;
    private Vector2 turretX;


    // Variable for better overview of the numbers I want to see
    public Vector2 TurretRotation;

    // public variables to find an object;
    public GameObject turretObject;

    // Other script variables
    public PlayerMovement playerMovement;

    // Normal variables
    public float turretSpeed;
    public bool isMoving = false;

    void Start()
    { 
        //Make sure the turret is found, the turret is a child of a child of the player
        // Ok so first shit didn't work use GameObject for christ sake
        turretObject = GameObject.Find("Turret");
        if (turretObject == null)
        {
            Debug.LogError("Turret not found");
        }
        else
        {
            Debug.Log("Turret found");
        }
    }

    void Update()
    {
        if (playerMovement.CurrentSpeed > 0 || playerMovement.CurrentSpeed < 0)
        {
            isMoving = true;
        }
        else if (playerMovement.CurrentSpeed == 0)
        {
            isMoving = false;
        }

        if (isMoving == false)
        {
            // Make the turret rotate to either left or right
            TurretRotation = turretX = TurretInput.ReadValue<Vector2>();
            turretObject.transform.Rotate (Vector3.up, (turretX.x * turretSpeed));



        }
    }

    void OnEnable()
    {
        TurretInput.Enable();
    }

    void OnDisable()
    {
        TurretInput.Disable();
    }
}
