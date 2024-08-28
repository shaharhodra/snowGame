using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class falldown : MonoBehaviour
{
   GameObject player;
     Transform snowpos;
	 Transform newpos;
	StarterAssets.ThirdPersonController tpc;
	 GameObject fallsnow;

	
	private void Start()
	{
		fallsnow = GameObject.Find("fallSnow");
		snowpos = GameObject.Find("snowpos").GetComponent<Transform>();
		newpos = GameObject.Find("startPos").GetComponent<Transform>();
		player = GameObject.Find("PlayerArmature");
		tpc = player.GetComponent<StarterAssets.ThirdPersonController>();

	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			Invoke("PlayerSetActive", 3);
			fallsnow.transform.position = snowpos.position;
			fallsnow.GetComponent<Rigidbody>().isKinematic = true;
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
