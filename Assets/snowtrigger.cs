using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowtrigger : MonoBehaviour
{
	StarterAssets.ThirdPersonController tpc;
	public GameObject Player;
	private void Start()
	{
		tpc = Player.GetComponent<StarterAssets.ThirdPersonController>();
	}
	private void Update()
	{
		if (tpc.cold==10)
		{
			CancelInvoke("heatconter");
		}
		if (tpc.PlayerState==1)
		{

			CancelInvoke("Reducrspeed");
		}

	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			InvokeRepeating("heatconter", 0, 1);
			InvokeRepeating("Reducrspeed", 0, 4);
		}

	}
	
	void Reducrspeed()
	{
		if (tpc.PlayerState > 1)
		{
			tpc.PlayerState--;


		}
	}
	public void heatconter()
	{
		

		if (tpc.cold<10)
		{
			
			tpc.cold++;

		}
		
	}
}
