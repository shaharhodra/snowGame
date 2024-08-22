using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cliff : MonoBehaviour
{
	StarterAssets.ThirdPersonController tpc;
	[SerializeField] GameObject Player;


	private void Start()
	{
		tpc = Player.GetComponent<StarterAssets.ThirdPersonController>();

	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			tpc._animator.SetBool("slide", true);
			tpc.slde = true;
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			tpc._animator.SetBool("slide", false);
			tpc.slde = false;
		}
	}
}
