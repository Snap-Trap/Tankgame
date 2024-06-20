using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTurret : MonoBehaviour
{
    // InputAction for the turret so it can read the input
    public InputAction TurretInput;
    public InputAction ShootInput;
    private Vector2 turretX;


    // Variable for better overview of the numbers I want to see
    public Vector2 TurretRotation;

    // public variables to find an object;
    public GameObject turretObject;
    public GameObject shellPrefab;

    // Other script variables
    public PlayerMovement playerMovement;

    // Normal variables
    public float turretSpeed;
    public float shellSpeed;

    public bool isMoving = false;
    public bool canShoot = true;

    public Transform firePoint;

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

            if (ShootInput.triggered && canShoot)
            {
                // Quaternion forwardRotation = Quaternion.Euler(0, 0, 0);
                GameObject ShellInstance = Instantiate(shellPrefab, firePoint.position, firePoint.rotation);

                // Apply force to the bullet for movement
                ShellInstance.GetComponent<Rigidbody>().AddForce(ShellInstance.transform.forward * shellSpeed, ForceMode.Impulse);

                canShoot = false;
                StartCoroutine(ShootCooldown());
            }
        }
    }

    // make something so there is cooldown for shooting

    IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(2);
        canShoot = true;
    }

    void OnEnable()
    {
        ShootInput.Enable();
        TurretInput.Enable();
    }

    void OnDisable()
    {
        ShootInput.Disable();
        TurretInput.Disable();
    }
}
