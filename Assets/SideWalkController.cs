using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWalkController : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this to control movement speed

    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Get horizontal input (A/D keys or left/right arrow keys)
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate movement vector
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, 0f);
        moveDirection *= moveSpeed * Time.deltaTime;

        // Move the character using the CharacterController
        controller.Move(moveDirection);
    }
}
