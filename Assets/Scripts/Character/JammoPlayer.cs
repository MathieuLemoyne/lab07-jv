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

    private CharacterController controller;
    
    
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        var camera = Camera.main!;
        var cameraTransform = camera.transform;
        var forward = cameraTransform.forward;
        var right = cameraTransform.right;
        
        // retirer la rotation x et z (garder le mouvement horizontal).
        
        forward.y = 0;
        right.y = 0;
        
        //lire les entrées du joueur.
        
        var moveInput = moveAction.action.ReadValue<Vector2>();
        
        // Si le joueur veut pas bouger, ne pas faire bouger le joueur.

        if (moveInput == Vector2.zero)
        {
            controller.Move(Vector3.zero);
        }
        else
        {
            var moveDirection = forward * moveInput.y + right * moveInput.x;
            controller.Move(moveDirection * (speed * Time.deltaTime));
            
            var lookRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
            
            
        }
        
    }
}