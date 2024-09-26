using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour

{
    public float moveSpeed = 5f; // Adjust this to control movement speed

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // Get input (left/right arrow keys)

        // Move the character horizontally
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.x);
    }
}


