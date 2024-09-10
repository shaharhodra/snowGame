using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class win : MonoBehaviour
{
	GameObject player;
	StarterAssets.ThirdPersonController tpc;
	private void Start()
	{
		player = GameObject.Find("PlayerArmature");
		tpc = player.GetComponent<StarterAssets.ThirdPersonController>();
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			tpc._animator.SetBool("win", true);
			Invoke("youwin", 3);
			tpc.move = false;
		}
	}
	void youwin()
	{
		tpc._animator.SetBool("win", false);
		tpc.move = true;
		Destroy(gameObject);

	}
}
