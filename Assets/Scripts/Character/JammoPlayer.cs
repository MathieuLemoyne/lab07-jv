using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class JammoPlayer : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float rotationSpeed = 0.1f;

    [Header("Inputs")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;

private CharacterController controller;
private float verticalVelocity;
    
    
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        var camera = Camera.main!;
        var cameraTransform = camera.transform;
        var up = cameraTransform.up;
        var forward = cameraTransform.forward;
        var right = cameraTransform.right;
        
        // retirer la rotation x et z (garder le mouvement horizontal).
        
        forward.y = 0;
        right.y = 0;
        
        //lire les entrées du joueur.
        
        var moveInput = moveAction.action.ReadValue<Vector2>();
        var jumpInput = jumpAction.action.triggered;

        var horizontalMovement = Vector3.zero;
        // Si le joueur veut pas bouger, ne pas faire bouger le joueur.

        if (moveInput == Vector2.zero)
        {
            var moveDirection = forward * moveInput.y + right * moveInput.x;
            horizontalMovement=(moveDirection * (speed * Time.deltaTime));
            
            var lookRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }

        var gravity = Physics.gravity;
        var isGrounded = controller.isGrounded;

        if (isGrounded)
        {
            verticalVelocity = 0;
        }

        if (isGrounded && jumpInput)
        {
            verticalVelocity = Mathf.Sqrt(2 * -gravity.y * 3f);
        }

        verticalVelocity += gravity.y * Time.deltaTime;

        var verticalMovement = up * (verticalVelocity * Time.deltaTime);

        controller.Move(horizontalMovement+verticalMovement);
    }
}