using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class VehicleRotation : MonoBehaviour
{
    [SerializeField] private GameObject playerInputSource;  // Reference to the GameObject with PlayerInput
    [SerializeField] private float turnRotationAngle = 15f; // Maximum rotation angle during turns
    [SerializeField] private float rotationSmoothing = 5f;  // Speed of rotation smoothing
    [SerializeField] private Boolean isFirstPlayer = true;

    private Quaternion originalRotation; // Store the original rotation
    private float targetYRotation = 0f;  // Target Y rotation offset based on turning input
    private Vector2 moveInput;           // Stores input values from the Input System

    private PlayerInput playerInput;     // Reference to the Input System's PlayerInput component

    // Start is called before the first frame update
    void Start()
    {
        // Store the initial rotation of the vehicle
        originalRotation = transform.localRotation;

        // Get the PlayerInput component from the assigned GameObject
        if (playerInputSource != null)
        {
            playerInput = playerInputSource.GetComponent<PlayerInput>();
            if (playerInput == null)
            {
                Debug.LogError("PlayerInput component not found on the assigned GameObject!");
            }
        }
        else
        {
            Debug.LogError("PlayerInput source GameObject is not assigned!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInput != null)
        {
            HandleTurnRotation();
        }
    }

    // Handles the rotation during turns
    void HandleTurnRotation()
    {
        InputAction moveAction;
        if (isFirstPlayer) // If "Player1Move" doesn't exist, try "Player2Move"
        {
            moveAction = playerInput.actions.FindAction("Player1Move");
            //Debug.Log("Player 1 Move");
        }
        else
        {
            moveAction = playerInput.actions.FindAction("Player2Move");
            //Debug.Log("Player 2 Move");
        }

        //if (moveAction == null) // If neither action exists, log a warning and exit
        //{
        //    Debug.LogWarning("Neither 'Player1Move' nor 'Player2Move' exists in the PlayerInput actions.");
        //    return;
        //}

        // Read the move input value from the found action
        moveInput = moveAction.ReadValue<Vector2>();

        // Only perform rotation when forward or backward input is active
        if (Mathf.Abs(moveInput.y) > 0.01f) // Small threshold to ignore very slight inputs
        {
            // Check if moving backward and invert turn input if necessary
            float turnInput = moveInput.y < 0 ? -moveInput.x : moveInput.x;

            // Calculate the target Y rotation based on the (possibly inverted) horizontal input
            targetYRotation = turnInput * turnRotationAngle;

            // Smoothly interpolate to the target rotation
            Quaternion targetRotation = Quaternion.Euler(
                originalRotation.eulerAngles.x,
                originalRotation.eulerAngles.y + targetYRotation,
                originalRotation.eulerAngles.z
            );
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * rotationSmoothing);
        }
        else
        {
            // Reset the rotation to the original rotation if there's no forward or backward input
            transform.localRotation = Quaternion.Lerp(transform.localRotation, originalRotation, Time.deltaTime * rotationSmoothing);
        }
    }


}
