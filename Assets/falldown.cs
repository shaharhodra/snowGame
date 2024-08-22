using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class falldown : MonoBehaviour
{
	[SerializeField] GameObject player;
	[SerializeField] Transform snowpos;
	[SerializeField] Transform newpos;
	StarterAssets.ThirdPersonController tpc;

	
	private void Start()
	{
		tpc = player.GetComponent<StarterAssets.ThirdPersonController>();

	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			Invoke("PlayerSetActive", 3);
			this.transform.position = snowpos.position;
			tpc._animator.SetBool("dead", true);
			tpc.move = false;

		}
	}
	void PlayerSetActive()
	{
		player.SetActive(false);
		player.transform.position = newpos.position;
		player.SetActive(true);
		tpc._animator.SetBool("dead", false);
		tpc.move = true;
	}
}
