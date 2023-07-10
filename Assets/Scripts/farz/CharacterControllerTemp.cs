

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterControllerTemp : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float jumpForce = 5f;
    public float gravity = 9.81f;

    private CharacterController controller;
    private Vector3 moveDirection;
    private bool isJumping = false;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Movement controls
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        moveDirection = new Vector3(moveHorizontal, 0f, moveVertical);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= movementSpeed;

        // Jumping control
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            moveDirection.y = jumpForce;
            isJumping = true;
        }

        // Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the character controller
        controller.Move(moveDirection * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Reset jump state when character lands on the ground
        if (hit.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}
