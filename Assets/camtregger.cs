using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class camtregger : MonoBehaviour
{
	CinemachineVirtualCamera Vcam;
	// Start is called before the first frame update
	GameObject Player;
	StarterAssets.ThirdPersonController tpc;
	public float moveSpeed = 5.0f;
	public float jumpForce = 8.0f;
	Rigidbody rb;
	private void Start()
	{
		Vcam = GameObject.Find("Virtual Camera p").GetComponentInChildren<CinemachineVirtualCamera>();
		Player = GameObject.Find("PlayerArmature");
		tpc = Player.GetComponent<StarterAssets.ThirdPersonController>();
	
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			//cam.Priority = 11;
			Vcam.Priority = 11;
			tpc.onCliff = false;
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			//cam.Priority = 1;
			Vcam.Priority = 1;

			tpc.onCliff = true;
		}
	}

}
