using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class SlopeSlide : MonoBehaviour
{
    public float slideForce = 10f; // Adjust as needed
    public float maxSlideSpeed = 5f; // Limit the sliding speed

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Cast a ray downward to detect slopes
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit))
        {
            Vector3 slopeNormal = hit.normal;
            float slopeAngle = Vector3.Angle(Vector3.up, slopeNormal);

            // Check if the slope angle exceeds a threshold (e.g., 30 degrees)
            if (slopeAngle > 3f)
            {
                // Apply sliding force
                Vector3 slideForceVector = -slopeNormal * slideForce;
                rb.AddForce(slideForceVector, ForceMode.Force);

                // Limit sliding speed
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSlideSpeed);
            }
        }
    }
}




