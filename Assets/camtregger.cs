using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class camtregger : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook cam;
	// Start is called before the first frame update

	
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			cam.Priority = 11;

		}
	}
	private void OnTriggerExit(Collider other)
	{
		cam.Priority = 1;
	}

}
