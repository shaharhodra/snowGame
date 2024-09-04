using UnityEngine;

public class FreeRoamCamera : MonoBehaviour
{
    public float movementSpeed = 10.0f;
    public float fastMovementSpeed = 50.0f;
    public float rotationSpeed = 2.0f;
    public float fastRotationSpeed = 5.0f;

    private bool isRotating = false;

    void Update()
    {
        // Movement
        float speed = Input.GetKey(KeyCode.LeftShift) ? fastMovementSpeed : movementSpeed;
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        movement = transform.TransformDirection(movement);
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        // Start rotating
        if (Input.GetMouseButtonDown(1))
        {
            isRotating = true;
        }

        // Stop rotating
        if (Input.GetMouseButtonUp(1))
        {
            isRotating = false;
        }

        // Rotate the camera
        if (isRotating)
        {
            float rotationX = Input.GetAxis("Mouse X") * (Input.GetKey(KeyCode.LeftShift) ? fastRotationSpeed : rotationSpeed);
            float rotationY = Input.GetAxis("Mouse Y") * (Input.GetKey(KeyCode.LeftShift) ? fastRotationSpeed : rotationSpeed);

            transform.Rotate(0, rotationX, 0, Space.World);
            transform.Rotate(-rotationY, 0, 0, Space.Self);
        }

        // Move up/down
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
        }
    }
}
