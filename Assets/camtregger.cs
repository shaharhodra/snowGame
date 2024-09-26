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
	public bool side;
	private void Start()
	{
		side = false;
		Vcam = GameObject.Find("Virtual Camera p").GetComponentInChildren<CinemachineVirtualCamera>();
		Player = GameObject.Find("PlayerArmature");
		tpc = Player.GetComponent<StarterAssets.ThirdPersonController>();
	
	}
	private void Update()
	{
		if (side)
		{
			Player.transform.rotation = Quaternion.Euler(00, -90, 00);
			tpc.MoveSpeed = 2;
			tpc.JumpHeight = 0;
		}

	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			Vcam.Priority = 11;
			tpc._animator.SetBool("side", true);
			
			side = true;
			//tcp.onCliff = false;
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			Vcam.Priority = 1;
			side = false;
			tpc._animator.SetBool("side", false);

			//tpc.onCliff = true;
		}
	}

}
