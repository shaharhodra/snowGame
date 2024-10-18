using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public GameObject targetCamera; // Assign the camera you want to control in the Inspector

    private void Start()
    {
        if (targetCamera != null)
            targetCamera.gameObject.SetActive(false); // Ensure the camera starts off
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (targetCamera != null)
                targetCamera.gameObject.SetActive(true); // Turn the camera on
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (targetCamera != null)
                targetCamera.gameObject.SetActive(false); // Turn the camera off
        }
    }
}

